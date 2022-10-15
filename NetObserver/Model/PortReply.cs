namespace NetObserver.Model
{
    /// <summary>
    /// An object that stores the necessary data to work with ports
    /// </summary>
    public class PortReply
    {
        /// <summary>
        /// Port number.
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// Status response.
        /// </summary>
        public PortStatus Status { get; private set; }

        /// <summary>
        /// Constructor this class.
        /// </summary>
        /// <param name="port">An Int32 value that port number.</param>
        /// <param name="status">Status response.</param>
        public PortReply(int port, PortStatus status)
        {
            Port = port;
            Status = status;
        }
    }
}
