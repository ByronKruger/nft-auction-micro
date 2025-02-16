namespace NftApi;

public class NftP
{
    public int Id { get; set; }
    // public int Id { get; set; }
    // public int NftContractId { get; set; }
    public int UserId { get; set; }
    public Auction Auction { get; set; }
    public int? AuctionId { get; set; }
}
