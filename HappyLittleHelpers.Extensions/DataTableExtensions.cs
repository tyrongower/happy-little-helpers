using System;
using System.Collections.Generic;
using System.Data;

namespace HappyLittleHelpers.Extensions
{
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            var type = typeof(T);

            var props = type.GetProperties();
            var fields = type.GetFields();
            var table = new DataTable();

            foreach (var field in props)
            {
                var propertyType = field.PropertyType;

                var nullable = false;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = propertyType.GetGenericArguments()[0];
                    nullable = true;
                }

                var col = table.Columns.Add(field.Name, propertyType);
                col.AllowDBNull = nullable;
            }


            foreach (var field in fields)

            {
                var propertyType = field.FieldType;

                var nullable = false;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = propertyType.GetGenericArguments()[0];
                    nullable = true;
                }

                var col = table.Columns.Add(field.Name, propertyType);
                col.AllowDBNull = nullable;
            }

            foreach (var item in list)
            {
                var row = table.NewRow();
                foreach (var propertyInfo in props)
                    row[propertyInfo.Name] = propertyInfo.GetValue(item)?? DBNull.Value;

                foreach (var field in fields)
                    row[field.Name] = field.GetValue(item)?? DBNull.Value;

                table.Rows.Add(row);
            }

            return table;
        }
    }
}