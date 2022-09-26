using System.Data;
using CurrencyObserver.Common.Hardcode;
using CurrencyObserver.Common.Models;
using CurrencyObserver.DAL.Extensions;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Options;

namespace CurrencyObserver.DAL.Repositories;

public class CurrencyRepository : PostgresRepositoryBase, ICurrencyRepository
{
    public CurrencyRepository(IOptions<DatabaseOptions> options) : base(options) { }

    public async Task<List<Currency>> GetAllAsync(CancellationToken cancellationToken)
    {
        using var cts = CreateCancellationTokenSource(cancellationToken);
        
        var query = $@"
-- @Query({GetQueryName()})
SELECT
   {SelectFieldsLst}
FROM
    currency_observer.currency;";
       
        await using var connection = await OpenConnectionAsync(cts.Token);
        await using var command = connection.CreateCommand();
        
        command.CommandText = query;
        
        await using var reader = await command.ExecuteReaderAsync(cts.Token);

        // Pre-allocation of the currencies list
        var currencies = new List<Currency>(WellKnownCurrencyCodes.CbrCharCodeToCurrencyCodeMap.Keys.Count);

        while (await reader.ReadAsync(cts.Token))
        {
            currencies.Add(ReadCurrency(reader));
        }

        return currencies;
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
    updated_at
";
}