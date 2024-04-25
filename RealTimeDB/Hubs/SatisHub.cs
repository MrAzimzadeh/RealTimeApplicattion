using Microsoft.AspNetCore.SignalR;

namespace RealTimeDB.Hubs;
public class SatisHub : Hub
{
    public async Task SendMessageAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", "Hello from the hub!");
        
        Console.WriteLine("Salam");
    
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine(Context.ConnectionId);
        return base.OnConnectedAsync();
    }
}
