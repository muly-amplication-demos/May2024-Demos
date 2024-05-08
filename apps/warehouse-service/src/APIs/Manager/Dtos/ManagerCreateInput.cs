namespace WarehouseService.APIs.Dtos;

public class ManagerCreateInput
{
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public long? Rank { get; set; }
    public ICollection<WarehouseDto>? Warehouses { get; set; }
}
