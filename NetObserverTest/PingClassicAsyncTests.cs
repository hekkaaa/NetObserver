using NetObserver.PingUtility;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetObserverTest
{
    public class PingClassicAsyncTests
    {
        private PingClassicAsync? _pingClassicAsync;

        [SetUp]
        public void Setup()
        {
            _pingClassicAsync = new PingClassicAsync();
        }

        [Test]
        public async Task PingRequestAsyncTest_WhenHostnameGoogle()
        {
            // Arrange
            string hostname = "google.com";
            int countItemRepeat = 4; // defaut repeat for CMD.
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = await _pingClassicAsync!.RequestPingAsync(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public async Task PingRequestAsyncTest_WhenHostnameLocalhost()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 4; // defaut repeat for CMD.
            int expectationDelay = 0; // default local time delay.
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = await _pingClassicAsync!.RequestPingAsync(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(countItemRepeat, actual.Count);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(expectationDelay, actual[0].RoundtripTime);
        }

        [Test]
        public async Task PingRequestAsyncTest_WhenRepeatWithRepeatbyUser()
        {
            // Arrange
            string hostname = "google.com";
            int countItemRepeat = 6; // user repeat
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = await _pingClassicAsync!.RequestPingAsync(hostname, countItemRepeat);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public async Task PingRequestAsyncTest_WhenFullSetOfArguments()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // user repeat
            int valueTimeout = 2000;
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = await _pingClassicAsync!.RequestPingAsync(hostname, valueTimeout, countItemRepeat);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public async Task PingRequestAsyncTest_WhenFullSetOfArgumentsWithHostnameLocalhost()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 2000;
            IPStatus expectedStatus = IPStatus.Success;

            // Act
            List<PingReply> actual = await _pingClassicAsync!.RequestPingAsync(hostname, valueTimeout, countItemRepeat);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedStatus, actual[0].Status);
            Assert.AreEqual(countItemRepeat, actual.Count);
        }

        [Test]
        public Task PingRequestAsyncTest_WhenFullSetOfArgumentsWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 2000;

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async() => await _pingClassicAsync!.RequestPingAsync(hostname, valueTimeout, countItemRepeat));
            return Task.CompletedTask;
        }

        [Test]
        public Task PingRequestAsyncTest_WhenFullSetOfArgumentsWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 2000;
            PingClassicAsync itemClass = new PingClassicAsync();

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _pingClassicAsync!.RequestPingAsync(hostname, valueTimeout, countItemRepeat));
            return Task.CompletedTask;
        }

        [Test]
        public Task PingRequestAsyncTest_WhenFullSetOfArgumentsWithZeroTimeout_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = 0;
            PingClassicAsync itemClass = new PingClassicAsync();

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async() => await _pingClassicAsync!.RequestPingAsync(hostname, valueTimeout, countItemRepeat));
            return Task.CompletedTask;
        }

        [Test]
        public Task PingRequestAsyncTest_WhenFullSetOfArgumentsWithNegativeTimeout_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "localhost";
            int countItemRepeat = 10; // defaut repeat for CMD.
            int valueTimeout = -10;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async() => await _pingClassicAsync!.RequestPingAsync(hostname, valueTimeout, countItemRepeat));
            return Task.CompletedTask;
        }

        [Test]
        public Task PingRequestAsyncTest_WhenHosnameAndRepeatWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";
            int countItemRepeat = 4;

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async() => await _pingClassicAsync!.RequestPingAsync(hostname, countItemRepeat));
            return Task.CompletedTask;
        }

        [Test]
        public Task PingRequestAsyncTest_WhenHosnameAndRepeatWithNullHost_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;
            int countItemRepeat = 4;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _pingClassicAsync!.RequestPingAsync(hostname, countItemRepeat));
            return Task.CompletedTask;
        }

        [Test]
        public Task PingRequestAsyncTest_WhenHosnameWithBadHostname_ShouldThrowPingException()
        {
            // Arrange
            string hostname = "aaaaaaaaaaatestnonsite1111.com";

            // Act

            // Assert
            Assert.ThrowsAsync<PingException>(async() => await _pingClassicAsync!.RequestPingAsync(hostname));
            return Task.CompletedTask;
        }

        [Test]
        public Task PingRequestTest_WhenHosnameWithNullHostname_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? hostname = null;

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async() => await _pingClassicAsync!.RequestPingAsync(hostname));
            return Task.CompletedTask;
        }
    }
}
