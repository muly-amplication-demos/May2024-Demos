using System.ComponentModel.DataAnnotations;
using WarehouseService.APIs.Dtos;

namespace WarehouseService.APIs;

public interface IWarehousesService
{
    public Task<IEnumerable<Warehouse>> Warehouses();
    public Task<Warehouse> CreateWarehouse(WarehouseCreateInput input);
    public Task<IEnumerable<Manager>> Managers(string id);
    public Task UpdateWarehouse(string id, Warehouse dto);
    public Task DeleteWarehouse(string id);
}
