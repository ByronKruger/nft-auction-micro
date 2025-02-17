using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NftApi.Data;
using NftApi.Entities;
using NftApi.Helpers;

namespace NftApi;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public UserRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        // return 
        var user = await _dataContext.Users
            .Include(u => u.Nfts)
            // .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserName == username);
        // if (user != null)
        // {
        //     _dataContext.Entry(user).State = EntityState.Detached;
        // }
        return user;
        // return await _context.Users
        //     .Include(p => p.Photos)
        //     .SingleOrDefaultAsync(x => x.UserName == username);
    }

    // public async Task<bool> SaveChanges()
    // {
    //     return await _dataContext.SaveChangesAsync() > 0;
    // }

    public async Task<List<User>> GetCharities()
    {
        var charities = await _dataContext.UserAndRoles
        .Where(ur => ur.Role.Name == "CHARITY")
        .Select(ur => ur.User)
        .ToListAsync();   
        // var charityRole = _dataContext.Roles.Where(r => r.Name == "CHARITY").SingleOrDefaultAsync();
        // return await _dataContext.Users.Where(u => u.)
        return charities;
    }

    public async Task<User> GetUserByIdIncludeNfts(int id)
    {
        return await _dataContext.Users
            .Where(u => u.Id == id)
            .Include(u => u.Nfts)
            // .Include(u => u.UserRoles)
            // .Include(u => u.Wallet)
            .FirstAsync();
            // .FindAsync(id);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _dataContext.Users
            .Where(u => u.Id == id)
            // .Include()
            .Include(u => u.UserRoles)
            .Include(u => u.Wallet)
            .FirstAsync();
            // .FindAsync(id);
    }

    public async Task<UserDto> GetUserDtoByIdAsync(int id)
    {
        return await _dataContext.Users
            .Where(u => u.Id == id)
            // .Include()
            .Include(u => u.UserRoles)
            // .Include(u => u.Wallet)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstAsync();
            // .FindAsync(id);
    }

    public async Task<UserDto> GetUserWithNftByUsernameAsync(string username)
    {
        return await _dataContext.Users
            // .Include(u => u.Nfts)
            // .Select(u => new UserDto 
            // {
            //     Id = u.Id,
            //     Nfts = u.Nfts
            // })
            // .Include(u => u.Nfts)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
            // .ProjectTo<UserDto>(u => u.);    
    }

    public async Task<User> GetCharityFromUsers() 
    {
        return await _dataContext.UserAndRoles
            .Where(ur => ur.Role.Name == "CHARITY")
            .Select(u => u.User).SingleOrDefaultAsync();
    }

    public async Task<Role> GetRoleByIdAsync(int id)
    {
        return await _dataContext.UserAndRoles
            .Where(ur => ur.RoleId == id)
            .Select(ur => ur.Role)
            // .Indlude(ur => ur.Role)
            .FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByRoleId(int id)
    {
        // return _dataContext.Users
        //     .Where(u => u.Role) UserAndRoles.
        var role =  await _dataContext.UserAndRoles
            .Where(ur => ur.RoleId == id)
            .FirstOrDefaultAsync();

        return await _dataContext.Users
            .Where(u => u.Id == role.UserId)
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync();
    }

    public async Task<PagedList<UserDto>> GetUsersAsync(string username, UserParams userParams)
    {
        var query = _dataContext.Users.AsQueryable();
        Console.WriteLine("::::::::::::::::::::::::::::::::::::");
        Console.WriteLine(userParams.OrderByUsername);
        Console.WriteLine("::::::::::::::::::::::::::::::::::::");

        if (userParams.OrderByUsername == "asc") 
        {
            query = query.OrderBy(u => u.UserName);
        } else 
        {
            query = query.OrderByDescending(u => u.UserName);
        }

        query = query.Where(u => u.UserName != username);

        return await PagedList<UserDto>.CreateAsync(
            query.ProjectTo<UserDto>(_mapper.ConfigurationProvider), 
            userParams.PageNumber, 
            userParams.PageSize);
            // .ToListAsync();
    }

    public IQueryable<RoleUser> GetRoleUsersQueryable()
    {
        return _dataContext.UserAndRoles.AsQueryable();
    }
}
