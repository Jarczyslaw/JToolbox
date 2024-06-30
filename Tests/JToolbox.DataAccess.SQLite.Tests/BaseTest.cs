using JToolbox.DataAccess.SQLite.Tests.DataAccess;
using JToolbox.DataAccess.SQLiteNet;

namespace JToolbox.DataAccess.SQLite.Tests
{
    public class BaseTest
    {
        protected static string DatabasePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testDb.db");

        protected static string Password => "test"; // null if database should not be decrypted

        public static void InitializeDatabase()
        {
            if (File.Exists(DatabasePath))
            {
                File.Delete(DatabasePath);
            }

            DataAccessService service = CreateDataAccessService();

            Task initTask = service.Init(DatabasePath, Password, false);
            initTask.Wait();
        }

        protected static DataAccessService CreateDataAccessService()
        {
            Initializer initializer = new();
            DataAccessService service = new(initializer)
            {
                CacheConnection = false,
                UseMigrationLockFile = false
            };
            return service;
        }
    }
}