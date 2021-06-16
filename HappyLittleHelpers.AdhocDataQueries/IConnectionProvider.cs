using System.Data;

namespace HappyLittleHelpers.AdhocDataQueries
{
    public interface IConnectionProvider
    {
        IDbConnection GetConnection();
        IDbCommand GetCommand();
    }
}