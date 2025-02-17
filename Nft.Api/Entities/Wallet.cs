using NftApi.Entities;

namespace NftApi;

public class Wallet
{
    public int Id { get; set; }
    public string Address { get; set; }
    public decimal AvailableBalance { get; set; }
    public int UserId { get; set; }
}
