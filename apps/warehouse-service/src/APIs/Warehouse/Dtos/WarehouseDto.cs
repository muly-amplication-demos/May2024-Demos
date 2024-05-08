namespace WarehouseService.APIs.Dtos;

public class WarehouseDto
{
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public ManagerDto ManagerId { get; set; }
}
