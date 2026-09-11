using NetObserver.Common;
using System.Net;

namespace NetObserver.DnsUtility
{
    /// <summary>
    /// Breaks down simple domain name resolution functionality into separate tasks.
    /// </summary>
    public static class DnsInfo
    {
        /// <summary>
        /// Gets the DNS name of the host.
        /// </summary>
        /// <remarks>The method makes sense to use when getting the hostname from an IP address.</remarks>
        /// <param name="hostname">The address of the remote host that you want to get information about.</param>
        /// <exception cref="System.ArgumentNullException">Hostname is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The hostname parameter is longer than 255 characters.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Hostname resolves with an error.</exception>
        /// <exception cref="System.ArgumentException">Hostname is an invalid IP address.</exception>
        /// <returns>A <see cref="string"/> that contains the primary host name for the server.</returns>
        public static string DnsHostname(string hostname) =>
            ExceptionHelper.Execute(() => Dns.GetHostEntry(hostname).HostName, nameof(DnsHostname));

        /// <summary>
        /// Gets a list of aliases that are associated with a host.
        /// </summary>
        /// <remarks>It makes sense to use this method when specifying a hostname, not an IP address.</remarks>
        /// <param name="hostname">The address of the remote host that you want to get information about.</param>
        /// <exception cref="System.ArgumentNullException">Hostname is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The hostname parameter is longer than 255 characters.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Hostname resolves with an error.</exception>
        /// <exception cref="System.ArgumentException">Hostname is an invalid IP address.</exception>
        /// <returns>A <see cref="T:string[]"/> containing DNS aliases for the host.</returns>
        public static string[] DnsAliases(string hostname) =>
            ExceptionHelper.Execute(() => Dns.GetHostEntry(hostname).Aliases, nameof(DnsAliases));

        /// <summary>
        /// Gets a list of IP addresses that are associated with a host.
        /// </summary>
        /// <param name="hostname">The address of the remote host that you want to get information about.</param>
        /// <exception cref="System.ArgumentNullException">Hostname is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The hostname parameter is longer than 255 characters.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Hostname resolves with an error.</exception>
        /// <exception cref="System.ArgumentException">Hostname is an invalid IP address.</exception>
        /// <returns>An array of <see cref="T:IPAddress[]"/> that contains the IP addresses that resolve to the host name.</returns>
        public static IPAddress[] DnsAddressList(string hostname) =>
            ExceptionHelper.Execute(() => Dns.GetHostEntry(hostname).AddressList, nameof(DnsAddressList));
    }
}
