using NftApi.Helpers;

namespace NftApi;

public interface INftRepository
{
    // Task<IEnumerable<Nft>> GetNftByIdAsync();
    // Task<bool> SaveAllAsync();
    Task<List<Nft>> GetNftsAsync();
    Task<List<NftDto>> GetUserNftsAsync(int userId);
    Task<Nft> GetNftsByIdAsync(int id);
    Task<PagedList<NftDto>> GetAllNfts(UserParams userParams, bool isStartAuction);
    // Task<List<Nft>> GetAllNfts();
    void AddNft(Nft nft);
    Task<PagedList<NftDto>> GetAllNftsForUser(UserParams userParams);
}
