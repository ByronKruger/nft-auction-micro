
using Microsoft.EntityFrameworkCore;
using NftApi.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using NftApi.Entities;

namespace NftApi;

public class AuctionRepository : IAuctionRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public void ExplicitAdd(Auction auction)
    {
        _dataContext.Auctions.Attach(auction);
    }

    public AuctionRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public void AddAuction(Auction auction)
    {
        _dataContext.Auctions.Add(auction);
    }

    public void AddBid(Bid bid)
    {
        _dataContext.Bids.Add(bid);
    }

    public async Task<Auction> GetActiveAuctionAsync()
    {
        // throw new NotImplementedException();
        var list = await _dataContext.Auctions
            .Where(a => a.IsActive)
            .Include(a => a.Bids)
                .ThenInclude(a => a.User)
            .Include(a => a.nft)
            .Include(a => a.Users)
            .Select(a => 
                // var usersDetails = new List<User>();
                // foreach (var account in a.Users)
                // {
                //     usersDetails.Add({
                //         Id = account.Id,
                //         Username = account.Username
                //     });
                // };
                new Auction {
                    Id = a.Id,
                    Users = a.Users.Select(u => 
                        new User {
                            Id = u.Id,
                            UserName = u.UserName
                        }
                    ).ToList(),
                    nft = a.nft,
                    Bids = a.Bids,
                    IsActive = a.IsActive
                }
            )
            .FirstOrDefaultAsync();
            // .ToListAsync();

        return list;
        // return list.ElementAt(list.Count() - 1);
        // return list.ElementAt(0);
        // .FirstOrDefaultAsync();
    }

    public async Task<AuctionDto> GetActiveAuctionDtoAsync()
    {
        // throw new NotImplementedException();
        var list = await _dataContext.Auctions
            .Where(a => a.IsActive)
            .Include(a => a.Bids)
            .Include(a => a.nft)
            .Include(a => a.Users)
            .ProjectTo<AuctionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
            // .ToListAsync();

        return list;
        // return list.ElementAt(list.Count() - 1);
        // return list.ElementAt(0);
            // .FirstOrDefaultAsync();
    }

    public async Task<List<Auction>> GetAllAuctionsAsync()
    {
        // throw new NotImplementedException();
        return await _dataContext.Auctions.ToListAsync();
    }

    public async Task<Auction> GetAuctionByIdAsync(int id)
    {
        // throw new NotImplementedException();
        return await _dataContext.Auctions.FindAsync(id);
    }

    public void RemoveAuction(Auction auction)
    {
        _dataContext.Auctions.Remove(auction);
    }

    public void SetContext(Auction auction)
    {
        _dataContext.Entry(auction).State = EntityState.Modified;
    }

    // public async Task<bool> SaveChangesAsync()
    // {
    //     bool hasChanges = _dataContext.ChangeTracker.HasChanges(); // should be true
    //     int updates = _dataContext.SaveChanges();   
    //     Console.WriteLine(hasChanges);
    //     Console.WriteLine(updates);
    //     return await _dataContext.SaveChangesAsync() > 0;
    // }

    public void DetachExplicit(Auction auction)
    {
       _dataContext.Entry(auction).State = EntityState.Detached;
    }
}
