using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NftApi.Data;
using NftApi.Helpers;

namespace NftApi;

public class NftRepository : INftRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public NftRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public void AddNft(Nft nft)
    {
        this._dataContext.Nfts.Add(nft);
    }

    // public async Task<bool> SaveAllAsync()
    // {
    //     return await _dataContext.SaveChangesAsync() > 0;
    // }

    public async Task<List<Nft>> GetNftsAsync()
    {
        return await _dataContext.Nfts.ToListAsync();
    }

    public async Task<List<NftDto>> GetUserNftsAsync(int userId)
    {
        // return await _dataContext.Nfts
        //     .Where(n => n.UserId == userId)
        //     .Include(n => n.Id)
        //     .ToListAsync();

        return await _dataContext.Nfts
            // .Where(n => n.UserId == userId)
            .Where(n => n.UserId == userId)
            // .Select(n => )
            .ProjectTo<NftDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
            // .Select(n => n.);
            // .ToListAsync();
        // return userNfts.Select(u => u.Nfts);
        
        // return userNfts.nft;
    }

    public async Task<Nft> GetNftsByIdAsync(int id)
    {
        return await _dataContext.Nfts.FindAsync(id);
    }

    // public async Task<List<Nft>> GetAllNfts()
    // {
    //     return await _dataContext.Nfts.ToListAsync();
    // }

    public async Task<PagedList<NftDto>> GetAllNfts(UserParams userParams, bool isStartAuction)
    {
        var query = _dataContext.Nfts.AsQueryable();

        // query = query.ProjectTo<NftDto>(_mapper.ConfigurationProvider);

        if (userParams.FilterForOwner != null) query = query.Where(nft => nft.User.UserName == userParams.FilterForOwner); 

        if (isStartAuction) query = query.Where(nft => nft.IsOwned == false);

        return await PagedList<NftDto>.CreateAsync(
            query.ProjectTo<NftDto>(_mapper.ConfigurationProvider), 
            userParams.PageNumber, 
            userParams.PageSize);
    }

    public async Task<PagedList<NftDto>> GetAllNftsForUser(UserParams userParams)
    {
        var query = _dataContext.Nfts
            .Where(nft => nft.UserId == userParams.UserId)
            .ProjectTo<NftDto>(_mapper.ConfigurationProvider);

        // if (userParams.)
        // {

        // }

        // query = query.Where()

        // .Where(0 != 1);
        return await PagedList<NftDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
    }
}
