using MySqlConnector;

namespace JToolbox.DataAccess.L2DB.MySql
{
    public static class MySqlBackupHelper
    {
        public static void CreateBackup(string connectionString, string targetFilePath)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(targetFilePath);

                        conn.Close();
                    }
                }
            }
        }
    }
}