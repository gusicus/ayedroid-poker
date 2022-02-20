using Ayedroid.Poker.Helpers;
using Ayedroid.Poker.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ayedroid.Poker.Test
{
    [TestClass]
    public class PermissionHelperTests
    {
        [TestMethod]
        public void SinglePermission()
        {
            Assert.IsTrue(PermissionHelper.HasPermission(ParticipantType.Viewer, Permission.ViewTopics));
            Assert.IsFalse(PermissionHelper.HasPermission(ParticipantType.Viewer, Permission.EndSession));
        }

        [TestMethod]
        public void MultiplePermissions()
        {
            Assert.IsTrue(PermissionHelper.HasPermission(ParticipantType.Owner, Permission.ViewTopics, Permission.CompleteTopicVotesEarly, Permission.EndSession));
            Assert.IsFalse(PermissionHelper.HasPermission(ParticipantType.Viewer, Permission.CastVotes, Permission.CompleteTopicVotesEarly, Permission.EndSession));
        }

        [TestMethod]
        public void UnknownParticipantType()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                PermissionHelper.HasPermission(ParticipantType.None);
            });
        }
    }
}