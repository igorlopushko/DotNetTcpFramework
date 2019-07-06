using System.Net;
using TCP.Framework.Core.Communication.Channels;
using TCP.Framework.Core.Communication.Channels.Tcp;
using TCP.Framework.Core.Communication.EndPoints;

namespace TCP.Framework.Core.Client.Tcp
{
    /// <summary>
    /// This class is used to communicate with server over TCP/IP protocol.
    /// </summary>
    internal class TcpClient : ClientBase
    {
        /// <summary>
        /// The endpoint address of the server.
        /// </summary>
        private readonly TcpEndPoint _serverEndPoint;

        /// <summary>
        /// Creates a new TcpClient object.
        /// </summary>
        /// <param name="serverEndPoint">The endpoint address to connect to the server</param>
        public TcpClient(TcpEndPoint serverEndPoint)
        {
            _serverEndPoint = serverEndPoint;
        }

        /// <summary>
        /// Creates a communication channel using ServerIpAddress and ServerPort.
        /// </summary>
        /// <returns>Ready communication channel to communicate</returns>
        protected override ICommunicationChannel CreateCommunicationChannel()
        {
            return new TcpCommunicationChannel(
                TcpHelper.ConnectToServer(
                    new IPEndPoint(IPAddress.Parse(_serverEndPoint.IpAddress), _serverEndPoint.TcpPort),
                    ConnectTimeout
                    ));
        }
    }
}