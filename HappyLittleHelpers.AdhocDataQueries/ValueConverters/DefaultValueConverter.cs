using System;

namespace HappyLittleHelpers.AdhocDataQueries.ValueConverters
{
    internal class DefaultValueConverter: IValueConverter
    {
        public object Convert(object value,Type targetType) => System.Convert.ChangeType(value, targetType);
    }
}