using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using WarehouseService.APIs.Dtos;
using WarehouseService.APIs.Errors;
using WarehouseService.APIs.Extensions;
using WarehouseService.Infrastructure;
using WarehouseService.Infrastructure.Models;

namespace WarehouseService.APIs;

public abstract class ManagersServiceBase : IManagersService
{
    protected readonly WarehouseServiceContext _context;

    public ManagersServiceBase(WarehouseServiceContext context)
    {
        _context = context;
    }

    private bool ManagerExists(long id)
    {
        return _context.Managers.Any(e => e.Id == id);
    }

    public async Task DeleteManager(string id)
    {
        var manager = await _context.managers.FindAsync(id);

        if (manager == null)
        {
            throw new NotFoundException();
        }

        _context.managers.Remove(manager);
        await _context.SaveChangesAsync();
    }

    public async Task<ManagerDto> CreateManager(ManagerCreateInput inputDto)
    {
        var model = new Manager { Id = inputDto.Id, Name = inputDto.Name, };
        _context.managers.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Manager>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task DisconnectWarehouse(string id, [Required] string warehouseId)
    {
        var manager = await _context.managers.FindAsync(id);
        if (manager == null)
        {
            throw new NotFoundException();
        }

        var warehouse = await _context.warehouses.FindAsync(warehouseId);
        if (warehouse == null)
        {
            throw new NotFoundException();
        }

        manager.warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ManagerDto>> managers(ManagerFindMany findManyArgs)
    {
        var managers = await _context
            .managers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return managers.ConvertAll(manager => manager.ToDto());
    }

    public async Task<IEnumerable<WarehouseDto>> Warehouses(string id)
    {
        var manager = await _context.managers.FindAsync(id);
        if (manager == null)
        {
            throw new NotFoundException();
        }

        return manager.Warehouses.Select(warehouse => warehouse.ToDto()).ToList();
    }

    public async Task UpdateManager(string id, ManagerDto managerDto)
    {
        var manager = new Manager { Id = managerDto.Id, Name = managerDto.Name, };

        _context.Entry(manager).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ManagerExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task ConnectWarehouse(string id, [Required] string warehouseId)
    {
        var manager = await _context.managers.FindAsync(id);
        if (manager == null)
        {
            throw new NotFoundException();
        }

        var warehouse = await _context.warehouses.FindAsync(warehouseId);
        if (warehouse == null)
        {
            throw new NotFoundException();
        }

        manager.warehouses.Add(warehouse);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateWarehouses(ManagerIdDto idDto, WarehouseIdDto[] warehousesId)
    {
        var manager = await _context
            .managers.Include(x => x.Warehouses)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (manager == null)
        {
            throw new NotFoundException();
        }

        var warehouses = await _context
            .Warehouses.Where(t => warehousesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (warehouses.Count == 0)
        {
            throw new NotFoundException();
        }

        manager.Warehouses = warehouses;
        await _context.SaveChangesAsync();
    }
}
