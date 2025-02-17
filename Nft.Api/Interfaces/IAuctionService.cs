using NftApi.Entities;

namespace NftApi;

public interface IAuctionService
{
    Task<bool> CreateAuction(CreateAuctionDto auction);
    Task<bool> PlaceBid(User user, PlaceBidDto bid);
    Task<AuctionDto> GetActiveAuction();
    // Task<User> CloseAuction();
    Task<AuctionDto> CloseAuction();
    Task<bool> RemoveAuction(int id);
    Task<bool> HasActiveAction();
}
