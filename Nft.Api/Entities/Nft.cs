using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NftApi.Entities;

namespace NftApi;

[Table("Nft")]
public class Nft
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    // public int Id { get; set; }
    // public int NftContractId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Auction Auction { get; set; }
    public int? AuctionId { get; set; }
    public bool IsOwned { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string ArtistName { get; set; }
    public string shortDescription { get; set; }
}
