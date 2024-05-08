using Microsoft.EntityFrameworkCore;
using WarehouseService.Infrastructure.Models;

namespace WarehouseService.Infrastructure;

public class WarehouseServiceContext : DbContext
{
    public WarehouseServiceContext(DbContextOptions<WarehouseServiceContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Manager> Managers { get; set; } = null!;
}
