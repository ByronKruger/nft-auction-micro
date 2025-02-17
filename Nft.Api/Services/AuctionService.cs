using Microsoft.AspNetCore.Identity;
using NftApi.Entities;
using NftApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace NftApi;

public class AuctionService : IAuctionService
{
    // private readonly IAuctionRepository _auctionRepository;
    // private readonly INftRepository _nftRepository;
    // private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;


    public AuctionService(
        // IAuctionRepository auctionRepository, 
        // INftRepository nftRepository,
        // IUserRepository userRepository, 
        IUnitOfWork uow,
        RoleManager<Role> roleManager, IMapper mapper)
    {
        // _auctionRepository = auctionRepository;
        // _nftRepository = nftRepository;
        // _userRepository = userRepository;
        _uow = uow;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<AuctionDto> CloseAuction()
    {
        // AuctionDto auction = await _uow.AuctionRepo.GetActiveAuctionDtoAsync();
        Auction auction = await _uow.AuctionRepo.GetActiveAuctionAsync();
        Console.WriteLine("auction.Id");
        Console.WriteLine(auction.Id);
        Console.WriteLine("auction.nft.Id");
        Console.WriteLine(auction.nft.Id);
        Nft auctionNft = await _uow.NftRepo.GetNftsByIdAsync(auction.nft.Id);
        List<Bid> bids = auction.Bids;
        Auction auctionToUpdate = await _uow.AuctionRepo.GetActiveAuctionAsync();
        Console.WriteLine("auctionToUpdate:");
        Console.WriteLine(auctionToUpdate.Id);
        Console.WriteLine(auctionToUpdate.nft.Id);
        Console.WriteLine(auctionToUpdate.Users.Count());

        Console.WriteLine("bids.Count()");
        Console.WriteLine(bids.Count());
        if (bids.Count() == 0) 
        {
            Console.WriteLine("Inside");
            // auctionToUpdate.IsActive = false;
            auction.IsActive = false;//dup
            auctionNft.IsOwned = true;

            // // =================================
            // var newAuction = new Auction
            // {
            //     IsActive = true,
            //     nft = auctionNft,
            //     Users = auctionUsers
            // };
            // _auctionRepository.AddAuction(newAuction);
            // return await _auctionRepository.SaveChangesAsync();
            // // =================================

            // _uow.AuctionRepo.SetContext(auctionToUpdate); // ???
            _uow.AuctionRepo.SetContext(auction); // dup
            await _uow.Complete();
            // await _nftRepository.SaveAllAsync();
            return _mapper.Map<AuctionDto>(auction);
        }

        List<User> auctionUsers = auction.Users;
        var rolesUsers = _uow.UserRepo.GetRoleUsersQueryable();
        User charity = new();

        Console.WriteLine("OUTSIDE LOOP: auctionUsers.Count()");
        Console.WriteLine(auctionUsers.Count());
        Console.WriteLine("OUTSIDE LOOP: auctionUsers");
        Console.WriteLine(auctionUsers);

        for (var i = 0; i < auctionUsers.Count(); i++) 
        {
            Console.WriteLine("INSIDE LOOP1: auctionUsers.Count()");
            Console.WriteLine(auctionUsers.Count());

            rolesUsers = rolesUsers.Where(ru => ru.User.Id == auctionUsers.ElementAt(i).Id);

            Console.WriteLine("INSIDE LOOP1: rolesUsers");
            Console.WriteLine(rolesUsers.Count());

            var userRoles = rolesUsers.Select(ru => ru.Role);
            Console.WriteLine("userRoles.Count()");
            Console.WriteLine(userRoles.Count());
            Console.WriteLine("i");
            Console.WriteLine(i);

            for (var j = 0; j < userRoles.Count(); j++)
            {
                Console.WriteLine("j");
                Console.WriteLine(j);
                Console.WriteLine("userRoles.ElementAt(0).Name");
                Console.WriteLine(userRoles.ElementAt(0).Name);

                if (userRoles.ElementAt(j).Name == "CHARITY") 
                {
                    charity = auctionUsers.ElementAt(i);
                    break;
                }
            }
        }

        Console.WriteLine("bids.Count()");
        Console.WriteLine(bids.Count());
        Console.WriteLine(bids.Count() == 0);

        // Console.WriteLine("bids");
        // Console.WriteLine(bids.Count());
        var highestBid = bids.OrderByDescending(b => b.Amount).FirstOrDefault();
        // Console.WriteLine("highestBid.Amount");
        // Console.WriteLine(highestBid.Amount);
        User winningBidder = await _uow.UserRepo.GetUserByIdAsync(highestBid.User.Id);
        // Console.WriteLine("winningBidder.UserName");
        // Console.WriteLine(winningBidder.UserName);
        
        // Console.WriteLine("auctionUsers.Count()");
        // Console.WriteLine(auctionUsers.Count());

        // for (var i = 0; i < auctionUsers.Count(); i++) 
        // {
        //     // Console.WriteLine("auctionUsers.ElementAt(i).Id");
        //     // Console.WriteLine(auctionUsers.ElementAt(i).Id);
        //     rolesUsers = rolesUsers.Where(ru => ru.User.Id == auctionUsers.ElementAt(i).Id);
        //     var userRoles = rolesUsers.Select(ru => ru.Role);

        //     for (var j = 0; j < userRoles.Count(); j++)
        //     {
        //         if (userRoles.ElementAt(0).Name == "CHARITY") 
        //         {
        //             charity = auctionUsers.ElementAt(i);
        //             break;
        //         }
        //     }

        //     // Console.WriteLine(userRoles.ElementAt(0).Name);
        //     // Console.WriteLine(userRoles.ElementAt(0));

        //     // var userRoles = auctionUsers.ElementAt(i).UserRoles;
        //     // Console.WriteLine("userRoles.Count()");
        //     // Console.WriteLine(userRoles.Count());
        //     // Console.WriteLine(i);
        //     // Console.WriteLine("auctionUsers.ElementAt(i).UserName");
        //     // Console.WriteLine(auctionUsers.ElementAt(i).UserName);
        // }

        // Console.WriteLine("auctionUsers.elem");
        // Console.WriteLine("auctionUsers.Count");
        // Console.WriteLine(auctionUsers.Count());
        // Console.WriteLine("bids.Count");
        // Console.WriteLine(bids.Count());
        // Console.WriteLine("auctionUsers.ElementAt(0).UserName");
        // Console.WriteLine(auctionUsers.ElementAt(0).UserName);

        // User auctionUser;
        // for (int i = 0; i < bids.Count(); i++)
        // {
        //     Console.WriteLine("auctionUsers.ElementAt(i).UserName "+ i +": ");
        //     Console.WriteLine("bids.ElementAt(i).User.UserName");
        //     Console.WriteLine("bids.ElementAt(i).Id");
        //     Console.WriteLine(bids.ElementAt(i).Id);
        //     Console.WriteLine("bids.ElementAt(i).AuctionId");
        //     Console.WriteLine(bids.ElementAt(i).AuctionId);
        //     Console.WriteLine("bids.ElementAt(i).Auction.nft.Id");
        //     Console.WriteLine(bids.ElementAt(i).Auction.nft.Id);
        //     Console.WriteLine("bids.ElementAt(i).UserId");
        //     Console.WriteLine(bids.ElementAt(i).UserId);

        //     var a = await _uow.UserRepo.GetRoleByIdAsync(0);
        //     // var a = await _uow.UserRepo.GetRoleByIdAsync(bids.ElementAt(0).Id);
        //     Console.WriteLine("a");
        //     Console.WriteLine(a);
        //     // Console.WriteLine(a.Name);

        //     Console.WriteLine("bids.ElementAt(i).User");
        //     Console.WriteLine(bids.ElementAt(i).User);
        //     Console.WriteLine(bids.ElementAt(i).User.UserName);
        //     auctionUser = await _uow.UserRepo.GetUserByIdAsync(bids.ElementAt(i).User.Id);
        //     Console.WriteLine(auctionUser.UserName);
        //     // Console.WriteLine(auctionUser.);
        //     // Console.WriteLine(auctionUser.Roles.ElementAt(0).Name);
        //     // Console.WriteLine(auctionUser.Roles.ElementAt(0).Role.Name);
        //     // Console.WriteLine("auctionUser");
        //     // Console.WriteLine(auctionUser.UserName);
        //     // Console.WriteLine("auctionUser.UserRoles");
        //     // Console.WriteLine(auctionUser.UserRoles);
        //     // Console.WriteLine("auctionUser.UserRoles.Count");
        //     // Console.WriteLine(auctionUser.UserRoles.Count);
        //     // Console.WriteLine("auctionUsers.ElementAt(0).UserRoles.Count");
        //     // Console.WriteLine("i");
        //     // Console.WriteLine(i);
        //     // Console.WriteLine(auctionUser.UserRoles.Count);
        //     // Console.WriteLine("auctionUser.UserRoles.ElementAt(0)");
        //     // Console.WriteLine(auctionUser.UserRoles.ElementAt(0));
        //     // Console.WriteLine("auctionUser.UserRoles.ElementAt(0).RoleId");
        //     // Console.WriteLine(auctionUser.UserRoles.ElementAt(0).RoleId);
        //     // var userByRoleId = await _uow.UserRepo.GetUserByRoleId(auctionUser.UserRoles.ElementAt(0).RoleId);
        //     // Console.WriteLine("userByRoleId");
        //     // Console.WriteLine(userByRoleId.UserName);
        //     // Console.WriteLine("userByRoleId.UserRoles.Count()");
        //     // Console.WriteLine(userByRoleId.UserRoles.Count());
        //     // Console.WriteLine("userByRoleId.UserRoles.ElementAt(0).Role");
        //     // Console.WriteLine(userByRoleId.UserRoles.ElementAt(0).Role);
        //     // Console.WriteLine("userByRoleId.UserRoles.ElementAt(0).Role.Name");
        //     // Console.WriteLine(userByRoleId.UserRoles.ElementAt(0).Role.Name);
        //     // auctionUser.UserRoles.ElementAt(j)
        //     // Console.WriteLine(userByRoleId.);
        //     // var auctionUserRole = _roleManager.
        //     // .GetRoleIdAsync(auctionUser.UserRoles.ElementAt(0).Role);
        //     // Console.WriteLine(auctionUserRole);

        //     // for (int j = 0; j < auctionUser.UserRoles.Count(); j++)
        //     // {
        //     //     if (auctionUser.UserRoles.ElementAt(j).Role.Name == "CHARITY") 
        //     //         charity = auctionUsers.ElementAt(i);
        //     // }
        // }

        // auction.nft.UserId = charity.Id;
        Console.WriteLine("auctionNft.Id");
        Console.WriteLine(auctionNft.Id);
        auctionNft.User = winningBidder;
        // await _nftRepository.SaveAllAsync();

        Console.WriteLine("charity");
        // Console.WriteLine(charity.UserName);
        Console.WriteLine(charity.Id);

        UserDto charityWithWallet = await _uow.UserRepo.GetUserDtoByIdAsync(charity.Id);
        Console.WriteLine("charityWithWallet.Wallet.Address");
        // Console.WriteLine(charityWithWallet.Wallet.Address);

        // var auction1 = await _auctionRepository.GetActiveAuctionAsync();
        // auctionToUpdate.IsActive = false;
        auction.IsActive = false; // dup
        auctionNft.IsOwned = true; 

        Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::");
        Console.WriteLine(auctionNft);
        Console.WriteLine("auctionNft.Id");
        Console.WriteLine(auctionNft.Id);
        Console.WriteLine("auctionNft.IsOwned");
        Console.WriteLine(auctionNft.IsOwned);
        // Console.WriteLine(auctionNft.User.UserName);
        Console.WriteLine("=========================================\n");
        Console.WriteLine(auction);
        Console.WriteLine("auction.IsActive");
        Console.WriteLine(auction.IsActive);
        Console.WriteLine("auction.nft.User.UserName");
        Console.WriteLine(auction.nft.User.UserName);
        // Console.WriteLine(auction.IsOwned);
        // Console.WriteLine(auction.IsOwned);
        Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::");

        // _uow.AuctionRepo.SetContext(auctionToUpdate); // ???

        // udpate user (winnning bidder) to have MyNFTs
        winningBidder.HasNfts = true;

        auction.nft = auctionNft;

        auction.WinningBidder = winningBidder;

        _uow.AuctionRepo.SetContext(auction); // dup
        await _uow.Complete();
        // await _uow.AuctionRepo.GetActiveAuctionAsync()

        // exchange funds for NFT
        // 1. send nft to winning bidder
        // await SendNft(winningBidder.Wallet.Address, auction.nft.Id);
        // 2. send money to charity
        // await SendFunds(charity.Wallet.Address, highestBid.Amount);
        // 3. revoke ActiveBidderRole
        // for (int i = 0; i < auctionUsers.Count; i++)
        //     if (auctionUsers.ElementAt(i) != charity)
        //         service.revokeRole(auctionUsers.ElementAt(i).Wallet.Address);
        
        return _mapper.Map<AuctionDto>(auction);
    }

    public async Task<bool> CreateAuction(CreateAuctionDto auction)
    {

        var auctionNft = await _uow.NftRepo.GetNftsByIdAsync(auction.nftId);
        var auctionCharity = await _uow.UserRepo.GetUserByIdAsync(auction.charityId);
        var auctionUsers = new List<User>([auctionCharity]);

        // for (var i = 0; i < auctionUsers.Count(); i++) 
        // {
        //     Console.WriteLine("auctionUsers.ElementAt(i).UserName");
        //     Console.WriteLine(auctionUsers.ElementAt(i).UserName);
        // }

        var newAuction = new Auction
        {
            IsActive = true,
            nft = auctionNft,
            Users = auctionUsers
        };
        _uow.AuctionRepo.AddAuction(newAuction);
        return await _uow.Complete();
    }

    public async Task<bool> PlaceBid(User user, PlaceBidDto bid)
    {
        // do check for exceeding funding contract balance
        Console.WriteLine("1:");
        // var user1 = await _uow.UserRepo.GetUserByUsernameAsync(user.UserName);

        var auction = await _uow.AuctionRepo.GetActiveAuctionAsync();

        // if (auction == null)
        // {
        //     // existingEntity = await _context.Parents
        //     //     .Include(p => p.Children)
        //     //     .FirstOrDefaultAsync(p => p.Id == parentId);
        //     var auction = await _uow.AuctionRepo.GetActiveAuctionAsync();

        // if (auction != null)
        // {
        //     _uow.Attach(auction);
        // }
        // }

        // _uow.AuctionRepo.ExplicitAdd(auction);
        Console.WriteLine("auction");    
        Console.WriteLine(auction);
        Console.WriteLine(auction.Id);
        Console.WriteLine(auction.Bids.Count());
        Console.WriteLine(auction.Users.Count());
        // Console.WriteLine(auction.Users.Count);
        // Console.WriteLine(auction.nft.Id);
        Console.WriteLine(auction.IsActive);

        var userWithWallet = await _uow.UserRepo.GetUserByIdAsync(user.Id);

        // if (user.Wallet == null) return false;
        if (userWithWallet.Wallet == null) throw new Exception("No funds to place bid");

        var newBid = new Bid
        {
            // User = user.,
            UserId = user.Id,
            Amount = bid.amount,
            // Auction = auction,
            AuctionId = auction.Id
        };

        // auction.Users.Add(user);
        // auction.Bids.Add(newBid);

        var auctionBids = auction.Bids;
        Console.WriteLine("2:");

        var highestBid = 0m;
        if (auctionBids.Count != 0) highestBid = auctionBids.OrderByDescending(b => b.Amount).ElementAt(0).Amount;
        Console.WriteLine("3:");

        Console.WriteLine(newBid.Id);
        Console.WriteLine(newBid.Amount);
        Console.WriteLine("newBid.Auction.nft");
        // Console.WriteLine(newBid.Auction.nft);
        // Console.WriteLine(newBid.Auction.nft.Id);
        Console.WriteLine("user");
        Console.WriteLine(user);
        Console.WriteLine("user.Id");
        Console.WriteLine(user.Id);
        Console.WriteLine(user.GetType());
        // Console.WriteLine(newBid.User.Id);

        Console.WriteLine("isValid Bid?");
        Console.WriteLine((newBid.Amount > highestBid));

        Console.WriteLine("newBid.Amount");
        Console.WriteLine(newBid.Amount);
        Console.WriteLine("highestBid");        
        Console.WriteLine(highestBid);
        if (newBid.Amount > highestBid)
        {
            // auction.Users.Add(user1);
            user.AuctionId = auction.Id;
            // _uow.AuctionRepo.AddBid(newBid);
            _uow.BidRepo.AddBid(newBid);
            bool saved = await _uow.Complete(); 
            Console.WriteLine("saved");
            Console.WriteLine(saved);
            // _auctionRepository.DetachExplicit(auction);
            return saved;
            // return await _auctionRepository.SaveChangesAsync(); 
        }
        Console.WriteLine("4:");

        return false;
    }

    public async Task<bool> RemoveAuction(int id)
    {
        var auction = await _uow.AuctionRepo.GetAuctionByIdAsync(id);
        _uow.AuctionRepo.RemoveAuction(auction);
        return await _uow.Complete();
    }

    private User GetCharity(List<User> users)
    {
        var rolesUsers = _uow.UserRepo.GetRoleUsersQueryable();
        for (var i = 0; i < users.Count(); i++) 
        {
            rolesUsers = rolesUsers.Where(ru => ru.User.Id == users.ElementAt(i).Id);  
            var userRoles = rolesUsers.Select(ru => ru.Role);

            for (var j = 0; j < userRoles.Count(); j++)
            {
                // Console.WriteLine(":::::::::::::::");
                // Console.WriteLine(userRoles.ElementAt(j).Name);
                // Console.WriteLine(":::::::::::::::");
                if (userRoles.ElementAt(j).Name == "CHARITY") return users.ElementAt(i);
            }
        }
        return new();
    }

    public async Task<AuctionDto> GetActiveAuction()
    {
        var auction = await _uow.AuctionRepo.GetActiveAuctionAsync();
        // if (auction == null) throw new Exception("No active auction");
        // var a = _mapper.Map<AuctionDto>(auction);
        // Console.WriteLine("a.Bids.ElementAt(0).User");
        // Console.WriteLine(a.Bids.ElementAt(0).user.Username);
        // List<Bid> placeholder = (List<Bid>)auction.Bids.Skip(Math.Max(0, auction.Bids.Count() - 5));
        List<Bid> minimalBids = new List<Bid>();
        if (auction.Bids.Count() >= 5) 
        {
            for (var i = auction.Bids.Count() - 5; i < auction.Bids.Count(); i++)
            {
                minimalBids.Add(auction.Bids.ElementAt(i));
            }
            auction.Bids = minimalBids;
        }

        // auction.Bids = (List<Bid>) auction.Bids.Skip(Math.Max(0, auction.Bids.Count() - 5));/
        // Console.WriteLine("auction.Users.Count()");
        // Console.WriteLine(auction.Users.Count());
        // Console.WriteLine("auction.Users.ElementAt(0).UserName");
        // Console.WriteLine(auction.Users.ElementAt(0).UserName);
        // Console.WriteLine("auction.Users.ElementAt(0).UserName");
        // Console.WriteLine(auction.Users.ElementAt(0).UserName);
        // Console.WriteLine("auction.Users.ElementAt(1).UserName");
        // Console.WriteLine(auction.Users.ElementAt(1).UserName);
        // Console.WriteLine("auction.Users.ElementAt(2).UserName");
        // Console.WriteLine(auction.Users.ElementAt(2).UserName);
        // Console.WriteLine("auction.Users.ElementAt(3).UserName");
        // Console.WriteLine(auction.Users.ElementAt(3).UserName);
        AuctionDto auctionDto = _mapper.Map<AuctionDto>(auction);
        auctionDto.Charity = GetCharity(auction.Users).UserName;
        // var charityUser = _mapper.Map<UserDto>(GetCharity(auction.Users));
        // Console.WriteLine(charityUser.Id);
        // Console.WriteLine(charityUser);
        Console.WriteLine("auctionDto.Charity");
        Console.WriteLine(auctionDto.Charity);
        // Console.WriteLine(charityUser.Charity.UserName);
        // auctionDto.Charity = charityUser.UserName;

        
        return auctionDto;
    }

    public async Task<bool> HasActiveAction()
    {
        var auction = await _uow.AuctionRepo.GetActiveAuctionAsync();
        if (auction == null) return false;
        return true;
        // return _mapper.Map<AuctionDto>(auction);
    }
}
