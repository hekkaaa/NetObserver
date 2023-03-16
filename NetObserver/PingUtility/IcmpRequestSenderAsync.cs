using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NetObserver.PingUtility
{
    /// <summary>
    /// Attempts to send async an ICMP ping request message to a remote computer and receive a corresponding ICMP ping response message from it.
    /// </summary>
    public class IcmpRequestSenderAsync
    {
        /// <summary>
        /// Attempts to send async an ICMP ping request message to a remote computer and receive a corresponding ICMP ping response message from it.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="SocketException">Hostname is not a valid IP address.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A Task <see cref="PingReply"/> objects that provides information about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public async Task<PingReply> RequestIcmpAsync(string hostname)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    return await ping.SendPingAsync(hostname);
                }
                catch (ArgumentNullException ex)
                {
                    throw new ArgumentNullException("Address is null.", ex.InnerException);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException("Timeout is less than zero.", ex.Message);
                }
                catch (PingException ex)
                {
                    throw new PingException("An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.", ex.InnerException);
                }
                catch (SocketException ex)
                {
                    throw new SocketException(ex.ErrorCode);
                }
                catch (ObjectDisposedException ex)
                {
                    throw new ObjectDisposedException("This object has been disposed.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected error" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Attempts to asynchronously send an ICMP ping request message to a remote computer and receive an appropriate ICMP response message from it within the specified timeout.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="SocketException">Hostname is not a valid IP address.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A Task <see cref="PingReply"/> objects that provides information about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public async Task<PingReply> RequestIcmpAsync(string hostname, int timeout)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    return await ping.SendPingAsync(hostname, timeout);
                }
                catch (ArgumentNullException ex)
                {
                    throw new ArgumentNullException("Address is null.", ex.InnerException);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException("Timeout is less than zero.", ex.Message);
                }
                catch (PingException ex)
                {
                    throw new PingException("An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.", ex.InnerException);
                }
                catch (SocketException ex)
                {
                    throw new SocketException(ex.ErrorCode);
                }
                catch (ObjectDisposedException ex)
                {
                    throw new ObjectDisposedException("This object has been disposed.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected error" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Attempts async to send an Internet Control Message Protocol (ICMP) echo message to a remote computer and receive an appropriate ICMP echo response message from the remote computer with details of each response.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <param name="buffer">A Byte[] array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
        /// <param name="options">A PingOptions object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A Task <see cref="PingReply"/> objects that provides information about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public async Task<PingReply> RequestIcmpAsync(string hostname, int timeout, byte[] buffer, PingOptions options)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply result = await ping.SendPingAsync(hostname, timeout, buffer, options);
                    return result;
                }
                catch (ArgumentNullException ex)
                {
                    throw new ArgumentNullException(@"Hostname is null or is an empty string ("").", ex.InnerException);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException(hostname, ex.Message);
                }
                catch (PingException ex)
                {
                    throw new PingException("An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.", ex.InnerException);
                }
                catch (ObjectDisposedException ex)
                {
                    throw new ObjectDisposedException("This object has been disposed.", ex.InnerException);
                }
                catch (PlatformNotSupportedException ex)
                {
                    throw new PlatformNotSupportedException("This code does not work on the current platform", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected error" + ex.Message);
                }
            }
        }
    }
}
