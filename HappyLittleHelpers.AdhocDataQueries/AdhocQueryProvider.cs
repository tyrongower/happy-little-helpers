using System;
using System.Collections.Generic;
using System.Data;
using HappyLittleHelpers.AdhocDataQueries.ValueConverters;

namespace HappyLittleHelpers.AdhocDataQueries
{
    public abstract class AdhocQueryProvider : IAdhocQueryProvider
    {
        private readonly string _connectionString;
        internal IConnectionProvider ConnectionProvider;
        private AdhocQueryProviderConfiguration Configuration { get; }


        internal AdhocQueryProvider(string connectionString)
        {
            _connectionString = connectionString;
            Configuration = new AdhocQueryProviderConfiguration();
            ConfigureInternal();
        }

        private void ConfigureInternal()
        {
            Configuration.OverrideDbTypeMapping(typeof(int), DbType.Int32);

            Configure(Configuration);

        }

        public abstract void Configure(AdhocQueryProviderConfiguration configuration);

        public IEnumerable<T> RunQuery<T, TP>(string query, T template, TP parameters = default)
        {
            using var conn = ConnectionProvider.GetConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();

            using var command = ConnectionProvider.GetCommand();
            command.Connection = conn;
            command.CommandText = query;

            if (parameters != null)
            {
                var props = parameters.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var targetType = Configuration.GetDbTypeMapping(prop.PropertyType);
                    var parameter = command.CreateParameter();
                    parameter.Value = prop.GetValue(parameters);
                    parameter.DbType = targetType;
                    parameter.ParameterName = $"{Configuration.ParameterPrefix}{prop.Name}";
                    command.Parameters.Add(parameter);
                }
            }

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var resultColumns = new Dictionary<string, int>();
                for (var i = 0; i < reader.FieldCount; i++)
                    resultColumns.Add(reader.GetName(i), i);

                var instance = Activator.CreateInstance<T>();
                foreach (var property in instance.GetType().GetProperties())
                {
                    var propertyType = property.PropertyType;
                    
                    if (!resultColumns.ContainsKey(property.Name))
                        throw new MissingColumnException(property.Name);

                    var resultValue = reader[property.Name];
                    try
                    {
                        var converter = Configuration.GetConverter(propertyType);
                        var convertedValue = converter.Convert(resultValue, propertyType);
                        
                        property.SetValue(instance, convertedValue);
                    }
                    catch (Exception e)
                    {
                        //todo allow customer converters 
                        throw new ValueConverterException(propertyType, resultValue.GetType(), resultValue,e);
                    }

                }

                yield return instance;

            }
        }
    }
}