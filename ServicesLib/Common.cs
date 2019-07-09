using System;
using System.Data;

namespace ServicesLib
{
    public class Common
    {
        public static DataTable ObjectToDataTable<T>(string tableName) where T : class, new()
        {
            var t = typeof(T);
            var pis = t.GetProperties();

            DataTable table = new DataTable(tableName);

            foreach (var pi in pis)
            {
                var colName = pi.Name;
                table.Columns.Add(new DataColumn(colName,pi.PropertyType));
            }

            return table;
        }
    }
}