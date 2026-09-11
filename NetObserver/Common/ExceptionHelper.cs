using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NetObserver.Common
{
    /// <summary>
    /// Centralizes translation of low-level networking exceptions into the exception
    /// contract documented on NetObserver's public methods, without losing the original
    /// exception (stack trace, message, error code) as the inner exception.
    /// </summary>
    internal static class ExceptionHelper
    {
        /// <summary>
        /// Runs a synchronous operation and translates known exceptions into
        /// NetObserver's documented exception contract.
        /// </summary>
        public static T Execute<T>(Func<T> operation, string context)
        {
            try
            {
                return operation();
            }
            catch (Exception ex) when (IsTranslatable(ex))
            {
                throw Translate(ex, context);
            }
        }

        /// <summary>
        /// Runs an asynchronous operation and translates known exceptions into
        /// NetObserver's documented exception contract.
        /// </summary>
        public static async Task<T> ExecuteAsync<T>(Func<Task<T>> operation, string context)
        {
            try
            {
                return await operation().ConfigureAwait(false);
            }
            catch (Exception ex) when (IsTranslatable(ex))
            {
                throw Translate(ex, context);
            }
        }

        private static bool IsTranslatable(Exception ex) =>
            ex is ArgumentNullException
            or ArgumentOutOfRangeException
            or PingException
            or SocketException
            or ObjectDisposedException
            or ArgumentException;

        private static Exception Translate(Exception ex, string context)
        {
            // NOTE: in every branch below the ORIGINAL exception (ex) is passed as the
            // inner exception, so the full stack trace is preserved and visible via
            // Exception.InnerException / Exception.ToString().
            switch (ex)
            {
                case ArgumentNullException argNull:
                    return new ArgumentNullException(argNull.ParamName, $"{context}: hostname is null or an empty string.");

                case ArgumentOutOfRangeException argRange:
                    return new ArgumentOutOfRangeException(argRange.ParamName, argRange.ActualValue, $"{context}: timeout is less than zero.");

                case PingException pingEx:
                    return new PingException($"{context}: an exception was thrown while sending or receiving the ICMP messages. See the inner exception for details.", pingEx);

                case SocketException sockEx:
                    return new SocketException(sockEx.ErrorCode);

                case ObjectDisposedException disposedEx:
                    return new ObjectDisposedException(disposedEx.ObjectName, $"{context}: this object has been disposed.");

                case ArgumentException argEx:
                    return new ArgumentException($"{context}: {argEx.Message}", argEx.ParamName, argEx);

                default:
                    // Should not be reachable given IsTranslatable, but keep the
                    // original exception intact if it ever is.
                    return ex;
            }
        }
    }
}
