using DomainLayer.Interfaces.Repository;
using DomainLayer.Models.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace InfrastructureLayer.Data.Repository
{
    public class SqlServerConnectionProvider : ISqlServerConnectionProvider
    {

        private readonly IConfiguration _configuration;

        public SqlServerConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(BuilderConnectionString());
        }

        private string BuilderConnectionString()
        {
            var builder = new SqlConnectionStringBuilder();
            var dataSource = SetDataSource();

            builder.UserID = dataSource.UserId;
            builder.Password = dataSource.Password;
            builder.DataSource = dataSource.DataSource;
            builder.InitialCatalog = dataSource.InitialCatalog;
            builder.ConnectTimeout = dataSource.ConnectTimeout;
            builder.MaxPoolSize = dataSource.MaxPoolSize;
            builder.Pooling = dataSource.Pooling;
            builder.TrustServerCertificate = dataSource.TrustServerCertificate;
            builder.PersistSecurityInfo = dataSource.PersistSecurityInfo;
            builder.Encrypt = dataSource.Encrypt;
            return builder.ConnectionString;
        }

        private SqlServerDataSource SetDataSource()
        {
            return new SqlServerDataSource
            {
                UserId = _configuration.GetValue<string>("SqlServerSettings:User")!,
                Password = _configuration.GetValue<string>("SqlServerSettings:Password")!,
                DataSource = _configuration.GetValue<string>("SqlServerSettings:DataSource")!,
                InitialCatalog = _configuration.GetValue<string>("SqlServerSettings:InitialCatalog")!,
                ConnectTimeout = _configuration.GetValue<int>("SqlServerSettings:ConnectTimeout")!,
                MaxPoolSize = _configuration.GetValue<int>("SqlServerSettings:MaxPoolSize")!,
                Pooling = _configuration.GetValue<bool>("SqlServerSettings:Pooling")!,
                TrustServerCertificate = _configuration.GetValue<bool>("SqlServerSettings:TrustServerCertificate")!,
                PersistSecurityInfo = _configuration.GetValue<bool>("SqlServerSettings:PersistSecurityInfo")!,
                Encrypt = _configuration.GetValue<bool>("SqlServerSettings:Encrypt")!,
                CommandTimeout = _configuration.GetValue<int>("SqlServerSettings:CommandTimeout")!
            };
        }
    }
}