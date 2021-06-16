using System;
using System.Collections.Generic;
using System.Data;
using HappyLittleHelpers.AdhocDataQueries.ValueConverters;

namespace HappyLittleHelpers.AdhocDataQueries
{
    public class AdhocQueryProviderConfiguration
    {

        private Dictionary<Type, IValueConverter> _valueConverters = new Dictionary<Type, IValueConverter>();
        private Dictionary<Type, DbType> _typeMappings = new Dictionary<Type, DbType>();

        /// <summary>
        /// The prefix for parameters in T-SQL (e.g "@" for MSSQL"
        /// </summary>
        public string ParameterPrefix { get; private set; }

        /// <summary>
        /// Overrides type mappings used when generating parameters. 
        /// </summary>
        /// <param name="sourceType">The type of parameter provided in the parameters anonymous type</param>
        /// <param name="dbType">The target <see cref="DbType">DbType</see> used when adding the parameter to <seealso cref="IDbCommand">Command object</seealso></param>
        public void OverrideDbTypeMapping(Type sourceType, DbType dbType) => _typeMappings[sourceType] = dbType;

        public void SetParameterPrefix(string prefix = "@") => ParameterPrefix = prefix;

        internal DbType GetDbTypeMapping(Type sourceType) =>
            _typeMappings.ContainsKey(sourceType) ? _typeMappings[sourceType] : DbType.String;

        public void AddConverter<T,TC>() where TC : IValueConverter,new()
        {
            _valueConverters[typeof(T)] = new TC();
        }

        public IValueConverter GetConverter(Type targetType)
        {
            if (_valueConverters.ContainsKey(targetType))
                return _valueConverters[targetType];
            
            if (targetType.IsEnum)
                return new EnumValueConverter();
            
            return new DefaultValueConverter();
        }
    }
}