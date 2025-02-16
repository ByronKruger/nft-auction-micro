using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NftApi.Interfaces;
using NftApi.Helpers;
using NftApi.Extensions;
using NftApi.Services;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace NftApi;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class NftController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly INftService _nftService;
    private readonly IImageService _imageService;
    private readonly IUnitOfWork _uow;

    public NftController(
        // IUserRepository userRepository, 
        IUnitOfWork uow,
        INftService nftService,
        IImageService imageService)
    {
        // _userRepository = userRepository;
        _nftService = nftService;
        _imageService = imageService;
        _uow = uow;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("sendNftImage")]
    public async Task<ActionResult<NftDto>> SendNftImage(IFormFile file)
    {
        var user = await _uow.UserRepo.GetUserByUsernameAsync(User.GetUsername());

        return Ok(await _nftService.MintNftImage(user, file));
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("sendNftDetails")]
    public async Task<ActionResult<NftDto>> MintNfts(NftDto nftMintDto)
    {
        // ===============================================================
        // // var u = User;
        // // Console.WriteLine("a");
        // var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        // // Console.WriteLine("a");

        // var newNfts = await _nftService.MintNfts(nftMintDto.Amount, user);
        // // Console.WriteLine("newNfts");
        // // foreach (var nft in newNfts)
        // // {
        // //     Console.WriteLine(nft);
        // //     Console.WriteLine(nft.Id);
        // //     Console.WriteLine(nft.User);
        // // }
        // return Ok(newNfts);

        // // u.FindFirst(ClaimTypes.Name)?.Value);           
        // // var user = 
        // // var a = User.

        // // var a = Context
        // // caller must be admim // unauthorized (policy, actaully)
        // // check nft existing id (must exist a get call) //bad requesy
        // // perform mint via nftContractService
        // // save all nfts to admin user
        // // return all minted nft
        // ===============================================================
        if (!await _nftService.MintNfts(nftMintDto)) return BadRequest("Could not save nft details");
        // return CreatedAtAction(nameof(GetNfts()));
        return Ok();
    }

    // Maybe add one where admin can do on-behalf
    [HttpPost("claimDeposit")]
    public async Task<ActionResult> ClaimDeposit(ClaimDepositDto claimDepositDto)
    {
        // var u = User;
        // var user = await _userRepository.GetUserByUsernameAsync(u.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _uow.UserRepo.GetUserByUsernameAsync(User.GetUsername());
        Console.WriteLine("user.Username");
        Console.WriteLine(user.UserName);
        if (await _nftService.ClaimDeposit(claimDepositDto.Address, user)) return Ok();
        return BadRequest("Something went wrong while claiming the deposit");
    }

    [HttpGet("getNfts")]
    public async Task<ActionResult<PagedList<NftDto>>> GetNfts([FromQuery]UserParams userParams, [FromQuery]bool isStartAuction = false)
    {
        var user = await _uow.UserRepo.GetUserByUsernameAsync(User.GetUsername());
        PagedList<NftDto> nfts;

        List<String> roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();

        if (roles.Contains("ADMIN")) {
            nfts = await _nftService.GetAllNfts(userParams, isStartAuction);
        }
        else {
            userParams.UserId = user.Id;
            nfts = await _nftService.GetAllNftsForUser(userParams);
        }
        // var u = User;
        // var user = await _userRepository.GetUserByUsernameAsync(u.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        // =================
        // var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        // List<NftDto> nfts = await _nftService.GetUserNfts(user);
        // =================

        // PagedList<NftDto> 

        Response.AddPaginationHeader(nfts);
        return nfts;
    }

    // [HttpGet("getMyNfts")]
    // public async Task<ActionResult<PagedList<NftDto>>> GetAllNftsForUser() {
    //     var user = await _uow.UserRepo.GetUserByUsernameAsync(User.GetUsername());
    //     Console.WriteLine("user");
    //     Console.WriteLine(user);
    //     // var user = await _userRepository.GetUserByUsernameAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    //     UserParams userParams = new();
    //     userParams.UserId = user.Id;
    //     PagedList<NftDto> nfts = await _nftService.GetAllNftsForUser(userParams);

    //     Response.AddPaginationHeader(nfts);
    //     return nfts;
    // }
}
