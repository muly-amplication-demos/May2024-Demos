using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.APIs;

[ApiController]
public class ManagersController : ManagersControllerBase
{
    public ManagersController(IManagersService service)
        : base(service) { }
}
