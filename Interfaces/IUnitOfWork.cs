
namespace NftApi.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepo {get;}
    IBidRepository BidRepo {get;}
    INftRepository NftRepo {get;}
    IAuctionRepository AuctionRepo {get;}
    void Attach(Auction auction);
    // IBidRepository BidRepo {get;}
    Task<bool> Complete();
    bool HasChanges();
}   
