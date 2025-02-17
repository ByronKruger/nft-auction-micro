using System.Security.Claims;

namespace NftApi;

public static class UserClaimsExtension
{

    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }
}
