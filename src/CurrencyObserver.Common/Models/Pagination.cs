namespace CurrencyObserver.Common.Models;

public record Pagination(int Limit, int OrderNumber)
{
    public int GetOffsetSize() => OrderNumber > 0 ? (OrderNumber - 1) * Limit : 0;
};