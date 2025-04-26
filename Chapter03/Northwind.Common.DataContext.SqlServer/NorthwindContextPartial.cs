using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

// [Calculated property] 4. Create an instance of the interceptor and register it.
public partial class NorthwindContext
{
    private static readonly SetLastRefreshedInterceptor setLastRefreshedInterceptor = new(); // <--

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            SqlConnectionStringBuilder conStrBuilder = new()
            {
                DataSource = ".",
                //// If using Azure SQL Edge.
                //DataSource = "tcp:127.0.0.1,1433",
                InitialCatalog = "Northwind",
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                // Because we want to fail fast. Default is 15 seconds.
                ConnectTimeout = 3,
                // If using Windows Integrated authentication.
                IntegratedSecurity = true,
                //// If using SQL Server authentication.
                //UserID = Environment.GetEnvironmentVariable("MSSQL_USR"),
                //Password = Environment.GetEnvironmentVariable("MSSQL_PWD"),
            };

            optionsBuilder.UseSqlServer(conStrBuilder.ConnectionString);
        }

        optionsBuilder.AddInterceptors(setLastRefreshedInterceptor); // <--
    }
}
