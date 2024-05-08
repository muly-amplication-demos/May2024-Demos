using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WarehouseService.APIs.Dtos;
using WarehouseService.APIs.Errors;

namespace WarehouseService.APIs;

[Route("api/[controller]")]
[ApiController]
public class WarehousesControllerBase : ControllerBase
{
    protected readonly IWarehousesService _service;

    public WarehousesControllerBase(IWarehousesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WarehouseDto>>> Warehouses()
    {
        return Ok(await _service.warehouses());
    }

    [HttpPost]
    public async Task<ActionResult<WarehouseDto>> CreateWarehouse(WarehouseCreateInput input)
    {
        var dto = await _service.CreateWarehouse(input);
        return CreatedAtAction(nameof(Warehouse), new { id = dto.Id }, dto);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateWarehouse(string id, WarehouseDto warehouseDto)
    {
        if (id != warehouseDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateWarehouse(id, warehouseDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouse(string id)
    {
        try
        {
            await _service.DeleteWarehouse(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
