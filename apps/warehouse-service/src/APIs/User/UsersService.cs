using WarehouseService.Infrastructure;

namespace WarehouseService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(WarehouseServiceContext context)
        : base(context) { }
}
