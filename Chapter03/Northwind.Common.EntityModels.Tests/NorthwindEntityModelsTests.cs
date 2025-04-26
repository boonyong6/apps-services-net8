using Northwind.EntityModels;

namespace Northwind.Common.EntityModels.Tests;

public class NorthwindEntityModelsTests
{
    [Fact]
    public void CanConnectIsTrue()
    {
        // Arrange
        using NorthwindContext db = new();

        // Act
        bool canConnect = db.Database.CanConnect();

        // Assert
        Assert.True(canConnect);
    }

    [Fact]
    public void ProviderIsSqlServer()
    {
        using NorthwindContext db = new();

        string? provider = db.Database.ProviderName;

        Assert.Equal("Microsoft.EntityFrameworkCore.SqlServer", provider);
    }

    [Fact]
    public void ProductId1IsChai()
    {
        using NorthwindContext db = new();

        Product product1 = db.Products.Single(p => p.ProductId == 1);

        Assert.Equal("Chai", product1.ProductName);
    }

    [Fact]
    public void EmployeeHasLastRefreshedIn10sWindow()
    {
        using NorthwindContext db = new();
        DateTimeOffset now = DateTimeOffset.Now;

        Employee employee1 = db.Employees.Single(e => e.EmployeeId == 1);

        Assert.InRange(actual: employee1.LastRefreshed, 
            low: now.Subtract(TimeSpan.FromSeconds(5)), 
            high: now.AddSeconds(5));
    }
}