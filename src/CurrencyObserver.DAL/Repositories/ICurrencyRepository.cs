﻿using CurrencyObserver.Common.Models;

namespace CurrencyObserver.DAL.Repositories;

public interface ICurrencyRepository
{
   Task<List<Currency>> GetLstAsync (CancellationToken cancellationToken);

   Task UpsertLstAsync(IEnumerable<Currency> currencies, CancellationToken cancellationToken);
}