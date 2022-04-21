using NUnit.Framework;
using NetObserver.PingUtility;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System;

namespace NetObserverTest
{
    public class PingClassicTests
    {
        private PingClassic? _pingClassic;
        
        [SetUp]
        public void Setup()
        {
            _pingClassic = new PingClassic();
        }

        [Test]
        public void PingRequest_OnlyHostname_Test()
        {
            // Arrange
            string hostname = "google.com";
            int countItemRepeat = 4; // defaut repeat for CMD.
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = _pingClassic!.RequestPing(hostname);

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

            // Act
            List<PingReply> actual = _pingClassic!.RequestPing(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(countItemRepeat, actual.Count);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(expectationDelay, actual[0].RoundtripTime);
        }

        [Test]
        public void PingRequest_HostnameAndRepeat_Test()
        {
            // Arrange
            string hostname = "google.com";
            int countItemRepeat = 6; // defaut repeat for CMD.
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = _pingClassic!.RequestPing(hostname, countItemRepeat);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public void PingRequest_Hostname_Timeout_Repeat_Test()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 2000;
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = _pingClassic!.RequestPing(hostname, valueTimeout, countItemRepeat);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public void PingRequest_Localhost_Timeout_Repeat_Test()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 2000;
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = _pingClassic!.RequestPing(hostname, valueTimeout, countItemRepeat);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public void PingRequest_BadHostname_Timeout_Repeat_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 2000;
           
            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingClassic!.RequestPing(hostname, valueTimeout, countItemRepeat));
        }

        [Test]
        public void PingRequest_NullHostname_Timeout_Repeat_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 2000;
            PingClassic itemClass = new PingClassic();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingClassic!.RequestPing(hostname, valueTimeout, countItemRepeat));
        }

        [Test]
        public void PingRequest_Localhost_ZeroTimeout_Repeat_NegativeTest()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 0;
            PingClassic itemClass = new PingClassic();

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingClassic!.RequestPing(hostname, valueTimeout, countItemRepeat));
        }

        [Test]
        public void PingRequest_Localhost_NegativeTimeout_Repeat_NegativeTest()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = -10;
         
            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _pingClassic!.RequestPing(hostname, valueTimeout, countItemRepeat));
        }

        [Test]
        public void PingRequest_BadHostnameAndRepeat_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int countItemRepeat = 4;

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingClassic!.RequestPing(hostname, countItemRepeat));
        }

        [Test]
        public void PingRequest_NullHostnameAndRepeat_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            int countItemRepeat = 4; 

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingClassic!.RequestPing(hostname, countItemRepeat));
        }

        [Test]
        public void PingRequest_BadHostname_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingClassic!.RequestPing(hostname));
        }

        [Test]
        public void PingRequest_NullHostname_NegativeTest()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingClassic!.RequestPing(hostname));
        }
    }
}