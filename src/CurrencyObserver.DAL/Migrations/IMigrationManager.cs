﻿using CurrencyObserver.DAL.Providers;
using Microsoft.Extensions.Logging;

namespace CurrencyObserver.DAL.Migrations;

public interface IMigrationManager
{
    void ApplyMigrations(
        IPgSqlConnectionProvider pgSqlConnectionProvider,
        ILogger logger);
}