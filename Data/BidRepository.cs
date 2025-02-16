using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NftApi.Data;

namespace NftApi;

public class BidRepository : IBidRepository
{

    private readonly DataContext _dataContext;

    public BidRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void AddBid(Bid bid)
    {
        _dataContext.Bids.Add(bid);
    }
}