using System.ComponentModel.DataAnnotations;

namespace NftApi;

public class NftMintDto
{
    // public List<int> Ids { get; set; }
    [Required]
    public int Amount {get; set;}
}
