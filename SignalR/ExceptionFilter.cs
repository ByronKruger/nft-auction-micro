namespace NftApi.SignalR;

using Microsoft.AspNetCore.SignalR;

public class ExceptionFilter : IHubFilter
{
    public async ValueTask<object> InvokeMethodAsync(
        HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
    {
        Console.WriteLine($"Calling hub method '{invocationContext.HubMethodName}'");
        try
        {
            Console.WriteLine("::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine("==================================================================");
            Console.WriteLine("::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine("==================================================================");
            return await next(invocationContext);
        }
        catch (Exception ex)
        {
            await invocationContext.Hub.Clients.Caller.SendAsync("HubException", ex.Message);
            throw;
            // Console.WriteLine($"Exception calling '{invocationContext.HubMethodName}': {ex}");
            // throw;
        }
    }
}