using CurrencyObserver.Common.Models;

namespace CurrencyObserver.DAL.Repositories;

public interface ICurrencyRepository
{
   Task<List<Currency>> GetAllAsync (CancellationToken cancellationToken);
}