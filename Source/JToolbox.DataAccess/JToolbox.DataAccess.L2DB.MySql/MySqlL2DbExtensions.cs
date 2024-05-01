using LinqToDB.Data;
using System.Linq;

namespace JToolbox.DataAccess.L2DB.MySql
{
    public static class MySqlL2DbExtensions
    {
        public static void AddForeignKey(
            this DataConnection db,
            string tableName,
            string foreignKeyColumnName,
            string primaryTableName,
            string primaryColumnName,
            string foreignKeyName)
        {
            string query = $@"ALTER TABLE {tableName}
                ADD CONSTRAINT {foreignKeyName}
                FOREIGN KEY ({foreignKeyColumnName})
                REFERENCES {primaryTableName}({primaryColumnName})";

            db.Execute(query);
        }

        public static void CreateForeignKeyIfNotExists<TForeign, TPrimary>(
            this DataConnection db,
            string foreignKeyColumnName,
            string primaryColumnName,
            string foreignKeyName = null)
        {
            foreignKeyName = foreignKeyName ?? GetForeignKeyName<TForeign, TPrimary>();

            db.CreateForeignKeyIfNotExists(
                typeof(TForeign).GetTableName(),
                typeof(TForeign).GetColumnName(foreignKeyColumnName),
                typeof(TPrimary).GetTableName(),
                typeof(TPrimary).GetColumnName(primaryColumnName),
                foreignKeyName);
        }

        public static void CreateForeignKeyIfNotExists(
            this DataConnection db,
            string tableName,
            string foreignKeyColumnName,
            string primaryTableName,
            string primaryColumnName,
            string foreignKeyName)
        {
            bool keyExists = IfForeignKeyExists(db, tableName, foreignKeyName);

            if (!keyExists)
            {
                AddForeignKey(
                    db,
                    tableName,
                    foreignKeyColumnName,
                    primaryTableName,
                    primaryColumnName,
                    foreignKeyName);
            }
        }

        public static string GetForeignKeyName<TForeign, TPrimary>()
        {
            string foreignTable = typeof(TForeign).GetTableName();
            string primaryTable = typeof(TPrimary).GetTableName();

            return $"FK_{foreignTable}_{primaryTable}";
        }

        public static bool IfForeignKeyExists(
           this DataConnection db,
           string tableName,
           string foreignKeyName)
        {
            string query = $@"SELECT CONSTRAINT_NAME
                FROM information_schema.KEY_COLUMN_USAGE
                WHERE TABLE_NAME = '{tableName}' AND CONSTRAINT_NAME = '{foreignKeyName}'";

            return db.Query<string>(query).Any();
        }

        public static void SetForeignKeyChecks(this DataConnection db, bool check)
        {
            db.Execute($"SET FOREIGN_KEY_CHECKS = {(check ? "1" : "0")};");
        }

        public static void Truncate<T>(this DataConnection db)
        {
            db.Execute($"TRUNCATE TABLE {typeof(T).GetTableName()}");
        }
    }
}