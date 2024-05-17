using Microsoft.AspNetCore.SignalR;

public class NotificaitionHub : Hub
{
    public async Task SendNotification(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}