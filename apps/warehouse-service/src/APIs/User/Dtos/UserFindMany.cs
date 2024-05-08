using Microsoft.AspNetCore.Mvc;
using WarehouseService.Infrastructure.Models;

namespace WarehouseService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserFindMany : FindManyInput<User, UserWhereInput> { }
