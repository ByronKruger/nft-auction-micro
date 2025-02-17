using AutoMapper;
using NftApi.Entities;
using NftApi.Interfaces;

namespace NftApi;

public class AutoMapperProfiles : Profile
{
    private IUnitOfWork _uow;

    public AutoMapperProfiles(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public AutoMapperProfiles()
    {
        CreateMap<User, UserDto>();
            // .ForMember(u => u.Nfts, n => new NftDto { Id = n. });
        CreateMap<Nft, NftDto>();
        CreateMap<Bid, BidDto>()
            .ForMember(d => d.username, opt => opt.MapFrom(u => u.User.UserName));
        CreateMap<Auction, AuctionDto>()
            .ForMember(d => d.nft, opt => opt.MapFrom(n => n.nft))
            .ForMember(d => d.Charity, opt => opt.MapFrom(n => GetCharity(n.Users).UserName));
    }

    private User GetCharity(List<User> users)
    {
        Console.WriteLine("1");
        Console.WriteLine(users.Count());
        Console.WriteLine("2");
        var rolesUsers = _uow.UserRepo.GetRoleUsersQueryable();
        for (var i = 0; i < users.Count(); i++) 
        {
            rolesUsers = rolesUsers.Where(ru => ru.User.Id == users.ElementAt(i).Id);  
            var userRoles = rolesUsers.Select(ru => ru.Role);

            for (var j = 0; j < userRoles.Count(); j++)
            {
                Console.WriteLine("2");
                Console.WriteLine(userRoles.ElementAt(j).Name);
                Console.WriteLine("2");
                Console.WriteLine("userRoles.ElementAt(j).Name == 'CHARITY'");
                Console.WriteLine(userRoles.ElementAt(j).Name == "CHARITY");
                if (userRoles.ElementAt(j).Name == "CHARITY") 
                {
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>");
                    Console.WriteLine(users.ElementAt(i).Id);
                    Console.WriteLine(users.ElementAt(i).UserName);
                    return users.ElementAt(i);
                }
            }
        }
        return new();
    }
}
