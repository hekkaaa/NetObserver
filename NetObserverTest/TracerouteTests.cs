using NUnit.Framework;
using NetObserver.PingUtility;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System;
using System.Linq;
using NetObserver.TracerouteUtility;

namespace NetObserverTest
{
    public class TracerouteTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetIpTraceRoute_Test()
        {
            // Arrange
            string hostname = "google.com";
            int minimalCount = 1;
            Traceroute itemClass = new Traceroute();

            // Act
            List<string> actual = (List<string>)itemClass.GetIpTraceRoute(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(minimalCount < actual.Count);
        }

        [Test]
        public void GetIpTraceRoute_FullCustom_Test()
        {
            // Arrange
            string hostname = "google.com";
            int minimalCount = 1; // minimal count 'hop' for traceroute.
            int timeout = 4000; // default timeout
            bool fragment = true;
            int ttl = 1;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 30, DontFragment = true };
            Traceroute itemClass = new Traceroute();

            // Act
            List<string> actual = (List<string>)itemClass.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(minimalCount <= actual.Count);
        }

        [Test]
        public void GetIpTraceRoute_FullCustom_BadHostname_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";
            int timeout = 4000;
            bool fragment = true;
            int ttl = 1;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 30, DontFragment = true };
            Traceroute itemClass = new Traceroute();

            // Act
           
            // Assert
            Assert.Throws<PingException>(() => itemClass.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetIpTraceRoute_FullCustom_NullHostname_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            int timeout = 4000;
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            PingOptions options = new PingOptions() { Ttl = 30, DontFragment = true };
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => itemClass.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetIpTraceRoute_FullCustom_BadTimeout_NegativeTest()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = -100;
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            PingOptions options = new PingOptions() { Ttl = 30, DontFragment = true };
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => itemClass.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetIpTraceRoute_FullCustom_ZeroTimeout_NegativeTest()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 0;
            byte[] buffer = new byte[32];
            PingOptions options = new PingOptions() { Ttl = 30, DontFragment = true };
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<PingException>(() => itemClass.GetIpTraceRoute(hostname, timeout, buffer));
        }

        [Test]
        public void GetIpTraceRoute_FullCustom_LowMaxTtl_Test()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 4000;
            byte[] buffer = new byte[32];
            int maxTtl = 3;
            bool fragment = true;
            int ttl = 1;
            PingOptions options = new PingOptions() { Ttl = 1, DontFragment = true };
            Traceroute itemClass = new Traceroute();

            // Act
            List<string> actual = (List<string>)itemClass.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl, maxTtl);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(maxTtl, actual.Count);
        }

        [Test]
        public void GetIpTraceRoute_BadHostname_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<PingException>(() => itemClass.GetIpTraceRoute(hostname));
        }

        [Test]
        public void GetIpTraceRoute_NullHostname_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => itemClass.GetIpTraceRoute(hostname));
        }


        [Test]
        public void GetDetailTraceRoute_Test()
        {
            // Arrange
            string hostname = "google.com";
            int maxTtl = 30;
            byte[] buffer = new byte[32]; // default buffer byte.
            Traceroute itemClass = new Traceroute();

            // Act
            List<PingReply> actual = (List<PingReply>)itemClass.GetDetailTraceRoute(hostname);

            // Assert
            Assert.IsNotNull(actual);
        #pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            Assert.AreEqual(IPStatus.Success, actual.FirstOrDefault().Status);
            Assert.AreEqual(IPStatus.Success, actual.LastOrDefault().Status);
            Assert.AreEqual(buffer.Length, actual.LastOrDefault().Buffer.Length);
        #pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            Assert.IsTrue(maxTtl >= actual.Count);
        }


        [Test]
        public void GetDetailTraceRoute_BadHostname_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<PingException>(() => itemClass.GetDetailTraceRoute(hostname));
        }

        [Test]
        public void GetDetailTraceRoute_NullHostname_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => itemClass.GetDetailTraceRoute(hostname));
        }

        [Test]
        public void GetDetailTraceRoute_FullCustom_Test()
        {
            // Arrange
            string hostname = "google.com";
            int maxTtl = 30;
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            Traceroute itemClass = new Traceroute();

            // Act
            List<PingReply> actual = (List<PingReply>)itemClass.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl);

            // Assert
            Assert.IsNotNull(actual);
        #pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            Assert.AreEqual(IPStatus.Success, actual.FirstOrDefault().Status);
            Assert.AreEqual(IPStatus.Success, actual.LastOrDefault().Status);
            Assert.AreEqual(buffer.Length, actual.LastOrDefault().Buffer.Length);
        #pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            Assert.IsTrue(maxTtl >= actual.Count);
        }

        [Test]
        public void GetDetailTraceRoute_FullCustom_NullHostname_NegativeTest()
        {
            // Arrange
            string? hostname = null;
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => itemClass.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetDetailTraceRoute_FullCustom_BadHostname_NegativeTest()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<PingException>(() => itemClass.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetDetailTraceRoute_FullCustom_BadTimeout_NegativeTest()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = -100; // bad timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => itemClass.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetDetailTraceRoute_FullCustom_ZeroTimeout_NegativeTest()
        {
            // Arrange
            string hostname = "localhost";
            int timeout = 0; // zero timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            Traceroute itemClass = new Traceroute();

            // Act

            // Assert
            Assert.Throws<PingException>(() => itemClass.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }
    }
}
