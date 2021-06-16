using System;

namespace HappyLittleHelpers.AdhocDataQueries.ValueConverters
{
    class EnumValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType)
        {
            return Enum.Parse(targetType, value.ToString());
        }
    }
}