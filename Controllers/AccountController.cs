using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NftApi.DTOs;
using NftApi.Entities;
using NftApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using NftApi.Extensions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using NftApi.Helpers;

namespace NftApi;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    // private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, 
        ITokenService tokenService, IUserService userService, 
        IUnitOfWork uow,
        // IUserRepository userRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _userService = userService;
        _uow = uow;
        // _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto>> RegisterUser(RegisterUserDto userDto) 
    {
        // var a = User.Claims.FirstOrDefault(c => c.Issuer == "s").Value;  
        // var b = User
        // var user = _mapper.Map<User>(userDto);
        Console.WriteLine("userDto.username");
        Console.WriteLine(userDto.username);
        Console.WriteLine("userDto.password");
        Console.WriteLine(userDto.password);
        Console.WriteLine("userDto.type");
        Console.WriteLine(userDto.type);
        Console.WriteLine("1");

        if (await DoesUserExist(userDto.username)) return BadRequest("Username already exists");
        Console.WriteLine("2");
        
        // using var hmac = new HMACSHA512();
        // var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
        var registerUser = new User
        {
            UserName = userDto.username,//.ToLower(),
            // PasswordHash = passwordHash,
            // PasswordSalt = hmac.Key,
        };
        // _dataContext.Users.Add(registerUser);
        // await _dataContext.SaveChangesAsync();

        await _roleManager.CreateAsync(new Role {Name = "BIDDER"});
        await _roleManager.CreateAsync(new Role {Name = "ADMIN"});
        await _roleManager.CreateAsync(new Role {Name = "CHARITY"});

        Console.WriteLine("3");
        var registerResult = await _userManager.CreateAsync(registerUser, userDto.password);
        if (!registerResult.Succeeded) return BadRequest(registerResult.Errors);
        Console.WriteLine("4");

        var roleResult = await _userManager.AddToRoleAsync(registerUser, userDto.type);
        if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

        return new LoggedInDto
        {
            Username = userDto.username,
            Token = await _tokenService.CreateToken(registerUser)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users
            .Include(u => u.Wallet)
            .FirstOrDefaultAsync(u => u.UserName == loginDto.Username);

        if (user == null) return Unauthorized("Invalid username");

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!result) return Unauthorized("Invalid password");

        // using var hmac = new HMACSHA512(user.PasswordSalt);
        // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        // var savedPasswordHash = user.PasswordHash;

        // for (var index = 0; index < savedPasswordHash.Length; index++)
        // {
        //     if (computedHash[index] != savedPasswordHash[index]) return Unauthorized("Invalid password");
        // }

        
        return new LoggedInDto 
        {
            Username = loginDto.Username,
            Token = await _tokenService.CreateToken(user)
        };
    }

    private async Task<bool> DoesUserExist(string username)
    {
        return await _userManager.Users.AnyAsync(u => u.UserName == username);

        // var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        // if (user == null) return false;
        // return true;
    }

    [HttpGet("getCharities")]
    public async Task<ActionResult<List<User>>> getCharities()
    {
        var charities = _userService.GetCharities();
        return Ok(charities);
    }

    [HttpGet("getUsers")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<List<UserDto>> GetUsers([FromQuery]UserParams userParams)
    {
        Console.WriteLine("::::::::::::::::::::::::::::::::::::");
        Console.WriteLine(userParams);
        Console.WriteLine(userParams.OrderByUsername);
        Console.WriteLine(userParams.PageSize);
        Console.WriteLine("::::::::::::::::::::::::::::::::::::");
        var u = User;
        var user = await _uow.UserRepo.GetUserByUsernameAsync(User.GetUsername());
        // Console.WriteLine("Response");
        // Console.WriteLine(Response.StatusCode);
        // Response.AddPaginationHeader<UserDto>(user);
        var users = await _userService.GetUsersAsync(user, userParams);

        // var paginationHeader = new PaginationHeader(users.CurrentPage, users.PageSize,
        //     users.TotalCount, users.TotalPages);

        // var jsonOptions = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        // Response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));
        // Response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        Response.AddPaginationHeader(users);
        return _mapper.Map<List<UserDto>>(users);
    }

    [HttpGet("getRoles")]
    public async Task<List<Role>> GetRoles()
    {
        return _roleManager.Roles.ToList();
    }
}
