using NftApi.Entities;

namespace NftApi;

public class Bid
{
    public int Id { get; set;}
    public int Amount { get; set;}
    public int UserId { get; set;}
    public User User { get; set;}
    public int AuctionId { get; set;}
    public Auction Auction { get; set;}
    public DateTime Time { get; set; }
}
