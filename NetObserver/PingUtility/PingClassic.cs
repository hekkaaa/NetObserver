using System.Collections.Generic;
using System.Net.NetworkInformation;


namespace NetObserver.PingUtility
{
    public class PingClassic
    {

        public List<PingReply> PingRequest(string hostname)
        {
            PingIcmp ping = new PingIcmp();
            List<PingReply> pingReplyListReturn = new List<PingReply>();

            for (int i = 0; i < 4; i++)
            {
                pingReplyListReturn.Add(ping.PingRequest(hostname));
            }

            return pingReplyListReturn;
        }

        public List<PingReply> PingRequest(string hostname, int repeat)
        {
            PingIcmp ping = new PingIcmp();
            List<PingReply> pingReplyListReturn = new List<PingReply>();

            for (int i = 0; i < repeat; i++)
            {
                pingReplyListReturn.Add(ping.PingRequest(hostname));
            }

            return pingReplyListReturn;
        }
    }
}
