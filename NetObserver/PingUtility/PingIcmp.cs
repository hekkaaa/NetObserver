using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace NetObserver.PingUtility
{
    public class PingIcmp
    {
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
                    throw new ArgumentNullException("address is null.", ex.InnerException);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException("timeout is less than zero.", ex.InnerException);
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
                    throw new Exception("New Error" + ex.Message);
                }
            }
        }

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
                    throw new ArgumentNullException(@"hostNameOrAddress is null or is an empty string ("").", ex.InnerException);
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
