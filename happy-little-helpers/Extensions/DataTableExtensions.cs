using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace HappyLittleHelpers.Extensions
{
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            var props =  typeof(T).GetProperties();
            var table = new DataTable();

            foreach (var prop in props)
                table.Columns.Add(prop.Name, prop.PropertyType);

            foreach (var item in list)
            {
                var row = table.NewRow();
                foreach (var propertyInfo in props)
                    row[propertyInfo.Name] = propertyInfo.GetValue(item);
                
                table.Rows.Add(row);
            }

            return table;
        }
    }
}