using System;

namespace HappyLittleHelpers.AdhocDataQueries.ValueConverters
{
    public interface IValueConverter
    {
        object Convert(object value,Type targetType);
    }
}