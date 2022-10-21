using System.Data;
using CurrencyObserver.Common.Hardcode;
using CurrencyObserver.Common.Models;
using CurrencyObserver.DAL.Extensions;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using PostgreSQLCopyHelper;

namespace CurrencyObserver.DAL.Repositories;

public class CurrencyRepository : PostgresRepositoryBase, ICurrencyRepository
{
    public CurrencyRepository(IOptions<PgOptions> options) : base(options) { }

    private const string TempTable = "temp_currency";

    public async Task<List<Currency>> GetLstAsync(
        NpgsqlTransaction transaction,
        DateTime? toDate,
        CancellationToken cancellationToken)
    {
        using var cts = CreateCancellationTokenSource(cancellationToken);

        var whereStatement = string.Empty;
        if (toDate is not null)
        {
            whereStatement = "WHERE added_at = @added_at";
        }

        var query = $@"
-- @Query({GetQueryName()})
SELECT
   {SelectFieldsLst}
FROM
    currency_observer.currency
{whereStatement};";

        await using var command = transaction.Connection!.CreateCommand();

        command.CommandText = query;

        if (!string.IsNullOrEmpty(whereStatement))
        {
            command.Parameters.AddWithValue("@added_at", toDate!);
        }

        await using var reader = await command.ExecuteReaderAsync(cts.Token);

        // Pre-allocation of the currencies list
        var currencies = new List<Currency>(WellKnownCurrencyCodes.CbrCharCodeToCurrencyCodeMap.Keys.Count);
        while (await reader.ReadAsync(cts.Token))
        {
            currencies.Add(ReadCurrency(reader));
        }

        return currencies;
    }

    public async Task UpsertLstAsync(
        NpgsqlTransaction transaction,
        IEnumerable<Currency> currencies,
        CancellationToken cancellationToken)
    {
        using var cts = CreateCancellationTokenSource(cancellationToken);

        await CopyCurrenciesToTempTableAsync(transaction, currencies, cts.Token);
        await MergeCurrenciesFromTempTableAsync(transaction, cts.Token);
    }

    private async Task CopyCurrenciesToTempTableAsync(
        NpgsqlTransaction transaction,
        IEnumerable<Currency> currencies,
        CancellationToken cancellationToken)
    {
        await using var createTempTableCommand = transaction.Connection!.CreateCommand();
        createTempTableCommand.CommandText = $@"
-- @Query({GetQueryName()})
CREATE TEMP TABLE IF NOT EXISTS {TempTable}
(
    id            BIGINT                NOT NULL,
    currency_code INT                   NOT NULL,
    value         DOUBLE PRECISION      NOT NULL,
    name          VARCHAR(128)          NOT NULL,
    added_at      TIMESTAMP             NOT NULL
) ON COMMIT DROP;
";
        await createTempTableCommand.ExecuteNonQueryAsync(cancellationToken);
        var currenciesCopyHelper = new PostgreSQLCopyHelper<Currency>(TempTable)
            .MapBigInt("id", currency => currency.Id)
            .MapInteger("currency_code", currency => (int)currency.CurrencyCode)
            .MapDouble("value", currency => currency.Value)
            .MapVarchar("name", currency => currency.Name)
            .MapTimeStamp("added_at", currency => currency.AddedAt);

        await currenciesCopyHelper.SaveAllAsync(transaction.Connection, currencies, cancellationToken);
    }

    private async Task MergeCurrenciesFromTempTableAsync(
        NpgsqlTransaction transaction,
        CancellationToken cancellationToken)
    {
        await using var mergeCurrenciesCommand = transaction.Connection!.CreateCommand();
        
        mergeCurrenciesCommand.CommandText = $@"
-- @Query({GetQueryName()})
INSERT INTO currency_observer.currency (id,
                                        currency_code,
                                        value,
                                        name,
                                        added_at)
SELECT id,
       currency_code,
       value,
       name,
       added_at
FROM {TempTable}
ON CONFLICT (id, added_at)
    DO UPDATE
    SET currency_code = excluded.currency_code,
        value         = excluded.value,
        name          = excluded.name,
        added_at      = excluded.added_at;
";

        await mergeCurrenciesCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private static Currency ReadCurrency(IDataRecord reader)
    {
        return new Currency(
            reader.GetInt64(0),
            reader.GetEnum<CurrencyCode>(1),
            reader.GetDouble(2),
            reader.GetString(3),
            reader.GetDateTime(4));
    }

    private const string SelectFieldsLst = @"
    id,
    currency_code,
    value,
    name,
    added_at
";
}