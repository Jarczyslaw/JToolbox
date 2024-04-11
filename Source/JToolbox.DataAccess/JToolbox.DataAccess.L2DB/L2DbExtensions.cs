using LinqToDB.Mapping;
using System;
using System.Reflection;

namespace JToolbox.DataAccess.L2DB
{
    public static class L2DbExtensions
    {
        public static string GetColumnName<T>(this string propertyName) => GetColumnName(typeof(T), propertyName);

        public static string GetColumnName(this Type tableType, string propertyName)
        {
            return tableType.GetProperty(propertyName).GetCustomAttribute<ColumnAttribute>()?.Name ?? propertyName;
        }

        public static string GetTableName<T>() => GetTableName(typeof(T));

        public static string GetTableName(this Type tableType)
        {
            return tableType.GetCustomAttribute<TableAttribute>()?.Name ?? tableType.Name;
        }
    }
}