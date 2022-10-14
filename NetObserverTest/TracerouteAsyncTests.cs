using NetObserver.TracerouteUtility;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetObserverTest
{
    public class TracerouteAsyncTests
    {
        private TracerouteAsync? _tracerouteAsync;

        [SetUp]
        public void Setup()
        {
            _tracerouteAsync = new TracerouteAsync();
        }

        [Test]
        public async Task GetIpTraceRouteAsyncTest()
        {
            // Arrange
            string hostname = "google.com";
            int minimalCount = 1;

            // Act
            List<string> actual = (List<string>)await _tracerouteAsync!.GetIpTraceRouteAsync(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(minimalCount < actual.Count);
        }

        [Test]
        public async Task GetIpTraceRouteAsyncTest_WhenFullSetOfArguments()
        {
            // Arrange
            string hostname = "google.com";
            int minimalCount = 1; // minimal count 'hop' for traceroute.
            int timeout = 4000; // default timeout
            bool fragment = true;
            int ttl = 1;
            byte[] buffer = new byte[32];

            // Act
            List<string> actual = (List<string>)await _tracerouteAsync!.GetIpTraceRouteAsync(hostname, timeout, buffer, fragment, ttl);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(minimalCount <= actual.Count);
        }

        [Test]
        public Task GetIpTraceRouteAsyncTest_WhenFullSetOfArgumentsWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";
            int timeout = 4000;
            bool fragment = true;
            int ttl = 1;
            byte[] buffer = new byte[32];

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _tracerouteAsync!.GetIpTraceRouteAsync(hostname, timeout, buffer, fragment, ttl));
            return Task.CompletedTask;
        }

        [Test]
        public Task GetIpTraceRouteAsyncTest_WhenFullSetOfArgumentsWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 4000;
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _tracerouteAsync!.GetIpTraceRouteAsync(hostname, timeout, buffer, fragment, ttl));
            return Task.CompletedTask;
        }

        [Test]
        public Task GetIpTraceRouteAsyncTest_WhenFullSetOfArgumentsWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = -100;
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _tracerouteAsync!.GetIpTraceRouteAsync(hostname, timeout, buffer, fragment, ttl));
            return Task.CompletedTask;
        }

        [Test]
        public Task GetIpTraceRouteAsyncTest_WhenFullSetOfArgumentsWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 0;
            byte[] buffer = new byte[32];

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _tracerouteAsync!.GetIpTraceRouteAsync(hostname, timeout, buffer));
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetIpTraceRouteAsyncTest_WhenFullSetOfArgumentsWithLowMaxTtl()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = 4000;
            byte[] buffer = new byte[32];
            int maxTtl = 3;
            bool fragment = true;
            int ttl = 1;

            // Act
            List<string> actual = (List<string>)await _tracerouteAsync!.GetIpTraceRouteAsync(hostname, timeout, buffer, fragment, ttl, maxTtl);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(maxTtl, actual.Count);
        }

        [Test]
        public Task GetIpTraceRouteAsyncTest_WhenHostnameIsBad_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _tracerouteAsync!.GetIpTraceRouteAsync(hostname));
            return Task.CompletedTask;
        }

        [Test]
        public Task GetIpTraceRouteAsyncTest_WhenNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _tracerouteAsync!.GetDetailTraceRouteAsync(hostname));
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetDetailTraceRouteAsyncTest()
        {
            // Arrange
            string hostname = "google.com";
            int maxTtl = 30;
            byte[] buffer = new byte[32]; // default buffer byte.

            // Act
            List<PingReply> actual = (List<PingReply>)await _tracerouteAsync!.GetDetailTraceRouteAsync(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(IPStatus.Success, actual.FirstOrDefault()!.Status);
            Assert.AreEqual(IPStatus.Success, actual.LastOrDefault()!.Status);
            Assert.AreEqual(buffer.Length, actual.LastOrDefault()!.Buffer.Length);
            Assert.IsTrue(maxTtl >= actual.Count);
        }


        [Test]
        public async Task GetDetailTraceRouteAsyncTest_WhenHostnameIsBad_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _tracerouteAsync!.GetDetailTraceRouteAsync(hostname));
        }

        [Test]
        public Task GetDetailTraceRouteAsyncTest_WhenNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _tracerouteAsync!.GetIpTraceRouteAsync(hostname));
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetDetailTraceRouteAsyncTest_WhenFullSetOfArguments()
        {
            // Arrange
            string hostname = "google.com";
            int maxTtl = 30;
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;
            // Act
            List<PingReply> actual = (List<PingReply>)await _tracerouteAsync!.GetDetailTraceRouteAsync(hostname, timeout, buffer, fragment, ttl);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(IPStatus.Success, actual.FirstOrDefault()!.Status);
            Assert.AreEqual(IPStatus.Success, actual.LastOrDefault()!.Status);
            Assert.AreEqual(buffer.Length, actual.LastOrDefault()!.Buffer.Length);
            Assert.IsTrue(maxTtl >= actual.Count);
        }

        [Test]
        public async Task GetDetailTraceRouteAsyncTest_WhenFullSetOfArgumentsWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _tracerouteAsync!.GetDetailTraceRouteAsync(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public async Task GetDetailTraceRouteAsyncTest_WhenFullSetOfArgumentsWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.net";
            int timeout = 4000; // default timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _tracerouteAsync!.GetDetailTraceRouteAsync(hostname, timeout, buffer, fragment, ttl));
        }

        [Test]
        public Task GetDetailTraceRouteAsyncTest_WhenFullSetOfArgumentsWithBadTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int timeout = -100; // bad timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _tracerouteAsync!.GetDetailTraceRouteAsync(hostname, timeout, buffer, fragment, ttl));
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetDetailTraceRouteAsyncTest_WhenFullSetOfArgumentsWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "localhost";
            int timeout = 0; // zero timeout
            byte[] buffer = new byte[32];
            bool fragment = true;
            int ttl = 1;

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async () => await _tracerouteAsync!.GetDetailTraceRouteAsync(hostname, timeout, buffer, fragment, ttl));
        }
    }
}
