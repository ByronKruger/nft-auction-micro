using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NftApi.Entities;
using NftApi.Interfaces;

namespace NftApi;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService _auctionService;
    // private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public AuctionController(IAuctionService auctionService, 
        // IUserRepository userRepository,
        IUnitOfWork uow
    )
    {
        _auctionService = auctionService;
        _uow = uow;
        // _userRepository = userRepository;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("startAuction")]
    public async Task<ActionResult> StartAuction(CreateAuctionDto auctionStartDto)
    {
        if (await _auctionService.HasActiveAction()) throw new Exception("There is already an active auction");
        // if (await _auctionService.GetActiveAuction() != null) throw new Exception("There is already an active auction"); 
        var result = await _auctionService.CreateAuction(auctionStartDto);
        if (result) return Ok();
        return BadRequest("Failed to start new auction");
    }

    [HttpPost("placeBid")]
    public async Task<ActionResult> PlaceBid(PlaceBidDto placeBidDto)
    {
        // grant activeBidder role
        // bid amount is higher than deposit amount
        var u = User;
        var user = await _uow.UserRepo.GetUserByUsernameAsync(u.GetUsername());
        if (await _auctionService.PlaceBid(user, placeBidDto)) return Ok();
        return BadRequest("Could not place bid");
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("concludeAuction")]
    public async Task<ActionResult<User>> ConcludeAuction()
    {
        // policy-based authz (401) D
        // get active auction 
        // get winning bidder (highest bid amount) address
        // get charity address
        // make exchange of NFT w/ bidder and charity.
        // set auction as inactive
        // revoke activeBidder role for all bidders

        var charity = await _auctionService.CloseAuction();

        // var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        // var user = await _userRepository.GetUserByUsernameAsync("abc");
        // var list = user.Bids;
        // list.ElementAt(8).Amount = 10.0m;
        //     var u = User;
        //     var user = await _userRepository.GetUserByUsernameAsync(u.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //     if (await _auctionService.PlaceBid(user, placeBidDto)) return Ok();
        //     return BadRequest("Could not place bid");
        return Ok(charity);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("removeAuction")]
    public async Task<ActionResult> RemoveAuction(AuctionDeleteDto auctionDeleteDto)
    {
        if (await _auctionService.RemoveAuction(auctionDeleteDto.Id)) return Ok();
        return BadRequest("Could not delete auction");
    }

    [HttpGet("getAuction")]
    public async Task<ActionResult<AuctionDto>> GetAuction()
    {
        if (!await _auctionService.HasActiveAction()) return NotFound("No active auction.");
        return await _auctionService.GetActiveAuction();
    }
}
