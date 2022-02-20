using Ayedroid.Poker.Exceptions;
using Ayedroid.Poker.Interfaces;
using Ayedroid.Poker.Models;
using Ayedroid.Poker.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ayedroid.Poker.Test
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
            _sessionService = new SessionService(loggerMock.Object, notificationService.Object);
        }

        [TestMethod]
        public void CanAddSession()
        {
            Guid newId = _sessionService.AddSession("my new session");
            Assert.IsNotNull(newId);
        }

        [TestMethod]
        public void CanRetrieveSession()
        {
            SeedSessions();

            Guid newId = _sessionService.AddSession("my new session");

            try
            {
                Session session = _sessionService.GetSession(newId.ToString());
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
            Guid newId = _sessionService.AddSession("my new session");

            // Check session exists
            try
            {
                Session session = _sessionService.GetSession(newId.ToString());
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
            List<Guid> ids = SeedSessions();

            Assert.IsTrue(ids.Distinct().Count() == ids.Count);
        }

        /// <summary>
        /// Add a whole bunch of sessions for to test correct session is found and not just the only one
        /// </summary>
        private List<Guid> SeedSessions()
        {
            List<Guid> ids = new();
            for (int i = 0; i < 50; i++)
            {
                // Add session
                ids.Add(_sessionService.AddSession($"my new session {i}"));
            }

            return ids;
        }
    }
}