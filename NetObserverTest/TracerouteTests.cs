using NetObserver.TracerouteUtility;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace NetObserverTest
{
    public class TracerouteTests
    {
        private Traceroute? _traceroute;

        [SetUp]
        public void Setup()
        {
            _traceroute = new Traceroute();
        }

        [Test]
        public void GetIpTraceRouteTest()
        {
            // Arrange
            string hostname = "google.com";
            int minimalCount = 1;

            // Act
            List<string> actual = (List<string>)_traceroute!.GetIpTraceRoute(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(minimalCount < actual.Count);
        }

        [Test]
        public void GetIpTraceRouteTest_WhenFullSetOfArguments()
        {
            // Arrange
            string hostname = "google.com";
            int minimalCount = 1; // minimal count 'hop' for traceroute.
            int timeout = 4000; // default timeout
            bool fragment = true;
            int ttl = 1;
            byte[] buffer = new byte[32];

            // Act
            List<string> actual = (List<string>)_traceroute!.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(minimalCount <= actual.Count);
        }

        [Test]
        public void GetIpTraceRouteTest_WhenFullSetOfArgumentsWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";
            int timeout = 4000;
            bool fragment = true;
            int ttl = 1;
            byte[] buffer = new byte[32];

            // Act

            // Assert
            Assert.Throws<PingException>(() => _traceroute!.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetIpTraceRouteTest_WhenFullSetOfArgumentsWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 4000;
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _traceroute!.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetIpTraceRouteTest_WhenFullSetOfArgumentsWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = -100;
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _traceroute!.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetIpTraceRouteTest_WhenFullSetOfArgumentsWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 0;
            byte[] buffer = new byte[32];

            // Act

            // Assert
            Assert.Throws<PingException>(() => _traceroute!.GetIpTraceRoute(hostname, timeout, buffer));
        }

        [Test]
        public void GetIpTraceRouteTest_WhenFullSetOfArgumentsWithLowMaxTtl()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 4000;
            byte[] buffer = new byte[32];
            int maxTtl = 3;
            bool fragment = true;
            int ttl = 1;

            // Act
            List<string> actual = (List<string>)_traceroute!.GetIpTraceRoute(hostname, timeout, buffer, fragment, ttl, maxTtl);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(maxTtl, actual.Count);
        }

        [Test]
        public void GetIpTraceRouteTest_WhenBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";

            // Act
            
            // Assert
            Assert.Throws<PingException>(() => _traceroute!.GetIpTraceRoute(hostname));
        }

        [Test]
        public void GetIpTraceRouteTest_WhenNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act
            
            // Assert
            Assert.Throws<ArgumentNullException>(() => _traceroute!.GetIpTraceRoute(hostname));
        }


        [Test]
        public void GetDetailTraceRouteTest()
        {
            // Arrange
            string hostname = "google.com";
            int maxTtl = 30;
            byte[] buffer = new byte[32]; // default buffer byte.

            // Act
            List<PingReply> actual = (List<PingReply>)_traceroute!.GetDetailTraceRoute(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(IPStatus.Success, actual.FirstOrDefault()!.Status);
            Assert.AreEqual(IPStatus.Success, actual.LastOrDefault()!.Status);
            Assert.AreEqual(buffer.Length, actual.LastOrDefault()!.Buffer.Length);
            Assert.IsTrue(maxTtl >= actual.Count);
        }

        
        [Test]
        public void GetDetailTraceRouteTest_WhenBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";

            // Act
         
            // Assert
            Assert.Throws<PingException>(() => _traceroute!.GetDetailTraceRoute(hostname));
        }

        [Test]
        public void GetDetailTraceRouteTest_WhenNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act
            
            // Assert
            Assert.Throws<ArgumentNullException>(() => _traceroute!.GetDetailTraceRoute(hostname));
        }

        [Test]
        public void GetDetailTraceRouteTest_WhenFullSetOfArguments()
        {
            // Arrange
            string hostname = "google.com";
            int maxTtl = 30;
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            // Act
            List<PingReply> actual = (List<PingReply>)_traceroute!.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(IPStatus.Success, actual.FirstOrDefault()!.Status);
            Assert.AreEqual(IPStatus.Success, actual.LastOrDefault()!.Status);
            Assert.AreEqual(buffer.Length, actual.LastOrDefault()!.Buffer.Length);
            Assert.IsTrue(maxTtl >= actual.Count);
        }

        [Test]
        public void GetDetailTraceRouteTest_WhenFullSetOfArgumentsWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _traceroute!.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetDetailTraceRouteTest_WhenFullSetOfArgumentsWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act
            
            // Assert
            Assert.Throws<PingException>(() => _traceroute!.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetDetailTraceRouteTest_WhenFullSetOfArgumentsWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = -100; // bad timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act
            
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _traceroute!.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public void GetDetailTraceRouteTest_WhenFullSetOfArgumentsWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "localhost";
            int timeout = 0; // zero timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act
      
            // Assert
            Assert.Throws<PingException>(() => _traceroute!.GetDetailTraceRoute(hostname, timeout, buffer, fragment, ttl));
        }
    }
}
