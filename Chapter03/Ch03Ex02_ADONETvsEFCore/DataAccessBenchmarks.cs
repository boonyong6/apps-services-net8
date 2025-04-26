using BenchmarkDotNet.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Northwind.EntityModels;
using System.Data;

public class DataAccessBenchmarks
{
    private readonly string _connectionString;

    public DataAccessBenchmarks()
    {
        SqlConnectionStringBuilder conStrBuilder = new()
        {
            DataSource = ".",
            InitialCatalog = "Northwind",
            IntegratedSecurity = true,
            TrustServerCertificate = true,
            ConnectTimeout = 3,
            MultipleActiveResultSets = true,
        };
        _connectionString = conStrBuilder.ConnectionString;
    }

    [Benchmark(Baseline = true)]
    public async Task<ICollection<Product>> GetAllProductsUsingAdoNetAsync()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        SqlCommand command = connection.CreateCommand();
        command.CommandText = """
            SELECT ProductId, ProductName, UnitPrice
            FROM dbo.Products;
            """;

        List<Product> products = new();

        using SqlDataReader reader = command.ExecuteReader();
        while (await reader.ReadAsync())
        {
            Product product = new()
            {
                ProductId = await reader.GetFieldValueAsync<int>("ProductId"),
                ProductName = await reader.GetFieldValueAsync<string>("ProductName"),
                UnitPrice = await reader.GetFieldValueAsync<decimal?>("UnitPrice"),
            };
            products.Add(product);
        }

        return products;
    }

    [Benchmark]
    public async Task<ICollection<Product>> GetAllProductsUsingEfCoreAsync()
    {
        using NorthwindContext db = new();

        List<Product> products = await db.Products.Select(p => new Product()
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            UnitPrice = p.UnitPrice,
        }).ToListAsync();

        return products;
    }
}
