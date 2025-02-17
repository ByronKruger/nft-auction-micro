namespace NftApi;

public class UserDto
{
    public int Id { get; set; }
    // public Wallet Wallet { get; set; }
    public List<NftDto> Nfts { get; set; }
    public Wallet Wallet { get; set; }
    public string Username {get; set;}
}
