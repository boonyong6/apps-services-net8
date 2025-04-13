namespace Northwind.Models;

public class Product
{
    public required int ProductId { get; init; }
    public string? ProductName { get; init; }
    public decimal? UnitPrice { get; init; }
}
