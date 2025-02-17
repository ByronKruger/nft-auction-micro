using NftApi.DTOs;

namespace NftApi;

public class AuctionDto
{
    public int Id { get; set; }
    public List<UserDto> Users { get; set; }
    public UserDto winningBidder { get; set; }
    public NftDto nft { get; set; }
    public bool IsActive { get; set;}
    public string Charity { get; set; }
    public List<BidDto> Bids { get; set; }
}
