using NftApi.Entities;

namespace NftApi.Interfaces;

public interface ITokenService
{
    public Task<string> CreateToken(User user);
}
