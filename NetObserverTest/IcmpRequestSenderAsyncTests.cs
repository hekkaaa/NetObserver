using NetObserver.PingUtility;
using NUnit.Framework;
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetObserverTest
{
    public class IcmpRequestSenderAsyncTests
    {
        private IcmpRequestSenderAsync? _pingIcmpAsync;

        [SetUp]
        public void Setup()
        {
            _pingIcmpAsync = new IcmpRequestSenderAsync();
        }

        [Test]
        public async Task RequestPingAsyncTest_WhenHostnameGoogle()
        {
            // Arrange
            string hostname = "google.com";
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            PingReply actual = await _pingIcmpAsync!.RequestPingAsync(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
        }

        [Test]
        public async Task RequestPingAsyncTest_WhenHostnameLocalhost()
        {
            // Arrange
            string hostname = "localhost";
            IPStatus expectedStatus = IPStatus.Success;
            int expectationDelay = 0; // default local time delay.

            // Act
            PingReply actual = await _pingIcmpAsync!.RequestPingAsync(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual(expectationDelay, actual.RoundtripTime);
        }

        [Test]
        public async Task RequestPingAsyncTest_WhenHostnameGoogleWithTimeout()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000;
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            PingReply actual = await _pingIcmpAsync!.RequestPingAsync(hostname, timeout);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
        }

        [Test]
        public async Task RequestPingAsyncTest_WhenLocalhostWithTimeout()
        {
            // Arrange
            string hostname = "localhost";
            int timeout = 2000;
            IPStatus expectedStatus = IPStatus.Success;
            int expectationDelay = 0; // default local time delay.

            // Act
            PingReply actual = await _pingIcmpAsync!.RequestPingAsync(hostname, timeout);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual(expectationDelay, actual.RoundtripTime);
        }

        [Test]
        public void RequestPingAsyncTest_WhenHostnameIsBad_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";

            // Act

            //// Assert
            Assert.ThrowsAsync<PingException>(async () => await _pingIcmpAsync!.RequestPingAsync(hostname));
        }

        [Test]
        public void RequestPingAsyncTest_WhenNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _pingIcmpAsync!.RequestPingAsync(hostname));
        }

        [Test]
        public void RequestPingAsyncTest_WhenHostnameIsBadWithActualTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int timeout = 2000;

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _pingIcmpAsync!.RequestPingAsync(hostname, timeout));
        }

        [Test]
        public void RequestPingAsyncTest_WhenNullHostnameWithActualTimeout_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 2000;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async() => await _pingIcmpAsync!.RequestPingAsync(hostname, timeout));
        }

        [Test]
        public void RequestPingAsyncTest_WhenLocalhostWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string? hostname = "localhost";
            int timeout = -1;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _pingIcmpAsync!.RequestPingAsync(hostname, timeout));
        }

        [Test]
        public void RequestPingAsyncTest_WhenLocalhostWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string? hostname = "localhost";
            int timeout = 0;

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _pingIcmpAsync!.RequestPingAsync(hostname, timeout));
        }
    }
}
