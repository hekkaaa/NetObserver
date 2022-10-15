using NetObserver.PingUtility;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

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
        public void PingRequestTest_WhenHostnameGoogle()
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
        public void PingRequestTest_WhenHostnameLocalhost()
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
        public void PingRequestTest_WhenRepeatWithRepeatbyUser()
        {
            // Arrange
            string hostname = "google.com";
            int countItemRepeat = 6; // user repeat
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = _pingClassic!.RequestPing(hostname, countItemRepeat);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public void PingRequestTest_WhenFullSetOfArguments()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // user repeat
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
        public void PingRequestTest_WhenFullSetOfArgumentsWithHostnameLocalhost()
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
        public void PingRequestTest_WhenFullSetOfArgumentsWithBadHostname_ShouldThrowPingException()
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
        public void PingRequestTest_WhenFullSetOfArgumentsWithNullHostname_ShouldThrowArgumentNullException()
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
        public void PingRequestTest_WhenFullSetOfArgumentsWithZeroTimeout_ShouldThrowPingException()
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
        public void PingRequestTest_WhenFullSetOfArgumentsWithNegativeTimeout_ShouldThrowArgumentOutOfRangeException()
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
        public void PingRequestTest_WhenHosnameAndRepeatWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int countItemRepeat = 4;

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingClassic!.RequestPing(hostname, countItemRepeat));
        }

        [Test]
        public void PingRequestTest_WhenHosnameAndRepeatWithNullHost_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int countItemRepeat = 4;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingClassic!.RequestPing(hostname, countItemRepeat));
        }

        [Test]
        public void PingRequestTest_WhenHosnameWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingClassic!.RequestPing(hostname));
        }

        [Test]
        public void PingRequestTest_WhenHosnameWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingClassic!.RequestPing(hostname));
        }
    }
}