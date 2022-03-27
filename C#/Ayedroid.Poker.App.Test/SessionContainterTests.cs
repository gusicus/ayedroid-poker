using Ayedroid.Poker.App.Exceptions;
using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ayedroid.Poker.App.Test
{
    [TestClass]
    public class SessionContainterTests
    {
#pragma warning disable CS8618 // Intialised in Init()
        private SessionService _sessionService;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [TestInitialize]
        public void Init()
        {
            var loggerMock = new Mock<ILogger<SessionService>>();
            var notificationService = new Mock<INotificationService>();
            var userServiceMock = new Mock<IUserService>();
            _sessionService = new SessionService(loggerMock.Object, notificationService.Object, userServiceMock.Object);
        }

        [TestMethod]
        public void CanAddSession()
        {
            string newId = _sessionService.AddSession("my new session", Array.Empty<string>());
            Assert.IsNotNull(newId);
        }

        [TestMethod]
        public void CanRetrieveSession()
        {
            SeedSessions();

            string newId = _sessionService.AddSession("my new session", Array.Empty<string>());

            try
            {
                Session session = _sessionService.GetSession(newId);
                Assert.IsNotNull(session);
                Assert.AreEqual(newId, session.Id);
            }
            catch (SessionNotFoundException)
            {
                Assert.Fail("Session should exist");
            }
        }

        [TestMethod]
        public void NonExistantSessionShouldThrow()
        {
            SeedSessions();

            Assert.ThrowsException<SessionNotFoundException>(() =>
            {
                _sessionService.GetSession("abc");
            });
        }

        [TestMethod]
        public void CanEndSession()
        {
            // Add session
            string newId = _sessionService.AddSession("my new session", Array.Empty<string>());

            // Check session exists
            try
            {
                Session session = _sessionService.GetSession(newId);
            }
            catch (SessionNotFoundException)
            {
                Assert.Fail("Session should exist");
            }

            _sessionService.EndSession(newId.ToString());

            Assert.ThrowsException<SessionNotFoundException>(() =>
            {
                _sessionService.GetSession(newId.ToString());
            });
        }

        [TestMethod]
        public void MultipleSessionsShouldHaveUniqueIds()
        {
            List<string> ids = SeedSessions();

            Assert.IsTrue(ids.Distinct().Count() == ids.Count);
        }

        /// <summary>
        /// Add a whole bunch of sessions for to test correct session is found and not just the only one
        /// </summary>
        private List<string> SeedSessions()
        {
            List<string> ids = new();
            for (int i = 0; i < 50; i++)
            {
                // Add session
                ids.Add(_sessionService.AddSession($"my new session {i}", Array.Empty<string>()));
            }

            return ids;
        }
    }
}