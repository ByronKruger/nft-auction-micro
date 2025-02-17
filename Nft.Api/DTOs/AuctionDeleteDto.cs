using System.ComponentModel.DataAnnotations;

namespace NftApi;

public class AuctionDeleteDto
{
    [Required]
    public int Id { get; set; }
}
