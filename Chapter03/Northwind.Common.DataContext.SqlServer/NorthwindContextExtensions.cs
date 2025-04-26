using Microsoft.Data.SqlClient; // SqlConnectionStringBuilder
using Microsoft.EntityFrameworkCore; // UseSqlServer
using Microsoft.EntityFrameworkCore.Diagnostics; // RelationalEventId
using Microsoft.Extensions.DependencyInjection; // IServiceCollection

namespace Northwind.EntityModels;

public static class NorthwindContextExtensions
{
    /// <summary>
    /// Adds NorthwindContext to the specified IServiceCollection. Uses the 
    /// SqlServer database provider.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">Set to override the default.</param>
    /// <returns>An IServiceCollection that can be used to add more services.</returns>
    public static IServiceCollection AddNorthwindContext(
        this IServiceCollection services, string? connectionString = null)
    {
        if (connectionString == null)
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

            connectionString = conStrBuilder.ConnectionString;
        }

        services.AddDbContext<NorthwindContext>(dbCtxOptBuilder =>
            {
                dbCtxOptBuilder.UseSqlServer(connectionString);
                dbCtxOptBuilder.LogTo(Console.WriteLine, 
                    [RelationalEventId.CommandExecuting]);
            },
            // Register with a transient lifetime to avoid concurrency issues 
            // with Blazor Server projects.
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);

        return services;
    }
}
