using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseService.Infrastructure.Models;

[Table("Managers")]
public class Manager
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public int? Rank { get; set; }

    public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
