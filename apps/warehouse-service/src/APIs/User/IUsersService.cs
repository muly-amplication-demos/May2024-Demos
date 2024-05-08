using System.ComponentModel.DataAnnotations;
using WarehouseService.APIs.Dtos;

namespace WarehouseService.APIs;

public interface IUsersService
{
    public Task DeleteUser(string id);
    public Task UpdateUser(string id, User dto);
    public Task<IEnumerable<User>> Users();
    public Task<User> CreateUser(UserCreateInput input);
}
