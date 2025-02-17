namespace NftApi;

public interface IAuctionRepository
{
    void AddAuction(Auction auction);
    // Task<bool> SaveChangesAsync();
    Task<Auction> GetAuctionByIdAsync(int id); 
    Task<Auction> GetActiveAuctionAsync(); 
    Task<List<Auction>> GetAllAuctionsAsync();
    void AddBid(Bid bid);
    // Task GetAuctionByIdAsync(object value);
    void RemoveAuction(Auction auction);
    Task<AuctionDto> GetActiveAuctionDtoAsync();
    void SetContext(Auction auction);
    //  +++++++++
    void ExplicitAdd(Auction auction);
    void DetachExplicit(Auction auction);
    //  +++++++++
}
