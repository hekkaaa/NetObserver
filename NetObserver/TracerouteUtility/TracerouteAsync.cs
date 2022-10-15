using NetObserver.PingUtility;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetObserver.TracerouteUtility
{
    public class TracerouteAsync
    {
        private const int _timeout = 4000; // default timeout https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping
        private const int _maxTtl = 30;
        private byte[] _buffer = new byte[32]; // default value byte https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping 
        private const bool _fragment = false;

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
        public async Task<IEnumerable<string>> GetIpTraceRouteAsync(string hostname)
        {
            IcmpRequestSenderAsync pingSender = new IcmpRequestSenderAsync();
            List<string> resultList = new List<string>();

            for (var ttl = 1; ttl <= _maxTtl; ttl++)
            {
                PingReply reply = await pingSender.RequestIcmpAsync(hostname, _timeout, _buffer, new PingOptions { Ttl = ttl, DontFragment = _fragment });
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
        /// Attempts async to determine the route path to the specified network host or workstation by sending an ICMP ECHO message containing user-specified detailed settings.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <param name="buffer">A Byte[] array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
        /// <param name="frag">DontFragment = Packet fragmentation (Default value = (bool) true).</param>
        /// <param name="ttl">TTL = Initial value at the beginning of the trace (Default value = (int) 1).</param>
        /// <param name="maxTll">Maximum burst lifetime (TTL).</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>Returns an object Task IEnumerableable <see cref="string"/> representing the list of IP addresses of the entire route.</returns>
        public async Task<IEnumerable<string>> GetIpTraceRouteAsync(string hostname, int timeout, byte[] buffer, bool frag = _fragment, int ttl = 1, int maxTll = _maxTtl)
        {
            IcmpRequestSenderAsync pingSender = new IcmpRequestSenderAsync();
            List<string> resultList = new List<string>();

            for (var innerTtl = ttl; innerTtl <= maxTll; innerTtl++)
            {
                PingOptions innerOptions = new PingOptions() { Ttl = innerTtl, DontFragment = frag };
                PingReply reply = await pingSender.RequestIcmpAsync(hostname, timeout, buffer, innerOptions);
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
        /// Attempts async to determine a route path to a specified network host or workstation with a verbose response by sending an ICMP ECHO message containing verbose options specified by the user.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>Returns the Task IEnumerable <see cref="PingReply"/> object with a detailed description of each route step.</returns>
        public async Task<IEnumerable<PingReply>> GetDetailTraceRouteAsync(string hostname)
        {
            IcmpRequestSenderAsync pingSender = new IcmpRequestSenderAsync();
            List<PingReply> resultList = new List<PingReply>();

            for (var ttl = 1; ttl <= _maxTtl; ttl++)
            {
                PingReply reply = await pingSender.RequestIcmpAsync(hostname, _timeout, _buffer, new PingOptions { Ttl = ttl, DontFragment = _fragment });
                if (reply.Status == IPStatus.Success)
                {
                    PingReply result = await pingSender.RequestIcmpAsync(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    PingReply result = await pingSender.RequestIcmpAsync(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        /// <summary>
        /// Takes async an attempt to determine the route path to the specified network node or workstation with a detailed answer, sending an ECHO ICMP message protocol.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <param name="buffer">A Byte[] array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
        /// <param name="frag">DontFragment = Packet fragmentation (Default value = (bool) true).</param>
        /// <param name="ttl">TTL = Initial value at the beginning of the trace (Default value = (int) 1).</param>
        /// <param name="maxTtl">Maximum burst lifetime (TTL)</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>Returns the Task IEnumerable <see cref="PingReply"/> object with a detailed description of each route step.</returns>
        public async Task<IEnumerable<PingReply>> GetDetailTraceRouteAsync(string hostname, int timeout, byte[] buffer, bool frag = _fragment, int ttl = 1, int maxTtl = _maxTtl)
        {
            IcmpRequestSenderAsync pingSender = new IcmpRequestSenderAsync();
            List<PingReply> resultList = new List<PingReply>();

            for (var innerTtl = ttl; innerTtl <= maxTtl; innerTtl++)
            {
                PingOptions innerOptions = new PingOptions() { Ttl = innerTtl, DontFragment = frag };
                PingReply reply = await pingSender.RequestIcmpAsync(hostname, timeout, buffer, innerOptions);
                if (reply.Status == IPStatus.Success)
                {
                    PingReply result = await pingSender.RequestIcmpAsync(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    PingReply result = await pingSender.RequestIcmpAsync(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                }
            }
            return resultList;
        }
    }
}
