namespace Northwind.Models;

public class Supplier
{
    public required int SupplierId { get; init; }
    public string? CompanyName { get; init; }
    public string? City { get; init; }
    public string? Country { get; init; }
}
