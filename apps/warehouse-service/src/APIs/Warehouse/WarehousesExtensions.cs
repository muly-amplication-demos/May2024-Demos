using WarehouseService.APIs.Dtos;
using WarehouseService.Infrastructure.Models;

namespace WarehouseService.APIs.Extensions;

public static class WarehousesExtensions
{
    public static WarehouseDto ToDto(this Warehouse model)
    {
        return new WarehouseDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            Address = model.Address,
            Managers = model.Managers.Select(x => x.ToDto()).ToList(),
        };
    }
}
