using System.ComponentModel.DataAnnotations;

namespace NftApi;

public class ClaimDepositDto
{
    [Required]
    public string Address { get; set; }
}
