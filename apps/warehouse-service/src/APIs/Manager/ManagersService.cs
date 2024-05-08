using WarehouseService.Infrastructure;

namespace WarehouseService.APIs;

public class ManagersService : ManagersServiceBase
{
    public ManagersService(WarehouseServiceContext context)
        : base(context) { }
}
