using NetObserver.Common;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetObserver.PingUtility
{
    /// <summary>
    /// Attempts to send asynchronously an ICMP ping request message to a remote computer and receive a corresponding ICMP ping response message from it.
    /// </summary>
    public class IcmpRequestSenderAsync
    {
        /// <summary>
        /// Attempts to send asynchronously an ICMP ping request message to a remote computer and receive a corresponding ICMP ping response message from it.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="System.ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Hostname is not a valid IP address.</exception>
        /// <exception cref="System.ObjectDisposedException">This object has been disposed.</exception>
        /// <returns>A Task of <see cref="PingReply"/> that provides information about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public Task<PingReply> RequestIcmpAsync(string hostname) =>
            ExceptionHelper.ExecuteAsync(async () =>
            {
                using var ping = new Ping();
                return await ping.SendPingAsync(hostname).ConfigureAwait(false);
            }, nameof(RequestIcmpAsync));

        /// <summary>
        /// Attempts to asynchronously send an ICMP ping request message to a remote computer and receive an appropriate ICMP response message from it within the specified timeout.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <exception cref="System.ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Hostname is not a valid IP address.</exception>
        /// <exception cref="System.ObjectDisposedException">This object has been disposed.</exception>
        /// <returns>A Task of <see cref="PingReply"/> that provides information about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public Task<PingReply> RequestIcmpAsync(string hostname, int timeout) =>
            ExceptionHelper.ExecuteAsync(async () =>
            {
                using var ping = new Ping();
                return await ping.SendPingAsync(hostname, timeout).ConfigureAwait(false);
            }, nameof(RequestIcmpAsync));

        /// <summary>
        /// Attempts to asynchronously send an ICMP echo message to a remote computer and receive an appropriate ICMP echo response message from the remote computer with details of the response.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <param name="buffer">A Byte[] array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
        /// <param name="options">A PingOptions object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
        /// <exception cref="System.ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="System.ObjectDisposedException">This object has been disposed.</exception>
        /// <returns>A Task of <see cref="PingReply"/> that provides information about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public Task<PingReply> RequestIcmpAsync(string hostname, int timeout, byte[] buffer, PingOptions options) =>
            ExceptionHelper.ExecuteAsync(async () =>
            {
                using var ping = new Ping();
                return await ping.SendPingAsync(hostname, timeout, buffer, options).ConfigureAwait(false);
            }, nameof(RequestIcmpAsync));
    }
}
