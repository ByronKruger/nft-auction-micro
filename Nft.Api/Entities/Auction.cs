using NftApi.Entities;

namespace NftApi;

public class Auction
{
    public int Id { get; set; }
    public List<User> Users { get; set; }
    public Nft nft { get; set; }
    public bool IsActive { get; set;}
    // public User Charity { get; set; }
    public List<Bid> Bids { get; set; }
    public User WinningBidder {get; set;}
}
