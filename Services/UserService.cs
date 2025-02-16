using NftApi.Entities;
using NftApi.Interfaces;
using NftApi.Helpers;

namespace NftApi;

public class UserService : IUserService
{
    // private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public UserService(
        // IUserRepository userRepository
        IUnitOfWork uow
    )
    {
        // _userRepository = userRepository;
        _uow = uow;
    }

    public Task<List<User>> GetCharities()
    {
        return _uow.UserRepo.GetCharities();
    }

    // public async Task<List<Role>> getUserWithRoles()
    // {

    // }

    public async Task<PagedList<UserDto>> GetUsersAsync(User user, UserParams userParams)
    {
        Console.WriteLine("user");
        Console.WriteLine(user);
        Console.WriteLine("user.Id");
        Console.WriteLine(user.Id);
        Console.WriteLine("userParams");
        Console.WriteLine(userParams);
        return await _uow.UserRepo.GetUsersAsync(user.UserName, userParams);
    }
}
