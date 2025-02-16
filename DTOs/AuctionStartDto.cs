using System.ComponentModel.DataAnnotations;

namespace NftApi;

public class CreateAuctionDto
{
    [Required]
    public int charityId { get; set; }
    [Required]
    public int nftId { get; set; }
}
