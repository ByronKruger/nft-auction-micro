using NftApi.Data;
using NftApi.Interfaces;
using NftApi.Entities;
using Microsoft.AspNetCore.SignalR;

namespace NftApi.SignalR;

public class PresenceHub
(IAuctionRepository auctionRepo,
    IUserRepository userRepo, INftRepository nftRepo)
    : Hub
{
    public override async Task OnConnectedAsync()
    {
        var user = await userRepo.GetUserByUsernameAsync(Context.User.GetUsername());

        // if (nftId == null && charityId == null) await Clients.AddToGroupAsync(Context.Connec);

        await Groups.AddToGroupAsync(Context.User.GetUsername(), Context.ConnectionId);
        // await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // await Clients.Others.SendAsync('def');
        await base.OnDisconnectedAsync(exception);
    }
}