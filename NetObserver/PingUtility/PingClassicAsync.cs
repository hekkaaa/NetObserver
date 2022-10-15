using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetObserver.PingUtility
{
    /// <summary>
    /// Allows async an application to determine whether a remote computer is reachable over the network by using requests ICMP that act as echo requests in Windows - cmd.
    /// </summary>
    public class PingClassicAsync
    {
        /// <summary>
        /// Attempts async to send 4 ICMP ping request messages to the remote computer and receive a corresponding ICMP ping response message from the remote computer.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A Task List of 4 <see cref="PingReply"/> objects that provides information  about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public async Task<List<PingReply>> RequestPingAsync(string hostname)
        {
            IcmpRequestSenderAsync ping = new IcmpRequestSenderAsync();
            List<PingReply> pingReplyListReturn = new List<PingReply>();

            for (int i = 0; i < 4; i++)
            {
                pingReplyListReturn.Add(await ping.RequestIcmpAsync(hostname));
            }

            return pingReplyListReturn;
        }

        /// <summary>
        /// Attempts async to send the specified number of ICMP ping request messages to the remote computer and receive an appropriate ICMP ping response message from the remote computer.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="repeat">Number of request repetitions ICMP.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A Task List <see cref="PingReply"/> that provides information  about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public async Task<List<PingReply>> RequestPingAsync(string hostname, int repeat)
        {
            IcmpRequestSenderAsync ping = new IcmpRequestSenderAsync();
            List<PingReply> pingReplyListReturn = new List<PingReply>();

            for (int i = 0; i < repeat; i++)
            {
                pingReplyListReturn.Add(await ping.RequestIcmpAsync(hostname));
            }

            return pingReplyListReturn;
        }

        /// <summary>
        /// Attempts async to send the specified number of ICMP ping request messages to the remote computer and receive an appropriate ICMP ping response message from the remote computer within the specified timeout interval.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <param name="repeat">Number of request repetitions ICMP.</param>
        /// <exception cref="ArgumentNullException">Hostname is null or is an empty string ("").</exception>
        /// <exception cref="ArgumentOutOfRangeException">Timeout is less than zero.</exception>
        /// <exception cref="PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
        /// <exception cref="ObjectDisposedException">This object has been disposed.</exception>
        /// <exception cref="Exception">Unexpected error.</exception>
        /// <returns>A Task List <see cref="PingReply"/> that provides information  about the ICMP ping response message, if one was received, or the reason for the failure if the message was not received.</returns>
        public async Task<List<PingReply>> RequestPingAsync(string hostname, int timeout, int repeat)
        {
            IcmpRequestSenderAsync ping = new IcmpRequestSenderAsync();
            List<PingReply> pingReplyListReturn = new List<PingReply>();

            for (int i = 0; i < repeat; i++)
            {
                pingReplyListReturn.Add(await ping.RequestIcmpAsync(hostname, timeout));
            }

            return pingReplyListReturn;
        }
    }
}
