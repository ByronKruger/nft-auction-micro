using Microsoft.AspNetCore.Identity;
using NftApi.Entities;

namespace NftApi;

public class RoleUser : IdentityUserRole<int>
{
    public User User { get; set; }
    public Role Role { get; set; }
}
