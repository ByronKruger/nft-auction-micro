using Microsoft.AspNetCore.Identity;

namespace NftApi;

public class Role : IdentityRole<int>
{
    public ICollection<RoleUser> UserRoles { get; set; }
}
