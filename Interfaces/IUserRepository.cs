using NftApi.Data;
using NftApi.Entities;
using NftApi.Helpers;

namespace NftApi;

public interface IUserRepository
{
    // Task<bool> SaveChanges();
    Task<User> GetUserByUsernameAsync(string username);
    Task<UserDto> GetUserWithNftByUsernameAsync(string username);
    Task<User> GetUserByIdAsync(int id);
    Task<UserDto> GetUserDtoByIdAsync(int id);
    Task<List<User>> GetCharities();
    Task<User> GetUserByRoleId(int id);
    Task<Role> GetRoleByIdAsync(int id);
    IQueryable<RoleUser> GetRoleUsersQueryable();
    Task<User> GetUserByIdIncludeNfts(int id);
    Task<PagedList<UserDto>> GetUsersAsync(string username, UserParams userParams);
}
