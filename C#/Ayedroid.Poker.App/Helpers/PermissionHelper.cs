using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Helpers
{
    public static class PermissionHelper
    {
        private static Dictionary<ParticipantType, Permission[]> _participantPermissions = new()
        {
            {
                ParticipantType.Viewer,
                new Permission[] {
                    Permission.ViewTopics
            }},
            {
                ParticipantType.Voter,
                new Permission[] {
                    Permission.ViewTopics,
                    Permission.CastVotes
            }},
            {
                ParticipantType.Moderator,
                new Permission[] {
                    Permission.ViewTopics,
                    Permission.CastVotes,
                    Permission.RaiseTopics,
                    Permission.CompleteTopicVotesEarly
            }},
            {
                ParticipantType.Owner,
                new Permission[] {
                    Permission.ViewTopics,
                    Permission.CastVotes,
                    Permission.RaiseTopics,
                    Permission.CompleteTopicVotesEarly,
                    Permission.EndSession
            }}
        };

        public static bool HasPermission(ParticipantType participantType, params Permission[] permissions)
        {
            if (!_participantPermissions.ContainsKey(participantType))
                throw new ArgumentException($"{participantType} does not exist.");

            return _participantPermissions[participantType].Intersect(permissions).Count() == permissions.Length;
        }
    }
}
