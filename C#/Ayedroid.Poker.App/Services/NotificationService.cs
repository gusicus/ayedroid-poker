using Ayedroid.Poker.Hubs;
using Ayedroid.Poker.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Ayedroid.Poker.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Implementing INotificationClient is really just a tricksy way to try ensure all client endpoints are covered, although
    /// there is nothing forcing the methods here to actually make the call to the SignalR Client.
    /// </remarks>
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IHubContext<NotificationHub, INotificationClient> _notificationHub;

        public NotificationService(ILogger<NotificationService> logger, IHubContext<NotificationHub, INotificationClient> notificationHub)
        {
            _logger = logger;
            _notificationHub = notificationHub;
        }

        public async Task ParticipantJoined()
        {
            await _notificationHub.Clients.All.ParticipantJoined();
        }

        public async Task ParticipantLeft()
        {
            await _notificationHub.Clients.All.ParticipantLeft();
        }

        public async Task SessionEnded()
        {
            await _notificationHub.Clients.All.SessionEnded();
        }
    }
}
