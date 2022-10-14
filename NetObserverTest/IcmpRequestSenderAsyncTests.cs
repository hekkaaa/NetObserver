using NetObserver.PingUtility;
using NUnit.Framework;
using System;
using System.Net.NetworkInformation;
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
            PingReply actual = await _pingIcmpAsync!.RequestIcmpAsync(hostname);

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
            PingReply actual = await _pingIcmpAsync!.RequestIcmpAsync(hostname);

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
            PingReply actual = await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout);

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
            PingReply actual = await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual(expectationDelay, actual.RoundtripTime);
        }

        [Test]
        public Task RequestPingAsyncTest_WhenHostnameIsBad_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";

            // Act

            //// Assert
            Assert.ThrowsAsync<PingException>(async () => await _pingIcmpAsync!.RequestIcmpAsync(hostname));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestPingAsyncTest_WhenNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _pingIcmpAsync!.RequestIcmpAsync(hostname));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestPingAsyncTest_WhenHostnameIsBadWithActualTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int timeout = 2000;

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestPingAsyncTest_WhenNullHostnameWithActualTimeout_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 2000;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestPingAsyncTest_WhenLocalhostWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string? hostname = "localhost";
            int timeout = -1;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestPingAsyncTest_WhenLocalhostWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string? hostname = "localhost";
            int timeout = 0;

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestIcmpAsyncTest_WhenFullSetOfArgumentsWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int timeout = 2000;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout, buffer, options));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestIcmpAsyncTest_WhenFullSetOfArgumentsWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 2000;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async() => await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout, buffer, options));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestIcmpAsyncTest_WhenFullSetOfArgumentsWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = -10; // default timeout
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async() => await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout, buffer, options));
            return Task.CompletedTask;
        }

        [Test]
        public Task RequestIcmpAsyncTest_WhenFullSetOfArgumentsWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 0; // zero timeout
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async() => await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout, buffer, options));
            return Task.CompletedTask;
        }

        [Test]
        public async Task RequestIcmpAsyncTest_WhenFullSetOfArgumentsWithBadBuffer()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000;
            byte[] buffer = new byte[0];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act
            PingReply actual = await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout, buffer, options);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(buffer, actual.Buffer);
        }

        [Test]
        public async Task RequestIcmpAsyncTest_WhenFullSetOfArgumentsWithLowTtl()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 1, DontFragment = true };

            // Act
            PingReply actual = await _pingIcmpAsync!.RequestIcmpAsync(hostname, timeout, buffer, options);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(null, actual.Options);
            Assert.AreEqual(new byte[0], actual.Buffer);
            Assert.AreEqual(0, actual.RoundtripTime);
            Assert.AreEqual(IPStatus.TtlExpired, actual.Status);
        }
    }
}
