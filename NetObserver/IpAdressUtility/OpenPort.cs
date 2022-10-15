using NetObserver.Model;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NetObserver.IpAdressUtility
{
    /// <summary>
    /// Tries to check whether the port is open for the TCP protocol
    /// </summary>
    public class OpenPort
    {

        /// <summary>
        /// Checks whether the specified port is open.
        /// </summary>
        /// <remarks>It is recommended to first check the availability of a remote host. Otherwise, if the host is not available - the working time of the method will be greatly increased.</remarks>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="port">The port number of the remote host, which you are going to check.</param>
        /// <returns>Returns the <see cref="PortReply"/> object with information about the status(open\closed) of the port.</returns>
        public PortReply GetOpenPort(string hostname, int port)
        {
            if (port <= 0 || port > 65535)
            {
                throw new ArgumentOutOfRangeException("port", $"Port: {port} - " +
                    $"The value of the argument is outside the range of permissible values");
            }

            using (TcpClient socket = new TcpClient())
            {
                IAsyncResult result = socket.BeginConnect(hostname, port, null, null);

                bool succ = result.AsyncWaitHandle.WaitOne(1000, true);

                if (socket.Connected)
                {
                    socket.EndConnect(result);
                    return new PortReply(port, PortStatus.Open);
                }
                else
                {
                    socket.Close();
                    return new PortReply(port, PortStatus.Closed);
                }
            }
        }

        /// <summary>
        /// Checks whether the ports are open in the specified range.
        /// </summary>
        /// <remarks>It is recommended to first check the availability of a remote host. Otherwise, if the host is not available - the working time of the method will be greatly increased.</remarks>
        /// <param name="hostname">The address of the remote host from which you want to receive a response.</param>
        /// <param name="startPort">The starting port from which you need to start check.</param>
        /// <param name="endPort">The last port for verification.</param>
        /// <returns>Returns list <see cref="PortReply"/> object with information about the status(open\closed) of the port.</returns>
        public List<PortReply> GetOpenPort(string hostname, int startPort, int endPort)
        {
            if (startPort <= 0 || startPort > 65535 || endPort < startPort || endPort > 65535 || startPort == endPort)
            {
                throw new ArgumentOutOfRangeException("port", $"Port: {startPort} or {endPort} - " +
                    $"The value of the argument is outside the range of permissible values");
            }

            List<PortReply> list = new List<PortReply>();

            for (int i = startPort; i <= endPort; i++)
            {
                using (TcpClient socket = new TcpClient())
                {
                    IAsyncResult result = socket.BeginConnect(hostname, i, null, null);

                    bool succ = result.AsyncWaitHandle.WaitOne(1000, true);

                    if (socket.Connected)
                    {
                        socket.EndConnect(result);
                        list.Add(new PortReply(i, PortStatus.Open));
                    }
                    else
                    {
                        socket.Close();
                        list.Add(new PortReply(i, PortStatus.Closed));
                    }
                }
            }
            return list;
        }
    }
}
