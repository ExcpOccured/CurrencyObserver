using System.Data;
using CurrencyObserver.Common.Extensions;

namespace CurrencyObserver.DAL.Extensions;

public static class DataRecordExtensions
{
    public static TEnum GetEnum<TEnum>(this IDataRecord dataRecord, int index) 
        where TEnum : Enum => dataRecord.GetInt32(index).ToEnum<TEnum>();
}