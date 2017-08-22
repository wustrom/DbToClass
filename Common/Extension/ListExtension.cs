using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Common.Extension
{
    public static class ListExtension
    {
        /// <summary>
        /// 转换List成为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">传入的List</param>
        public static DataTable ToDataTable<T>(this List<T> list) where T : class, new()
        {
            DataTable table = new DataTable();
            T t = new T();
            Type type = t.GetType();
            foreach (var proInfo in type.GetProperties())
            {
                DataColumn column = new DataColumn();
                column.ColumnName = proInfo.Name;
                table.Columns.Add(column);
                
            }
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (var proInfo in type.GetProperties())
                {
                    var proValue = proInfo.GetValue(item, null);
                    row[proInfo.Name] = proValue;
                }
                table.Rows.Add(row);
               
            }
            return table;
        }
    }
}
