using System.Data.Common;
using Npgsql;

namespace CurrencyObserver.DAL.Extensions;

public static class NpgsqlParameterCollectionExtensions
{
    public static NpgsqlParameter AddWithValue(
        this DbParameterCollection parameterCollection,
        string name,
        object value)
    {
        var npgsqlParameter = new NpgsqlParameter(name, value);
        parameterCollection.Add(npgsqlParameter);
        return npgsqlParameter;
    }
}