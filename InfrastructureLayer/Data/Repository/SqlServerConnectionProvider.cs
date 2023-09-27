using DomainLayer.Interfaces.Repository;
using System.Data.SqlClient;

namespace InfrastructureLayer.Data.Repository
{
    public class SqlServerConnectionProvider : ISqlServerConnectionProvider
    {
        public SqlConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }
}