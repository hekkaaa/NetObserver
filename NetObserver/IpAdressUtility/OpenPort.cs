using NetObserver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace NetObserver.IpAdressUtility
{
    /// <summary>
    /// Tries to check whether one or more ports are open for the TCP protocol.
    /// </summary>
    public class OpenPort
    {
        private const int DefaultTimeoutMs = 1000;
        private const int DefaultMaxConcurrency = 200;

        /// <summary>
        /// Checks whether the specified port is open.
        /// </summary>
        /// <remarks>It is recommended to first check the availability of a remote host. Otherwise, if the host is not available, this call will block for up to <paramref name="timeoutMs"/> milliseconds.</remarks>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="port">The port number of the remote host, which you are going to check.</param>
        /// <param name="timeoutMs">Maximum time to wait for the connection attempt, in milliseconds. Defaults to 1000ms.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="port"/> is outside the 1-65535 range.</exception>
        /// <returns>A <see cref="PortReply"/> object with information about the status (open/closed) of the port.</returns>
        public PortReply GetOpenPort(string hostname, int port, int timeoutMs = DefaultTimeoutMs) =>
            GetOpenPortAsync(hostname, port, timeoutMs, cancellationToken: CancellationToken.None).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously checks whether the specified port is open.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="port">The port number of the remote host, which you are going to check.</param>
        /// <param name="timeoutMs">Maximum time to wait for the connection attempt, in milliseconds. Defaults to 1000ms.</param>
        /// <param name="cancellationToken">A token to cancel the scan.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="port"/> is outside the 1-65535 range.</exception>
        /// <returns>A Task producing a <see cref="PortReply"/> object with information about the status (open/closed) of the port.</returns>
#pragma warning disable CA1822 // Пометьте члены как статические
        public async Task<PortReply> GetOpenPortAsync(string hostname, int port, int timeoutMs = DefaultTimeoutMs, CancellationToken cancellationToken = default)
#pragma warning restore CA1822 // Пометьте члены как статические
        {
            ValidatePort(port, nameof(port));

            using var client = new TcpClient();
            try
            {
                Task connectTask = client.ConnectAsync(hostname, port);
                Task timeoutTask = Task.Delay(timeoutMs, cancellationToken);

                Task completed = await Task.WhenAny(connectTask, timeoutTask).ConfigureAwait(false);

                // Regardless of which task "won", observe a faulted connectTask now (if it
                // already failed) or later (if it's still pending) so a background failure
                // never surfaces as an unobserved task exception after we return.
                ObserveFaultQuietly(connectTask);

                bool connected = completed == connectTask && client.Connected;
                return new PortReply(port, connected ? PortStatus.Open : PortStatus.Closed);
            }
            catch (SocketException)
            {
                return new PortReply(port, PortStatus.Closed);
            }
        }

        /// <summary>
        /// Checks whether the ports are open in the specified range.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="startPort">The starting port from which to begin the check.</param>
        /// <param name="endPort">The last port to check.</param>
        /// <param name="timeoutMs">Maximum time to wait per port, in milliseconds. Defaults to 1000ms.</param>
        /// <exception cref="ArgumentOutOfRangeException">The port range is invalid.</exception>
        /// <returns>A list of <see cref="PortReply"/> objects, ordered by port number, ascending.</returns>
        public IReadOnlyList<PortReply> GetOpenPort(string hostname, int startPort, int endPort, int timeoutMs = DefaultTimeoutMs) =>
            GetOpenPortAsync(hostname, startPort, endPort, timeoutMs).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously checks whether the ports are open in the specified range, scanning up to <paramref name="maxConcurrency"/> ports at a time.
        /// </summary>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="startPort">The starting port from which to begin the check.</param>
        /// <param name="endPort">The last port to check.</param>
        /// <param name="timeoutMs">Maximum time to wait per port, in milliseconds. Defaults to 1000ms.</param>
        /// <param name="maxConcurrency">Maximum number of ports checked in parallel. Defaults to 200.</param>
        /// <param name="cancellationToken">A token to cancel the scan.</param>
        /// <exception cref="ArgumentOutOfRangeException">The port range is invalid.</exception>
        /// <returns>A Task producing a list of <see cref="PortReply"/> objects, ordered by port number, ascending.</returns>
        public async Task<List<PortReply>> GetOpenPortAsync(
            string hostname,
            int startPort,
            int endPort,
            int timeoutMs = DefaultTimeoutMs,
            int maxConcurrency = DefaultMaxConcurrency,
            CancellationToken cancellationToken = default)
        {
            ValidateRange(startPort, endPort);

            using var throttle = new SemaphoreSlim(Math.Max(1, maxConcurrency));

            IEnumerable<Task<PortReply>> tasks = Enumerable.Range(startPort, endPort - startPort + 1)
                .Select(port => ScanWithThrottleAsync(hostname, port, timeoutMs, throttle, cancellationToken));

            PortReply[] results = await Task.WhenAll(tasks).ConfigureAwait(false);
            return results.OrderBy(r => r.Port).ToList();
        }

        private async Task<PortReply> ScanWithThrottleAsync(string hostname, int port, int timeoutMs, SemaphoreSlim throttle, CancellationToken cancellationToken)
        {
            await throttle.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                return await GetOpenPortAsync(hostname, port, timeoutMs, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                throttle.Release();
            }
        }

        private static void ObserveFaultQuietly(Task task)
        {
            if (task.IsCompleted)
            {
                _ = task.Exception;
                return;
            }

            task.ContinueWith(
                t => { _ = t.Exception; },
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);
        }

        private static void ValidatePort(int port, string paramName)
        {
            if (port <= 0 || port > 65535)
            {
                throw new ArgumentOutOfRangeException(paramName, port, $"Port {port} is outside the range of permissible values (1-65535).");
            }
        }

        private static void ValidateRange(int startPort, int endPort)
        {
            if (startPort <= 0 || startPort > 65535 || endPort < startPort || endPort > 65535 || startPort == endPort)
            {
                throw new ArgumentOutOfRangeException(nameof(startPort), $"Port range {startPort}-{endPort} is outside the range of permissible values (1-65535), or start equals end.");
            }
        }
    }
}
