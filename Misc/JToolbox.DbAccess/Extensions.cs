using System.Data;

namespace JToolbox.DbAccess
{
    public static class Extensions
    {
        public static void AddParameter(this IDbCommand command, string name, DbType type, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = type;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        public static DataSet ExecuteDataSet(this IDbCommand command, IDbDataAdapter adapter)
        {
            var ds = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            return ds;
        }

        public static DataTable ExecuteDataTable(this IDbCommand command, IDbDataAdapter adapter)
        {
            return command.ExecuteDataSet(adapter).Tables[0];
        }
    }
}