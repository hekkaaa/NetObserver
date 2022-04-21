using NetObserver.PingUtility;
using NUnit.Framework;
using System;
using System.Net.NetworkInformation;

namespace NetObserverTest
{
    public class PingIcmpTests
    {
        private IcmpRequestSender? _pingIcmp;

        [SetUp]
        public void Setup()
        {
            _pingIcmp = new IcmpRequestSender();
        }

        [Test]
        public void PingRequest_OnlyHostnameGoogle_Test()
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
        public void PingRequest_OnlyHostnameLocalhost_Test()
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
        public void PingRequest_HostnameGoogleAndTimeout_Test()
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
        public void PingRequest_LocalhostAndTimeout_Test()
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
        public void PingRequest_FullCustom_Test()
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
        public void PingRequest_FullCustom_BadHostname_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int timeout = 2000; // default timeout
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options));
        }

        [Test]
        public void PingRequest_FullCustom_NullHostname_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            int timeout = 2000; // default timeout
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options));
        }

        [Test]
        public void PingRequest_FullCustom_BadTimeout_NegativeTest()
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
        public void PingRequest_FullCustom_ZeroTimeout_NegativeTest()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 0; // default timeout
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options));
        }

        [Test]
        public void PingRequest_FullCustom_BadBuffer_NegativeTest()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000; // default timeout
            byte[] buffer = new byte[0];
            PingOptions options = new PingOptions() { Ttl = 32, DontFragment = true };

            // Act
            PingReply actual = _pingIcmp!.RequestIcmp(hostname, timeout, buffer, options);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(buffer, actual.Buffer);
        }

        [Test]
        public void PingRequest_FullCustom_LowTtl_Test()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 2000; // default timeout
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
        public void PingRequest_BadHostname_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname));
        }

        [Test]
        public void PingRequest_NullHostname_NegativeTest()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingIcmp!.RequestIcmp(hostname));
        }


        [Test]
        public void PingRequest_BadHostnameAndActualTimeout_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int timeout = 2000;

            // Act

            // Assert
            Assert.Throws<PingException>(() => _pingIcmp!.RequestIcmp(hostname, timeout));
        }

        [Test]
        public void PingRequest_NullHostnameAndActualTimeout_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            int timeout = 2000;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _pingIcmp!.RequestIcmp(hostname, timeout));
        }

        [Test]
        public void PingRequest_LocalhostAndBadTimeout_NegativeTest()
        {
            // Arrange
            string? hostname = "localhost";
            int timeout = -1;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _pingIcmp!.RequestIcmp(hostname, timeout));
        }

        [Test]
        public void PingRequest_LocalhostAndZeroTimeout__NegativeTest()
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
