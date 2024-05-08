using Microsoft.AspNetCore.Mvc;
using WarehouseService.Infrastructure.Models;

namespace WarehouseService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class WarehouseFindMany : FindManyInput<Warehouse, WarehouseWhereInput> { }
