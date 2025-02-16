namespace NftApi;

public interface IBidRepository
{
    void AddBid(Bid bid);
    // Task<List<Bid>> GetBidsBy(int id);
}
