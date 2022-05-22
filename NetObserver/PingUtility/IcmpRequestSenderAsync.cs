using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetObserver.PingUtility
{
    /// <summary>
    /// Attempts to send async an ICMP ping request message to a remote computer and receive a corresponding ICMP ping response message from it.
    /// </summary>
    public class IcmpRequestSenderAsync
    {
        /// <summary>
        /// Attempts to  send async an ICMP ping request message to a remote computer and receive a corresponding ICMP ping response message from it.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="SocketException">Hostname is not a valid IP address.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A Task <see cref="PingReply"/> objects that provides information about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public async Task<PingReply> RequestPingAsync(string hostname)
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
                catch(SocketException ex)
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
        public async Task<PingReply> RequestPingAsync(string hostname, int timeout)
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
    }
}
