using NetObserver.IpAdressUtility;
using NetObserver.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetObserverTest
{
    public class OpenPortTests
    {

        private OpenPort? _openPort;

        [SetUp]
        public void Setup()
        {
            _openPort = new OpenPort();
        }

        [Test]
        public void GetOpenPortTest()
        {
            // Arrange
            string hostname = "google.com";
            int port = 80;

            // Act
            PortReply actual = _openPort!.GetOpenPort(hostname, port);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(port, actual.Port);
            Assert.AreEqual(PortStatus.Open, actual.Status);
        }

        [Test]
        public void GetOpenPortTest_WhenPortIsClosed()
        {
            // Arrange
            string hostname = "google.com";
            int port = 81;

            // Act
            PortReply actual = _openPort!.GetOpenPort(hostname, port);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(port, actual.Port);
            Assert.AreEqual(PortStatus.Closed, actual.Status);
        }

        [Test]
        public void GetOpenPortTest_WhenHostnameIsBad()
        {
            // Arrange
            string hostname = "googlenewnametest12amazing113.com";
            int port = 80;

            // Act
            PortReply actual = _openPort!.GetOpenPort(hostname, port);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(port, actual.Port);
            Assert.AreEqual(PortStatus.Closed, actual.Status);
        }

        [Test]
        public void GetOpenPortTest_WhenStartAndEndPorts()
        {
            // Arrange
            string hostname = "google.com";
            int startPort = 80;
            int endPort = 82;

            // Act
            List<PortReply> actual = _openPort!.GetOpenPort(hostname, startPort, endPort);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(startPort, actual.FirstOrDefault()?.Port);
            Assert.AreEqual(PortStatus.Open, actual.FirstOrDefault()?.Status);
            Assert.AreEqual(endPort, actual.LastOrDefault()?.Port);
            Assert.AreEqual(PortStatus.Closed, actual.LastOrDefault()?.Status);
        }

        [Test]
        public void GetOpenPortTest_WhenStartAndEndPortsWithHostnameIsBad()
        {
            // Arrange
            string hostname = "googlenewnametest12amazing113.com";
            int startPort = 80;
            int endPort = 82;

            // Act
            List<PortReply> actual = _openPort!.GetOpenPort(hostname, startPort, endPort);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(startPort, actual.FirstOrDefault()?.Port);
            Assert.AreEqual(PortStatus.Closed, actual.FirstOrDefault()?.Status);
            Assert.AreEqual(endPort, actual.LastOrDefault()?.Port);
            Assert.AreEqual(PortStatus.Closed, actual.LastOrDefault()?.Status);
        }

        [Test]
        public void GetOpenPortTest_WhenPortIsNegativeValue_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int port = -12;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, port));
        }

        [Test]
        public void GetOpenPortTest_WhenValuePortIsZero_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int port = 0;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, port));
        }

        [Test]
        public void GetOpenPortTest_WhenValuePortIsMoreRange_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com";
            int port = 77777;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, port));
        }


        [Test]
        public void GetOpenPortTest_WhenStartAndEndPortsWithStartPortIsNegativeValue_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com.com";
            int startPort = -80;
            int endPort = 82;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, startPort, endPort));
        }

        [Test]
        public void GetOpenPortTest_WhenStartAndEndPortsWithLastPortIsNegativeValue_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com.com";
            int startPort = 80;
            int endPort = -82;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, startPort, endPort));
        }

        [Test]
        public void GetOpenPortTest_WhenStartAndEndPortsWithStartPortIsZeroValue_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com.com";
            int startPort = 0;
            int endPort = 82;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, startPort, endPort));
        }

        [Test]
        public void GetOpenPortTest_WhenStartAndEndPortsWithLastPortIsZeroValue_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com.com";
            int startPort = 80;
            int endPort = 0;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, startPort, endPort));
        }

        [Test]
        public void GetOpenPortTest_WhenStartAndEndPortsWithLastPortIsMoreRange_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com.com";
            int startPort = 80;
            int endPort = 77777;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, startPort, endPort));
        }

        [Test]
        public void GetOpenPortTest_WhenStartAndEndPortsWithStartPortIsMoreRange_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com.com";
            int startPort = 77777;
            int endPort = 83;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, startPort, endPort));
        }

        [Test]
        public void GetOpenPortTest_WhenStartAndEndPortsWithStartValuePortMoreLastValuePort_ShouldArgumentOutOfRangeException()
        {
            // Arrange
            string hostname = "google.com.com";
            int startPort = 145;
            int endPort = 83;

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _openPort!.GetOpenPort(hostname, startPort, endPort));
        }
    }
}
