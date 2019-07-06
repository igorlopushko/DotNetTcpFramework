using System;
using TCP.Framework.Core.Communication.Protocols;

namespace TCP.Framework.Core.Server
{
    using System.Collections.Concurrent;

    /// <summary>
    /// Represents a server that is used to accept and manage client connections.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// This event is raised when a new client connected to the server.
        /// </summary>
        event EventHandler<ServerClientEventArgs> ClientConnected;

        /// <summary>
        /// This event is raised when a client disconnected from the server.
        /// </summary>
        event EventHandler<ServerClientEventArgs> ClientDisconnected;

        /// <summary>
        /// Gets/sets wire protocol factory to create IWireProtocol objects.
        /// </summary>
        IWireProtocolFactory WireProtocolFactory { get; set; }

        /// <summary>
        /// A collection of clients that are connected to the server.
        /// </summary>
        ConcurrentDictionary<long, IServerClient> Clients { get; }
        
        /// <summary>
        /// Starts the server.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the server.
        /// </summary>
        void Stop();
    }
}