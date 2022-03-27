using Ayedroid.Poker.App.Hubs;
using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;
using Microsoft.AspNetCore.SignalR;

namespace Ayedroid.Poker.App.Services
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

        public async Task NewTopic(string sessionId, Topic topic)
        {
            await _notificationHub.Clients.All.NewTopic(sessionId, topic);
        }

        public async Task ParticipantJoined(string sessionId, ParticipantDto participant)
        {
            await _notificationHub.Clients.All.ParticipantJoined(sessionId, participant);
        }

        public async Task ParticipantLeft(string sessionId, ParticipantDto participant)
        {
            await _notificationHub.Clients.All.ParticipantLeft(sessionId, participant);
        }

        public async Task SessionEnded()
        {
            await _notificationHub.Clients.All.SessionEnded();
        }
    }
}
