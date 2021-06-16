using System;

namespace HappyLittleHelpers.AdhocDataQueries
{
    internal class MissingColumnException : Exception
    {
        public string PropertyName { get; }

        public MissingColumnException(string propertyName): base($"Query does not contain column {propertyName}")
        {
            PropertyName = propertyName;
        }
    }
}