using NetObserver.PingUtility;
using NUnit.Framework;
using System;
using System.Net.NetworkInformation;

namespace NetObserverTest
{
    public class IcmpRequestSenderTests
    {
        private IcmpRequestSender? _pingIcmp;

        [SetUp]
        public void Setup()
        {
            _pingIcmp = new IcmpRequestSender();
        }

        [Test]
        public void RequestIcmpTest_WhenHostnameGoogle()
        {
            // Arrange
            string hostname = "google.com";
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            PingReply actual = _pingIcmp!.RequestIcmp(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
        }

        [Test]
        public void RequestIcmpTest_WhenHostnameLocalhost()
        {
            // Arrange
            string hostname = "localhost";
            IPStatus expectedStatus = IPStatus.Success;
            int expectationDelay = 0; // default local time delay.

            // Act
            PingReply actual = _pingIcmp!.RequestIcmp(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual(expectationDelay, actual.RoundtripTime);
        }


        [Test]
        public void RequestIcmpTest_WhenHostnameGoogleWithTimeout()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000;
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            PingReply actual = _pingIcmp!.RequestIcmp(hostname, timeout);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
        }

        [Test]
        public void RequestIcmpTest_WhenLocalhostWithTimeout()
        {
            // Arrange
            string hostname = "localhost";
            int timeout = 2000;
            IPStatus expectedStatus = IPStatus.Success;
            int expectationDelay = 0; // default local time delay.

            // Act
            PingReply actual = _pingIcmp!.RequestIcmp(hostname, timeout);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual(expectationDelay, actual.RoundtripTime);
        }

        [Test]
        public void RequestIcmpTest_WhenFullSetOfArguments()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000; // default timeout
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            PingReply actual = _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual(buffer, actual.Buffer);
        }

        [Test]
        public void RequestIcmpTest_WhenFullSetOfArgumentsWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int timeout = 2000;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options));
        }

        [Test]
        public void RequestIcmpTest_WhenFullSetOfArgumentsWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 2000;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options));
        }

        [Test]
        public void RequestIcmpTest_WhenFullSetOfArgumentsWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = -10; // default timeout
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options));
        }

        [Test]
        public void RequestIcmpTest_WhenFullSetOfArgumentsWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 0; // zero timeout
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options));
        }

        [Test]
        public void RequestIcmpTest_WhenFullSetOfArgumentsWithBadBuffer()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000;
            byte[] buffer = new byte[0];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act
            PingReply actual = _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(buffer, actual.Buffer);
        }

        [Test]
        public void RequestIcmpTest_WhenFullSetOfArgumentsWithLowTtl()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 1, DontFragment = true };

            // Act
            PingReply actual = _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(null, actual.Options);
            Assert.AreEqual(new byte[0], actual.Buffer);
            Assert.AreEqual(0, actual.RoundtripTime);
            Assert.AreEqual(IPStatus.TtlExpired, actual.Status);
        }

        [Test]
        public void RequestIcmpTest_WhenHostnameIsBad_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname));
        }

        [Test]
        public void RequestIcmpTest_WhenNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingIcmp!.RequestIcmp(hostname));
        }


        [Test]
        public void RequestIcmpTest_WhenHostnameIsBadWithActualTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int timeout = 2000;

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname, timeout));
        }

        [Test]
        public void RequestIcmpTest_WhenNullHostnameWithActualTimeout_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 2000;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingIcmp!.RequestIcmp(hostname, timeout));
        }

        [Test]
        public void RequestIcmpTest_WhenLocalhostWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string? hostname = "localhost";
            int timeout = -1;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _pingIcmp!.RequestIcmp(hostname, timeout));
        }

        [Test]
        public void RequestIcmpTest_WhenLocalhostWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string? hostname = "localhost";
            int timeout = 0;

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname, timeout));
        }
    }
}
