using System.Data;
using System.Text;
using CurrencyObserver.Common.Extensions;
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

    public async Task<List<Currency>> GetAsync(
        NpgsqlTransaction transaction,
        DateTime? onDate,
        CurrencyCode? currencyCode,
        Pagination? pagination,
        CancellationToken cancellationToken)
    {
        using var cts = CreateCancellationTokenSource(cancellationToken);

        var filters = new List<Filter>
        {
            onDate switch
            {
                null => Filter.Empty,
                not null => Filter.WhereParameter(
                    "valid_date = @valid_date", 
                    "@valid_date",
                    onDate.Value)
            },
            currencyCode switch
            {
                null => Filter.Empty,
                not null => Filter.WhereParameter(
                    "currency_code = @currency_code", 
                    "@currency_code", 
                    currencyCode.ToInt())
            }
        };

        var paginationStatement = string.Empty;
        if (pagination is not null)
        {
            paginationStatement = $"LIMIT {pagination.Limit} OFFSET {pagination.GetOffsetSize()}";
        }

        var query = $@"
-- @Query({GetQueryName()})
SELECT
   {SelectFieldsLst}
FROM
    currency_observer.currency
WHERE {{0}}
{paginationStatement};";

        await using var command = transaction.Connection!.CreateCommand();

        command.CommandText = query;
        
        Filter.Apply(command, filters);

        await using var reader = await command.ExecuteReaderAsync(cts.Token);

        // Pre-allocation of the currencies list
        var currencies = new List<Currency>(WellKnownCurrencyCodes.CbrCharCodeToCurrencyCodeMap.Keys.Count);
        while (await reader.ReadAsync(cts.Token))
        {
            currencies.Add(ReadCurrency(reader));
        }

        return currencies;
    }

    public async Task AddOrUpdateAsync(
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
    valid_date    TIMESTAMP             NOT NULL
) ON COMMIT DROP;
";
        await createTempTableCommand.ExecuteNonQueryAsync(cancellationToken);
        var currenciesCopyHelper = new PostgreSQLCopyHelper<Currency>(TempTable)
            .MapBigInt("id", currency => currency.Id)
            .MapInteger("currency_code", currency => currency.CurrencyCode.ToInt())
            .MapDouble("value", currency => currency.Value)
            .MapVarchar("name", currency => currency.Name)
            .MapTimeStamp("valid_date", currency => currency.ValidDate);

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
                                        valid_date)
SELECT id,
       currency_code,
       value,
       name,
       valid_date
FROM {TempTable}
ON CONFLICT (id, valid_date)
    DO UPDATE
    SET currency_code = excluded.currency_code,
        value         = excluded.value,
        name          = excluded.name,
        valid_date    = excluded.valid_date;
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
    valid_date
";
}

internal class Filter
{
    protected Filter(string whereString)
    {
        WhereString = whereString;
    }

    private string WhereString { get; }

    protected virtual bool HasParameter => false;

    protected virtual NpgsqlParameter CreateParameter() =>
        throw new InvalidOperationException($"Parameterless filter: {WhereString}");

    public static Filter Where(string condition)
    {
        return new Filter(condition);
    }

    public static Filter WhereParameter<TType>(string condition, string parameterName, TType value)
    {
        return new Filter<TType>(condition, parameterName, value);
    }

    public static Filter Empty { get; } = new("TRUE");

    public static void Apply(NpgsqlCommand command, IReadOnlyCollection<Filter> filters)
    {
        var whereCondition = string.Join(
            "\nAND ",
            filters
                .Where(filter => filter != Empty)
                .Select(filter => filter.WhereString));
        
        if (string.IsNullOrWhiteSpace(whereCondition))
        {
            whereCondition = "TRUE";
        }
        
        command.CommandText = string.Format(command.CommandText, whereCondition);
        
        foreach (var filterWithParameter in filters.Where(filter => filter.HasParameter))
        {
            command.Parameters.Add(filterWithParameter.CreateParameter());
        }
    }
}

internal class Filter<TType> : Filter
{
    private string ParameterName { get; }

    private TType ParameterValue { get; }

    public Filter(
        string whereString,
        string parameterName,
        TType parameterValue)
        : base(whereString)
    {
        ParameterName = parameterName;
        ParameterValue = parameterValue;
    }

    protected override bool HasParameter => true;

    protected override NpgsqlParameter CreateParameter()
    {
        return new NpgsqlParameter<TType>(ParameterName, ParameterValue);
    }
}