using NetObserver.PingUtility;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NetObserver.TracerouteUtility
{
    /// <summary>
    /// Allows an application to determine a route to a destination by sending ICMP echo packets with increasing TTL values.
    /// </summary>
    public class Traceroute
    {
        private const int DefaultTimeout = 4000; // default timeout https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping
        private const int DefaultMaxTtl = 30; // matches TracerouteAsync; original Traceroute used 31, which was an inconsistency with no real effect
        private const bool DefaultFragment = false;
        private static readonly byte[] DefaultBuffer = new byte[32]; // default value byte https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping

        private readonly IcmpRequestSender _pingSender = new IcmpRequestSender();

        /// <summary>
        /// Attempts to determine the route path to the specified network node by sending ICMP echo messages with increasing TTL.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <returns>The list of IP addresses along the route, in order.</returns>
        public IEnumerable<string> GetIpTraceRoute(string hostname) =>
            TraceRouteCore(hostname, DefaultTimeout, DefaultBuffer, DefaultFragment, 1, DefaultMaxTtl);

        /// <summary>
        /// Attempts to determine the route path to the specified network host using the given ICMP echo settings.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">Maximum time to wait for a ping response, in milliseconds.</param>
        /// <param name="buffer">Data to send with the ICMP echo message. The array cannot contain more than 65,500 bytes.</param>
        /// <param name="frag">Whether packet fragmentation is disallowed (DontFragment).</param>
        /// <param name="ttl">Initial TTL value to start the trace at.</param>
        /// <param name="maxTll">Maximum TTL value before the trace gives up.</param>
        /// <returns>The list of IP addresses along the route, in order.</returns>
        public IEnumerable<string> GetIpTraceRoute(string hostname, int timeout, byte[] buffer, bool frag = DefaultFragment, int ttl = 1, int maxTll = DefaultMaxTtl) =>
            TraceRouteCore(hostname, timeout, buffer, frag, ttl, maxTll);

        /// <summary>
        /// Attempts to determine the route path to the specified network node, returning a detailed <see cref="PingReply"/> per hop.
        /// </summary>
        /// <remarks>
        /// For each responding hop, a second, direct ICMP echo (without a TTL restriction) is sent to that
        /// hop's address so the returned <see cref="PingReply"/> reflects a normal echo response (round-trip
        /// time, buffer, status) from that specific node, rather than a "TTL expired in transit" reply. This
        /// roughly doubles the ICMP traffic/time compared to <see cref="GetIpTraceRoute(string)"/>.
        /// </remarks>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <returns>A detailed <see cref="PingReply"/> per hop discovered along the route.</returns>
        public IEnumerable<PingReply> GetDetailTraceRoute(string hostname) =>
            DetailTraceRouteCore(hostname, DefaultTimeout, DefaultBuffer, DefaultFragment, 1, DefaultMaxTtl);

        /// <summary>
        /// Attempts to determine the route path to the specified network host, with a detailed <see cref="PingReply"/> per hop, using the given ICMP echo settings.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">Maximum time to wait for a ping response, in milliseconds.</param>
        /// <param name="buffer">Data to send with the ICMP echo message. The array cannot contain more than 65,500 bytes.</param>
        /// <param name="frag">Whether packet fragmentation is disallowed (DontFragment).</param>
        /// <param name="ttl">Initial TTL value to start the trace at.</param>
        /// <param name="maxTtl">Maximum TTL value before the trace gives up.</param>
        /// <returns>A detailed <see cref="PingReply"/> per hop discovered along the route.</returns>
        public IEnumerable<PingReply> GetDetailTraceRoute(string hostname, int timeout, byte[] buffer, bool frag = DefaultFragment, int ttl = 1, int maxTtl = DefaultMaxTtl) =>
            DetailTraceRouteCore(hostname, timeout, buffer, frag, ttl, maxTtl);

        private List<string> TraceRouteCore(string hostname, int timeout, byte[] buffer, bool frag, int ttl, int maxTtl)
        {
            var resultList = new List<string>();

            for (var innerTtl = ttl; innerTtl <= maxTtl; innerTtl++)
            {
                var options = new PingOptions { Ttl = innerTtl, DontFragment = frag };
                PingReply reply = _pingSender.RequestIcmp(hostname, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    resultList.Add(reply.Address.ToString());
                    break;
                }

                if (reply.Status == IPStatus.TtlExpired)
                {
                    resultList.Add(reply.Address.ToString());
                }
            }

            return resultList;
        }

        private List<PingReply> DetailTraceRouteCore(string hostname, int timeout, byte[] buffer, bool frag, int ttl, int maxTtl)
        {
            var resultList = new List<PingReply>();

            for (var innerTtl = ttl; innerTtl <= maxTtl; innerTtl++)
            {
                var options = new PingOptions { Ttl = innerTtl, DontFragment = frag };
                PingReply reply = _pingSender.RequestIcmp(hostname, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    resultList.Add(_pingSender.RequestIcmp(reply.Address.ToString(), timeout));
                    break;
                }

                if (reply.Status == IPStatus.TtlExpired)
                {
                    resultList.Add(_pingSender.RequestIcmp(reply.Address.ToString(), timeout));
                }
            }

            return resultList;
        }
    }
}
