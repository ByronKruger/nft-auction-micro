using Microsoft.AspNetCore.Identity;

namespace NftApi.Entities;

public class User : IdentityUser<int>
{
    // public int Id { get; set; } 
    // public string UserName { get; set; }
    // public byte[] PasswordHash { get; set; }
    // public byte[] PasswordSalt { get; set; }
    public ICollection<RoleUser> UserRoles { get; set; }
    public bool hasFunds { get; set; }
    public Wallet Wallet { get; set; }
    public List<Nft> Nfts { get; set; }
    public Auction? Auction {get; set; }
    public int? AuctionId { get; set; }
    public List<Bid> Bids { get; set; }
    public bool HasNfts {get;set;}
}