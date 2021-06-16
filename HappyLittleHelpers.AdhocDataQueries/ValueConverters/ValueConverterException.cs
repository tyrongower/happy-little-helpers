using System;

namespace HappyLittleHelpers.AdhocDataQueries.ValueConverters
{
    internal class ValueConverterException : Exception
    {
        public Type TargetType { get; }
        public Type SourceType { get; }
        public object Value { get; }

        public ValueConverterException(Type targetType, Type sourceType, object value, Exception exception) : base(
            $"Type {sourceType.Name}  with value {value} cannot be cast to {targetType.Name}", exception)
        {
            TargetType = targetType;
            SourceType = sourceType;
            Value = value;
        }
    }
}