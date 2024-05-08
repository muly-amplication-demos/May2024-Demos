using WarehouseService.APIs.Dtos;
using WarehouseService.Infrastructure.Models;

namespace WarehouseService.APIs.Extensions;

public static class ManagersExtensions
{
    public static ManagerDto ToDto(this Manager model)
    {
        return new ManagerDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            Rank = model.Rank,
            Warehouses = model.Warehouses.Select(x => x.ToDto()).ToList(),
        };
    }
}
