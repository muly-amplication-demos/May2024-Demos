using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using WarehouseService.APIs.Dtos;
using WarehouseService.APIs.Errors;
using WarehouseService.APIs.Extensions;
using WarehouseService.Infrastructure;
using WarehouseService.Infrastructure.Models;

namespace WarehouseService.APIs;

public abstract class WarehousesServiceBase : IWarehousesService
{
    protected readonly WarehouseServiceContext _context;

    public WarehousesServiceBase(WarehouseServiceContext context)
    {
        _context = context;
    }

    private bool WarehouseExists(long id)
    {
        return _context.Warehouses.Any(e => e.Id == id);
    }

    public async Task<IEnumerable<WarehouseDto>> warehouses(WarehouseFindMany findManyArgs)
    {
        var warehouses = await _context
            .warehouses.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return warehouses.ConvertAll(warehouse => warehouse.ToDto());
    }

    public async Task<WarehouseDto> CreateWarehouse(WarehouseCreateInput inputDto)
    {
        var model = new Warehouse { Id = inputDto.Id, Name = inputDto.Name, };
        _context.warehouses.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Warehouse>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task UpdateWarehouse(string id, WarehouseDto warehouseDto)
    {
        var warehouse = new Warehouse { Id = warehouseDto.Id, Name = warehouseDto.Name, };

        _context.Entry(warehouse).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WarehouseExists(id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task DeleteWarehouse(string id)
    {
        var warehouse = await _context.warehouses.FindAsync(id);

        if (warehouse == null)
        {
            throw new NotFoundException();
        }

        _context.warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
    }
}
