using System.Data;
using System.Data.SqlClient;

namespace HappyLittleHelpers.AdhocDataQueries.SQL
{
    internal sealed class SQLConnectionProvider : IConnectionProvider
    {
        public IDbConnection GetConnection() => new SqlConnection();

        public IDbCommand GetCommand() => new SqlCommand();
    }
}