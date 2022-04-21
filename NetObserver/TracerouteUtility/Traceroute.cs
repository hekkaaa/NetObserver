using System;
using NetObserver.PingUtility;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NetObserver.TracerouteUtility
{
    /// <summary>
    /// Allows an application to determine a route to a destination by sending ICMP (Internet Control Protocol) echo packets to the destination.
    /// </summary>
    public class Traceroute
    {
        private int _timeout = 4000; // default timeout https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping
        private int _maxttl = 30;
        private byte[] _buffer = new byte[32]; // default value byte https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping 
        private bool _fragment = false;

        /// <summary>
        /// Takes an attempt to determine the route path to the specified network node or workstation by sending an ECHO message of the ICMP protocol.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>Returns an object IEnumerableable <see cref="string"/> representing the list of IP addresses of the entire route.</returns>
        public IEnumerable<string> GetIpTraceRoute(string hostname)
        {
            PingIcmp pingSender = new PingIcmp();
            List<string> resultList = new List<string>();

            for (var ttl = 1; ttl <= _maxttl; ttl++)
            {
                PingReply reply = pingSender.PingRequest(hostname, _timeout, _buffer, new PingOptions { Ttl = ttl, DontFragment = _fragment });
                if (reply.Status == IPStatus.Success)
                {
                    resultList.Add(reply.Address.ToString());
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    resultList.Add(reply.Address.ToString());
                }
            }
            return resultList;
        }

        /// <summary>
        /// Takes an attempt to determine the route path to the specified network node or workstation by sending an ECHO message of the ICMP protocol.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <param name="buffer">A Byte[] array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
        /// <param name="dontFragmentint">DontFragment = Packet fragmentation (Default default = (bool) true).</param>
        /// <param name="ttl">TTL = initial value at the beginning of the trace (Default value = (int) 1).</param>
        /// <param name="maxTll">Maximum burst lifetime (TTL).</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>Returns an object IEnumerableable <see cref="string"/> representing the list of IP addresses of the entire route.</returns>
        public IEnumerable<string> GetIpTraceRoute(string hostname, int timeout, byte[] buffer, bool dontFragmentint = true, int ttl = 1, int maxTll = 30)
        {
            PingIcmp pingSender = new PingIcmp();
            List<string> resultList = new List<string>();

            for (var innerTtl = ttl; innerTtl <= maxTll; innerTtl++)
            {
                PingOptions inneroptions = new PingOptions() { Ttl = innerTtl, DontFragment = dontFragmentint };
                PingReply reply = pingSender.PingRequest(hostname, timeout, buffer, inneroptions);
                if (reply.Status == IPStatus.Success)
                {
                    resultList.Add(reply.Address.ToString());
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    resultList.Add(reply.Address.ToString());
                }
            }
            return resultList;
        }

        /// <summary>
        /// Takes an attempt to determine the route path to the specified network node or workstation with a detailed answer, sending an ECHO ICMP message protocol.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>Returns the IEnumerable <see cref="PingReply"/> object with a detailed description of each route step.</returns>
        public IEnumerable<PingReply> GetDetailTraceRoute(string hostname)
        {
            PingIcmp pingSender = new PingIcmp();
            List<PingReply> resultList = new List<PingReply>();

            for (var ttl = 1; ttl <= _maxttl; ttl++)
            {
                PingReply reply = pingSender.PingRequest(hostname, _timeout, _buffer, new PingOptions { Ttl = ttl, DontFragment = _fragment });
                if (reply.Status == IPStatus.Success)
                {
                    PingReply result = pingSender.PingRequest(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    PingReply result = pingSender.PingRequest(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        /// <summary>
        /// Takes an attempt to determine the route path to the specified network node or workstation with a detailed answer, sending an ECHO ICMP message protocol.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <param name="buffer">A Byte[] array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
        /// <param name="dontFragmentint">DontFragment = Packet fragmentation (Default default = (bool) true).</param>
        /// <param name="ttl">TTL = initial value at the beginning of the trace (Default value = (int) 1).</param>
        /// <param name="maxTll">Maximum burst lifetime (TTL)</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>Returns the IEnumerable <see cref="PingReply"/> object with a detailed description of each route step.</returns>
        public IEnumerable<PingReply> GetDetailTraceRoute(string hostname, int timeout, byte[] buffer, bool dontFragmentint = true, int ttl = 1, int maxTll = 30)
        {
            PingIcmp pingSender = new PingIcmp();
            List<PingReply> resultList = new List<PingReply>();

            for (var innerTtl = ttl; innerTtl <= maxTll; innerTtl++)
            {
                PingOptions inneroptions = new PingOptions() { Ttl = innerTtl, DontFragment = dontFragmentint };
                PingReply reply = pingSender.PingRequest(hostname, timeout, buffer, inneroptions);
                if (reply.Status == IPStatus.Success)
                {
                    PingReply result = pingSender.PingRequest(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    PingReply result = pingSender.PingRequest(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                }
            }
            return resultList;
        }
    }
}
