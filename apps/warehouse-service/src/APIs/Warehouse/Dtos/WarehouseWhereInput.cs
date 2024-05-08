namespace WarehouseService.APIs.Dtos;

public class WarehouseWhereInput
{
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public ManagerDto ManagerId { get; set; }
}
