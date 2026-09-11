using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace NetObserver.IpAdressUtility
{
    /// <summary>
    /// Allows the application to determine the IP address of the local host, as well as obtain additional information from network interfaces.
    /// </summary>
    /// <remarks>
    /// Every call re-enumerates the current network interfaces; nothing is cached between
    /// calls, so results always reflect the machine's current network state and repeated
    /// calls never accumulate stale data from earlier calls.
    /// </remarks>
    public static class LocalIp
    {
        /// <summary>
        /// Attempts to obtain the IPv4 address of the local computer.
        /// </summary>
        /// <remarks>Prefers addresses with a DHCP prefix; falls back to a Manual prefix if none are found.</remarks>
        /// <returns>The first matching IP address as a <see cref="string"/>, or <see langword="null"/> if none was found.</returns>
        public static string? GetIpv4Localhost()
        {
            var dhcpAddresses = GetAddressesByPrefix(PrefixOrigin.Dhcp);
            if (dhcpAddresses.Count > 0)
            {
                return dhcpAddresses[0];
            }

            var manualAddresses = GetAddressesByPrefix(PrefixOrigin.Manual);
            return manualAddresses.Count > 0 ? manualAddresses[0] : null;
        }

        /// <summary>
        /// Gets a tuple of (prefix origin, IP address) for every matching address found across all network interfaces.
        /// </summary>
        /// <returns>A new <see cref="List{T}"/> of <see cref="Tuple{PrefixOrigin, String}"/> built fresh from the current network state.</returns>
        public static List<Tuple<PrefixOrigin, string>> GetAllIpv4NetInterface()
        {
            var result = new List<Tuple<PrefixOrigin, string>>();
            result.AddRange(GetAddressesByPrefix(PrefixOrigin.Dhcp).Select(ip => Tuple.Create(PrefixOrigin.Dhcp, ip)));
            result.AddRange(GetAddressesByPrefix(PrefixOrigin.Manual).Select(ip => Tuple.Create(PrefixOrigin.Manual, ip)));
            return result;
        }

        private static List<string> GetAddressesByPrefix(PrefixOrigin prefixOrigin)
        {
            var addresses = new List<string>();

            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                UnicastIPAddressInformation? unicast = networkInterface.GetIPProperties()
                    .UnicastAddresses
                    .LastOrDefault(a => a.PrefixOrigin == prefixOrigin);

                if (unicast?.Address != null)
                {
                    addresses.Add(unicast.Address.ToString());
                }
            }

            return addresses;
        }
    }
}
