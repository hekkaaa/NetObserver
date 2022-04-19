using NetObserver.PingUtility;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NetObserver.TracerouteUtility
{
    /// <summary>
    /// Allows an application to determine a route to a destination by sending ICMP (Internet Control Protocol) echo packets to the destination.
    /// </summary>
    public class Traceroute
    {
        private int _timeout = 4000; // default timeout https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping
        private int _maxttl = 30;
        private byte[] _buffer = new byte[32]; // default value byte https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/ping 
        private bool _fragment = false;


        public IEnumerable<string> GetIpTraceRoute(string hostname)
        {
            PingIcmp pingSender = new PingIcmp();
            List<string> resultList = new List<string>();

            for (var ttl = 1; ttl <= _maxttl; ttl++)
            {
                PingReply reply = pingSender.PingRequest(hostname, _timeout, _buffer, new PingOptions { Ttl = ttl, DontFragment = _fragment });
                if (reply.Status == IPStatus.Success)
                {
                    resultList.Add(reply.Address.ToString());
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    resultList.Add(reply.Address.ToString());
                }
            }
            return resultList;
        }

        public IEnumerable<string> GetIpTraceRoute(string hostname, int timeout, byte[] buffer, PingOptions option, int maxtll = 30)
        {
            PingIcmp pingSender = new PingIcmp();
            List<string> resultList = new List<string>();

            for (var ttl = 1; ttl <= maxtll; ttl++)
            {
                PingReply reply = pingSender.PingRequest(hostname, timeout, buffer, option);
                if (reply.Status == IPStatus.Success)
                {
                    resultList.Add(reply.Address.ToString());
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    resultList.Add(reply.Address.ToString());
                }
            }
            return resultList;
        }

        public IEnumerable<PingReply> GetDetailTraceRoute(string hostname)
        {
            PingIcmp pingSender = new PingIcmp();
            List<PingReply> resultList = new List<PingReply>();

            for (var ttl = 1; ttl <= _maxttl; ttl++)
            {
                PingReply reply = pingSender.PingRequest(hostname, _timeout, _buffer, new PingOptions { Ttl = ttl, DontFragment = _fragment });
                if (reply.Status == IPStatus.Success)
                {
                    PingReply result = pingSender.PingRequest(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    PingReply result = pingSender.PingRequest(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        public IEnumerable<PingReply> GetDetailTraceRoute(string hostname, int timeout, byte[] buffer, PingOptions option, int maxtll = 30)
        {
            PingIcmp pingSender = new PingIcmp();
            List<PingReply> resultList = new List<PingReply>();

            for (var ttl = 1; ttl <= maxtll; ttl++)
            {
                PingReply reply = pingSender.PingRequest(hostname, timeout, buffer, option);
                if (reply.Status == IPStatus.Success)
                {
                    PingReply result = pingSender.PingRequest(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                    break;
                }
                else if (reply.Status == IPStatus.TtlExpired)
                {
                    PingReply result = pingSender.PingRequest(reply.Address.ToString(), _timeout);
                    resultList.Add(result);
                }
            }
            return resultList;
        }
    }
}
