using NetObserver.PingUtility;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetObserver.TracerouteUtility
{
    /// <summary>
    /// Test traceroute class
    /// </summary>
    public class TracerouteAndroidAsync
    {
        private const int _timeout = 4000; // default timeout https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping
        private const int _maxTtl = 30;
        private byte[] _buffer = Array.Empty<byte>(); 
        private const bool _fragment = false;

        /// <summary>
        /// Test traceroute class
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
    }
}
