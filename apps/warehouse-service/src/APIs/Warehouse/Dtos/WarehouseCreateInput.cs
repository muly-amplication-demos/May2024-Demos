namespace WarehouseService.APIs.Dtos;

public class WarehouseCreateInput
{
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public ManagerDto ManagerId { get; set; }
}
