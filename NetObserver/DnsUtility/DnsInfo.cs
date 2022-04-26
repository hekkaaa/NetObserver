using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetObserver.DnsUtility
{
    /// <summary>
    /// Breaks down simple domain name resolution functionality into separate tasks.
    /// </summary>
    public class DnsInfo
    {
        /// <summary>
        /// Gets or sets the DNS name of the host.
        /// </summary>
        /// <remarks>The method makes sense to use when getting the hostname from the IP-address.</remarks>
        /// <param name="hostname">The address of the remote host that you want to get information about.</param>
        /// <exception cref="ArgumentNullException"> Hostname is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The hostname parameter is longer than 255 characters.</exception>
        /// <exception cref="SocketException">Hostname resolves with an error.</exception>
        /// <exception cref="ArgumentException">Hostname is an invalid IP address.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A <see cref="string"/> that contains the primary host name for the server.</returns>
        public static string DnsHostname(string hostname)
        {
            try { return Dns.GetHostEntry(hostname).HostName; }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.ParamName, ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(ex.ParamName, ex.Message);
            }
            catch (SocketException ex)
            {
                throw new SocketException(ex.ErrorCode);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.ParamName, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        /// <summary>
        /// Gets or sets a list of aliases that are associated with a host.
        /// </summary>
        /// <remarks>It makes sense to use the method when specifying the hostname, not its IP address.</remarks>
        /// <param name="hostname">The address of the remote host that you want to get information about.</param>
        /// <exception cref="ArgumentNullException"> Hostname is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The hostname parameter is longer than 255 characters.</exception>
        /// <exception cref="SocketException">Hostname resolves with an error.</exception>
        /// <exception cref="ArgumentException">Hostname is an invalid IP address.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>Returns a <see cref="string[]"/> containing DNS names converted to IP addresses.</returns>
        public static string[] DnsAliases(string hostname)
        {
            try { return Dns.GetHostEntry(hostname).Aliases; }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.ParamName, ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(ex.ParamName, ex.Message);
            }
            catch (SocketException ex)
            {
                throw new SocketException(ex.ErrorCode);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.ParamName, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }

        /// <summary>
        /// Gets or sets a list of IP addresses that are associated with a host.
        /// </summary>
        /// <param name="hostname">The address of the remote host that you want to get information about.</param>
        /// <exception cref="ArgumentNullException"> Hostname is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The hostname parameter is longer than 255 characters.</exception>
        /// <exception cref="SocketException">Hostname resolves with an error.</exception>
        /// <exception cref="ArgumentException">Hostname is an invalid IP address.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>An array of type  <see cref="IPAddress[]"/> that contains IP addresses that resolve to the host names.</returns>
        public static IPAddress[] DnsAddressList(string hostname)
        {
            try { return Dns.GetHostEntry(hostname).AddressList; }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.ParamName, ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(ex.ParamName, ex.Message);
            }
            catch (SocketException ex)
            {
                throw new SocketException(ex.ErrorCode);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.ParamName, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex.Message}");
            }
        }
    }
}
