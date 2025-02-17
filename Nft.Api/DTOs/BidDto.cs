using NftApi.Entities;

namespace NftApi;

public class BidDto
{
    // public int Id { get; set;}
    public int amount { get; set;}
    // public int UserId { get; set;}
    public string username { get; set;}
    // public int AuctionId { get; set;}
    // public AuctionDto Auction { get; set;}
    public string time {get; set;}
}
