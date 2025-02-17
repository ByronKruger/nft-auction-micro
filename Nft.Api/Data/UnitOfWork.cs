using NftApi.Interfaces;
using AutoMapper;

namespace NftApi.Data;

class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public UnitOfWork(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public void Attach(Auction auction)
    {
        _dataContext.Attach(auction);
    }

    public IBidRepository BidRepo => new BidRepository(_dataContext);

    public IUserRepository UserRepo => new UserRepository(_dataContext, _mapper);

    public INftRepository NftRepo => new NftRepository(_dataContext, _mapper);

    public IAuctionRepository AuctionRepo => new AuctionRepository(_dataContext, _mapper);

    // public IBidRepository BidRepo => new BidRepository();

    public async Task<bool> Complete()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _dataContext.ChangeTracker.HasChanges();
    }
}