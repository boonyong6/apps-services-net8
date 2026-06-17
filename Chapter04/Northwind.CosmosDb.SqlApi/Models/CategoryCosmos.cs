namespace Northwind.CosmosDb.Items;

// Must not follow usual casing conventions because we can't dynamically
//   manipulate the serialization and the resulting JSON must use camel case.
public class CategoryCosmos
{
    public int categoryId { get; set; }
    public string categoryName { get; set; } = null!;
    public string? description { get; set; }
}
