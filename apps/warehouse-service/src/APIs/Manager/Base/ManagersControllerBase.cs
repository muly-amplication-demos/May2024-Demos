using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WarehouseService.APIs.Dtos;
using WarehouseService.APIs.Errors;

namespace WarehouseService.APIs;

[Route("api/[controller]")]
[ApiController]
public class ManagersControllerBase : ControllerBase
{
    protected readonly IManagersService _service;

    public ManagersControllerBase(IManagersService service)
    {
        _service = service;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManager(string id)
    {
        try
        {
            await _service.DeleteManager(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<ManagerDto>> CreateManager(ManagerCreateInput input)
    {
        var dto = await _service.CreateManager(input);
        return CreatedAtAction(nameof(Manager), new { id = dto.Id }, dto);
    }

    [HttpDelete("{id}/warehouses")]
    public async Task<IActionResult> DisconnectManager(string id, [Required] string WarehouseId)
    {
        try
        {
            await _service.DisconnectWarehouse(id, ManagerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManagerDto>>> Managers()
    {
        return Ok(await _service.managers());
    }

    [HttpGet("{id}/warehouses")]
    public async Task<IActionResult> Warehouses(string id)
    {
        try
        {
            return Ok(await _service.Warehouses(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateManager(string id, ManagerDto managerDto)
    {
        if (id != managerDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateManager(id, managerDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id}/warehouses")]
    public async Task<IActionResult> ConnectManager(string id, [Required] string WarehouseId)
    {
        try
        {
            await _service.ConnectWarehouse(id, ManagerId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}/warehouses")]
    public async Task<IActionResult> UpdateWarehouse(
        [FromRoute] ManagerIdDto idDto,
        [FromBody] WarehouseIdDto[] warehouseIds
    )
    {
        try
        {
            await _service.UpdateWarehouse(id, WarehouseId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
