using System;
using CurrencyObserver.DAL.Providers;

namespace CurrencyObserver.IntegrationTests.Helpers;

public class PgSqlDbCleanUpHelper : IDisposable
{
    public void Dispose()
    {
        CleanUpPgDb();
    }

    private void CleanUpPgDb()
    {
        using var dbConnection = ServiceClient.GetService<IPgSqlConnectionProvider>().OpenConnection();
        using var command = dbConnection.CreateCommand();

        command.CommandText = $@"
-- @Query({nameof(CleanUpPgDb)})
DELETE FROM currency_observer.currency;";

        command.ExecuteNonQuery();
    }
}