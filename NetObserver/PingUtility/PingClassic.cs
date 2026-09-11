using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NetObserver.PingUtility
{
    /// <summary>
    /// Allows an application to determine whether a remote computer is reachable over the network by using ICMP requests that act as echo requests, similar to Windows' cmd ping.
    /// </summary>
    public class PingClassic
    {
        private readonly IcmpRequestSender _ping = new IcmpRequestSender();

        /// <summary>
        /// Attempts to send 4 ICMP ping request messages to the remote computer and receive a corresponding ICMP ping response message from the remote computer.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <returns>A list of 4 <see cref="PingReply"/> objects that provide information about the ICMP ping response messages received (or the reason for failure).</returns>
        public IReadOnlyList<PingReply> RequestPing(string hostname) => RequestPing(hostname, 4);

        /// <summary>
        /// Attempts to send the specified number of ICMP ping request messages to the remote computer.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="repeat">Number of ICMP request repetitions. Must be greater than zero.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="repeat"/> is less than or equal to zero.</exception>
        /// <returns>A list of <see cref="PingReply"/> objects that provide information about the ICMP ping response messages received (or the reason for failure).</returns>
        public IReadOnlyList<PingReply> RequestPing(string hostname, int repeat)
        {
            if (repeat <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(repeat), repeat, "Repeat count must be greater than zero.");
            }

            var results = new List<PingReply>(repeat);
            for (int i = 0; i < repeat; i++)
            {
                results.Add(_ping.RequestIcmp(hostname));
            }

            return results;
        }

        /// <summary>
        /// Attempts to send the specified number of ICMP ping request messages to the remote computer within the specified timeout interval.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="timeout">An Int32 value that specifies the maximum time (after sending ping messages) to wait for an ICMP ping message, in milliseconds.</param>
        /// <param name="repeat">Number of ICMP request repetitions. Must be greater than zero.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="repeat"/> is less than or equal to zero.</exception>
        /// <returns>A list of <see cref="PingReply"/> objects that provide information about the ICMP ping response messages received (or the reason for failure).</returns>
        public IReadOnlyList<PingReply> RequestPing(string hostname, int timeout, int repeat)
        {
            if (repeat <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(repeat), repeat, "Repeat count must be greater than zero.");
            }

            var results = new List<PingReply>(repeat);
            for (int i = 0; i < repeat; i++)
            {
                results.Add(_ping.RequestIcmp(hostname, timeout));
            }

            return results;
        }
    }
}
