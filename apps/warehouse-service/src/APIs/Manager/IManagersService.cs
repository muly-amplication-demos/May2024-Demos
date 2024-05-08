using System.ComponentModel.DataAnnotations;
using WarehouseService.APIs.Dtos;

namespace WarehouseService.APIs;

public interface IManagersService
{
    public Task DeleteManager(string id);
    public Task<Manager> CreateManager(ManagerCreateInput input);
    public Task DisconnectWarehouse(string id, [Required] string WarehouseId);
    public Task<IEnumerable<Manager>> Managers();
    public Task<IEnumerable<Warehouse>> Warehouses(string id);
    public Task UpdateManager(string id, Manager dto);
    public Task ConnectWarehouse(string id, [Required] string WarehouseId);
    public Task UpdateWarehouses(ManagerIdDto idDto, WarehouseIdDto[] WarehousesId);
}
