using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WarehouseService.APIs.Dtos;
using WarehouseService.APIs.Errors;

namespace WarehouseService.APIs;

[Route("api/[controller]")]
[ApiController]
public class UsersControllerBase : ControllerBase
{
    protected readonly IUsersService _service;

    public UsersControllerBase(IUsersService service)
    {
        _service = service;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            await _service.DeleteUser(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(string id, UserDto userDto)
    {
        if (id != userDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateUser(id, userDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Users()
    {
        return Ok(await _service.users());
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserCreateInput input)
    {
        var dto = await _service.CreateUser(input);
        return CreatedAtAction(nameof(User), new { id = dto.Id }, dto);
    }
}
