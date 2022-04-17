using NUnit.Framework;
using NetObserver.PingUtility;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System;

namespace NetObserverTest
{
    public class PingClassicTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PingRequest_OnlyHostname_Test()
        {
            // Arrange
            string hostname = "google.com";
            int countItemRepeat = 4; // defaut repeat for CMD.
            IPStatus expectedStatus = IPStatus.Success;
            PingClassic itemClass = new PingClassic();

            // Act
            List<PingReply> actual = itemClass.PingRequest(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public void PingRequest_OnlyHostname_localhost_Test()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 4; // defaut repeat for CMD.
            int expectationDelay = 0; // default local time delay.
            IPStatus expectedStatus = IPStatus.Success;
            PingClassic itemClass = new PingClassic();

            // Act
            List<PingReply> actual = itemClass.PingRequest(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(countItemRepeat, actual.Count);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(expectationDelay, actual[0].RoundtripTime);
        }

        [Test]
        public void PingRequest_HostnameLocalhostAndRepeat_Test()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // defaut repeat for CMD.
            IPStatus expectedStatus = IPStatus.Success;
            PingClassic itemClass = new PingClassic();

            // Act
            List<PingReply> actual = itemClass.PingRequest(hostname, countItemRepeat);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public void PingRequest_BadHostnameAndRepeat_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            PingClassic itemClass = new PingClassic();

            // Act

            // Assert
            Assert.Throws<PingException>(() => itemClass.PingRequest(hostname));
        }

        [Test]
        public void PingRequest_NullHostnameAndRepeat_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            PingClassic itemClass = new PingClassic();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => itemClass.PingRequest(hostname));
        }

        [Test]
        public void PingRequest_BadHostname_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            PingClassic itemClass = new PingClassic();

            // Act

            // Assert
            Assert.Throws<PingException>(() => itemClass.PingRequest(hostname));
        }

        [Test]
        public void PingRequest_NullHostname_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            PingClassic itemClass = new PingClassic();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => itemClass.PingRequest(hostname));
        }
    }
}