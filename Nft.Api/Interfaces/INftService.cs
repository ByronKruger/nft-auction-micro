using NftApi.Entities;
using NftApi.Helpers;

namespace NftApi;

public interface INftService
{
    // Task<List<NftDto>> MintNfts(int amount, User user);
    Task<bool> MintNfts(NftDto nftDto);
    Task<bool> ClaimDeposit(string address, User user);
    Task<List<NftDto>> GetUserNfts(User user);
    // Task<List<NftDto>> GetAllNfts();
    Task<PagedList<NftDto>> GetAllNfts(UserParams userParams, bool isStartAuction);
    //  Task<List<NftD>> GetUserNfts(User user);
    Task<NftDto> MintNftImage(User user, IFormFile file);
    Task<PagedList<NftDto>> GetAllNftsForUser(UserParams userParams);
}
