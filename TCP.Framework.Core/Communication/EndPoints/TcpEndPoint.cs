using System;
using TCP.Framework.Core.Client;
using TCP.Framework.Core.Client.Tcp;
using TCP.Framework.Core.Server;

namespace TCP.Framework.Core.Communication.EndPoints
{
    /// <summary>
    /// Represens a TCP end point.
    /// </summary>
    public sealed class TcpEndPoint : BaseEndPoint
    {
        /// <summary>
        /// IP address of the server.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Listening TCP Port for incoming connection requests on server.
        /// </summary>
        public int TcpPort { get; private set; }

        /// <summary>
        /// Creates a new TcpEndPoint object with specified port number.
        /// </summary>
        /// <param name="tcpPort">Listening TCP Port for incoming connection requests on server</param>
        public TcpEndPoint(int tcpPort)
        {
            this.TcpPort = tcpPort;
        }

        /// <summary>
        /// Creates a new TcpEndPoint object with specified IP address and port number.
        /// </summary>
        /// <param name="ipAddress">IP address of the server</param>
        /// <param name="port">Listening TCP Port for incoming connection requests on server</param>
        public TcpEndPoint(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.TcpPort = port;
        }
        
        /// <summary>
        /// Creates a new TcpEndPoint from a string address.
        /// Address format must be like IPAddress:Port (For example: 127.0.0.1:10085).
        /// </summary>
        /// <param name="address">TCP end point Address</param>
        /// <returns>Created TcpEndpoint object</returns>
        public TcpEndPoint(string address)
        {
            var splittedAddress = address.Trim().Split(':');
            IpAddress = splittedAddress[0].Trim();
            TcpPort = Convert.ToInt32(splittedAddress[1].Trim());
        }

        /// <summary>
        /// Creates a Server that uses this end point to listen incoming connections.
        /// </summary>
        /// <returns>Server instance</returns>
        internal override IServer CreateServer()
        {
            return new TcpServer(this);
        }

        /// <summary>
        /// Creates a Client that uses this end point to connect to server.
        /// </summary>
        /// <returns>Client instance</returns>
        internal override IClient CreateClient()
        {
            return new TcpClient(this);
        }

        /// <summary>
        /// Generates a string representation of this end point object.
        /// </summary>
        /// <returns>String representation of this end point object</returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(IpAddress) ? ("tcp://" + TcpPort) : ("tcp://" + IpAddress + ":" + TcpPort);
        }
    }
}