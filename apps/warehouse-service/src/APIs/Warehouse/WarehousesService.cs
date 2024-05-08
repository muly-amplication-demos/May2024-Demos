using WarehouseService.Infrastructure;

namespace WarehouseService.APIs;

public class WarehousesService : WarehousesServiceBase
{
    public WarehousesService(WarehouseServiceContext context)
        : base(context) { }
}
