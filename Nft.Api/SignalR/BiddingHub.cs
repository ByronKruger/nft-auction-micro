using NftApi.Data;
using NftApi.Interfaces;
using NftApi.Entities;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;

namespace NftApi.SignalR;

public class BiddingHub(
    // IAuctionRepository auctionRepo,
    // IUserRepository userRepo, 
    // INftRepository nftRepo
    IMapper _mapper,
    IAuctionService _auctionService,
    IUnitOfWork uow
    ): Hub
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var charityId = httpContext.Request.Query["charity"];
        var nftId = httpContext.Request.Query["nft"];

        if (string.IsNullOrEmpty(charityId) && string.IsNullOrEmpty(nftId)) return;
        
        if (await _auctionService.HasActiveAction()) 
        {
            await Clients.Caller.SendAsync("HubException", "There is already an active auction");
            return;
            // throw new HubException("There is already an active auction");
        }

        var auctionNft = await uow.NftRepo.GetNftsByIdAsync(int.Parse(nftId));
        var auctionCharity = await uow.UserRepo.GetUserByIdAsync(int.Parse(charityId));
        var auctionUsers = new List<User>();
        auctionUsers.Add(auctionCharity);

        var newAuction = new Auction
        {
            IsActive = true,
            nft = auctionNft,
            Users = auctionUsers
        };
        uow.AuctionRepo.AddAuction(newAuction);
        await uow.Complete();
        var auctionDto = _mapper.Map<AuctionDto>(newAuction);
        await Clients.Others.SendAsync("NewAuction", auctionDto);
    }

    public async Task<BidDto> PlaceBid(PlaceBidDto placeBidDto)
    {
        // check for no active auction
        var auction = await uow.AuctionRepo.GetActiveAuctionAsync();
        var user = await uow.UserRepo.GetUserByUsernameAsync(Context.User.GetUsername());

        Console.WriteLine("user.UserName");
        Console.WriteLine(user.UserName);
        var userWithWallet = await uow.UserRepo.GetUserByIdAsync(user.Id);
        Console.WriteLine("userWithWallet");
        Console.WriteLine(userWithWallet.Id);
        // if (userWithWallet.Wallet == null) throw new HubException("No funds to place bid");
        if (userWithWallet.Wallet == null) 
        {
            await Clients.Caller.SendAsync("HubException", "No funds to place bid");
            return new();
        }

        var newBid = new Bid
        {
            // User = user,
            UserId = user.Id,
            Amount = placeBidDto.amount,
            AuctionId = auction.Id,
            // Time = "today",
            Time = DateTime.UtcNow
            // Auction = auction
        };
        // auction.User = user;
        // auction.Bids.Add(newBid);

        Console.WriteLine(newBid.Id);
        Console.WriteLine("newBid.Time");
        Console.WriteLine(newBid.Time);
        // Console.WriteLine(newBid.User.Id);
        // Console.WriteLine(newBid.Auction.Id);
        Console.WriteLine(newBid.Amount);
        auction.Users.Add(user);
        var auctionBids = auction.Bids;
        var highestBid = 0m;
        if (auctionBids.Count != 0) highestBid = auctionBids.OrderByDescending(b => b.Amount).ElementAt(0).Amount;

        Console.WriteLine("newBid.Amount > highestBid");
        Console.WriteLine(newBid.Amount > highestBid);
        if (newBid.Amount > highestBid)
        {
            uow.BidRepo.AddBid(newBid);
            // uow.AuctionRepo.AddBid(newBid);
            if (await uow.Complete())  
            {
                Console.WriteLine("await uow.Complete()");

                await Groups.AddToGroupAsync(Context.ConnectionId, auction.Id.ToString());        
                await Clients.Group(auction.Id.ToString()).SendAsync("NewBidPlaced", _mapper.Map<BidDto>(newBid));
                return _mapper.Map<BidDto>(newBid);
            }
            // throw new HubException("Could not place bid");
            await Clients.Caller.SendAsync("HubException", "Could not place bid");
            return _mapper.Map<BidDto>(new {});
        }
        // throw new HubException("Bid placed is less than current highest");
        await Clients.Caller.SendAsync("HubException", "Bid placed is less than current highest");
        return _mapper.Map<BidDto>(new {});
    }

    public async Task ConcludeAuction()
    {
        var auction = await _auctionService.CloseAuction();
        await Clients.Others.SendAsync("AuctionConcluded", auction);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // await Clients.Others.SendAsync('def');
        await base.OnDisconnectedAsync(exception);
    }
}