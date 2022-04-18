using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace NetObserver.PingUtility
{
    /// <summary>
    /// Attempts to send an ICMP ping request message to a remote computer and receive a corresponding ICMP ping response message from it.
    /// </summary>
    public class PingIcmp
    {
        /// <summary>
        /// Attempts to send an ICMP ping request message to the specified computer and receive a corresponding ICMP ping response message from it.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A List <see cref="PingReply"/> objects that provides information  about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public PingReply PingRequest(string hostname)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply result = ping.Send(hostname);
                    return result;
                }
                catch (ArgumentNullException ex)
                {
                    throw new ArgumentNullException("Address is null.", ex.InnerException);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException("Timeout is less than zero.", ex.InnerException);
                }
                catch (PingException ex)
                {
                    throw new PingException("An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.", ex.InnerException);
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
        /// A preliminary request to send ICMP messages to the specified computer and receive a response from it. This method allows you to specify a timeout for the operation.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A <see cref="PingReply"/> objects that provides information  about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public PingReply PingRequest(string hostname, int timeout)
        {
            using (Ping ping = new Ping()){
                try
                {
                    PingReply result = ping.Send(hostname, timeout);
                    return result;
                }
                catch (ArgumentNullException ex)
                {
                    throw new ArgumentNullException(@"Hostname is null or is an empty string ("").", ex.InnerException);
                }
                catch(ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException(hostname, ex.InnerException);
                }
                catch (PingException ex)
                {
                    throw new PingException("An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.", ex.InnerException);
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
