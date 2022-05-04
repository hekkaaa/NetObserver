using System;
using System.Collections.Generic;
using System.Text;

namespace NetObserver.Model
{   
    public class PortReply
    {
        public int Port { get; private set; }
        public PortStatus Status { get; private set; }

        public PortReply(int port, PortStatus status)
        {
            Port = port;
            Status = status;
        }
    }
}
