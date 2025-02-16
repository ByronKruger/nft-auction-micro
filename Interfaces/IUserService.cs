using NftApi.Entities;
using NftApi.Helpers;

namespace NftApi;

public interface IUserService
{
    Task<List<User>> GetCharities();
    Task<PagedList<UserDto>> GetUsersAsync(User user, UserParams userParams);
}
