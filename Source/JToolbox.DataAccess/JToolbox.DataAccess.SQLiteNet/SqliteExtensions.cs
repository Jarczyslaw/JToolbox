using SQLite;
using System;
using System.Reflection;

namespace JToolbox.DataAccess.SQLiteNet
{
    public static class SqliteExtensions
    {
        public static string GetColumnName<T>(string propertyName) => GetColumnName(typeof(T), propertyName);

        public static string GetColumnName(Type tableType, string propertyName)
        {
            return tableType.GetProperty(propertyName).GetCustomAttribute<ColumnAttribute>()?.Name ?? propertyName;
        }

        public static string GetTableName<T>() => GetTableName(typeof(T));

        public static string GetTableName(Type tableType)
        {
            return tableType.GetCustomAttribute<TableAttribute>()?.Name ?? tableType.Name;
        }
    }
}