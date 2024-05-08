using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseService.Infrastructure.Models;

[Table("Warehouses")]
public class Warehouse
{
    [Key, Required]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string ManagerId { get; set; }

    [ForeignKey(nameof(ManagerId))]
    public Manager? Manager { get; set; }
}
