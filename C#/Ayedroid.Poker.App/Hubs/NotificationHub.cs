using Ayedroid.Poker.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Ayedroid.Poker.Hubs
{
    public class NotificationHub : Hub<INotificationClient>
    {
        private const string UsersGroup = "Users";

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UsersGroup);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, UsersGroup);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
