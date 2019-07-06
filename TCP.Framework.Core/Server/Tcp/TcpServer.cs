using TCP.Framework.Core.Communication.Channels;
using TCP.Framework.Core.Communication.Channels.Tcp;
using TCP.Framework.Core.Communication.EndPoints;

namespace TCP.Framework.Core.Server
{
    /// <summary>
    /// This class is used to create a TCP server.
    /// </summary>
    internal class TcpServer : ServerBase
    {
        /// <summary>
        /// The endpoint address of the server to listen incoming connections.
        /// </summary>
        private readonly TcpEndPoint _endPoint;

        /// <summary>
        /// Creates a new TcpServer object.
        /// </summary>
        /// <param name="endPoint">The endpoint address of the server to listen incoming connections</param>
        public TcpServer(TcpEndPoint endPoint)
        {
            _endPoint = endPoint;
        }

        /// <summary>
        /// Creates a TCP connection listener.
        /// </summary>
        /// <returns>Created listener object</returns>
        protected override IConnectionListener CreateConnectionListener()
        {
            return new TcpConnectionListener(_endPoint);
        }
    }
}