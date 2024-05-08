using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.APIs;

[ApiController]
public class WarehousesController : WarehousesControllerBase
{
    public WarehousesController(IWarehousesService service)
        : base(service) { }
}
