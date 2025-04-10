namespace Northwind.Models;

public class Product
{
    public required int Id { get; init; }
    public string? Name { get; init; }
    public decimal? UnitPrice { get; init; }
}
