namespace Northwind.EntityModels;

// [Calculated property] 1. Define an interface with the extra property.
public interface IHasLastRefreshed
{
    DateTimeOffset LastRefreshed { get; set; }
}
