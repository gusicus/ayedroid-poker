using Ayedroid.Poker.Classes;
using Ayedroid.Poker.Exceptions;
using Ayedroid.Poker.Hubs;
using Ayedroid.Poker.Models;
using Microsoft.AspNetCore.SignalR;
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
        private SessionContainer _sessionContainer;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [TestInitialize]
        public void Init()
        {
            var loggerMock = new Mock<ILogger<SessionContainer>>();
            _sessionContainer = new SessionContainer(loggerMock.Object);
        }

        [TestMethod]
        public void CanAddSession()
        {
            Guid newId = _sessionContainer.AddSession("my new session");
            Assert.IsNotNull(newId);
        }

        [TestMethod]
        public void CanRetrieveSession()
        {
            SeedSessions();

            Guid newId = _sessionContainer.AddSession("my new session");

            try
            {
                Session session = _sessionContainer.GetSession(newId.ToString());
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
                _sessionContainer.GetSession("abc");
            });
        }

        [TestMethod]
        public void CanEndSession()
        {
            // Add session
            Guid newId = _sessionContainer.AddSession("my new session");

            // Check session exists
            try
            {
                Session session = _sessionContainer.GetSession(newId.ToString());
            }
            catch (SessionNotFoundException)
            {
                Assert.Fail("Session should exist");
            }

            _sessionContainer.EndSession(newId.ToString());

            Assert.ThrowsException<SessionNotFoundException>(() =>
            {
                _sessionContainer.GetSession(newId.ToString());
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
                ids.Add(_sessionContainer.AddSession($"my new session {i}"));
            }

            return ids;
        }
    }
}