using System.ComponentModel.DataAnnotations.Schema; // [NotMapped]

namespace Northwind.EntityModels;

// [Calculated property] 2. Implement the interface for the extra property.
public partial class Employee : IHasLastRefreshed
{
    [NotMapped]
    public DateTimeOffset LastRefreshed { get; set; }
}
