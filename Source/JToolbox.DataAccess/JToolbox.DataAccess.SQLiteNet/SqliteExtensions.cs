using SQLite;
using System.Reflection;

namespace JToolbox.DataAccess.SQLiteNet
{
    public static class SqliteExtensions
    {
        public static string GetColumnName<T>(string propertyName)
        {
            var tableType = typeof(T);
            return tableType.GetProperty(propertyName).GetCustomAttribute<ColumnAttribute>()?.Name ?? propertyName;
        }

        public static string GetTableName<T>()
        {
            var tableType = typeof(T);
            return tableType.GetCustomAttribute<TableAttribute>()?.Name ?? tableType.Name;
        }
    }
}