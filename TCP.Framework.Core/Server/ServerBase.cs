using System;
using System.Collections.Concurrent;
using TCP.Framework.Core.Communication.Channels;
using TCP.Framework.Core.Communication.Protocols;

namespace TCP.Framework.Core.Server
{
    /// <summary>
    /// This class provides base functionality for server classes.
    /// </summary>
    internal abstract class ServerBase : IServer
    {
        #region Public events

        /// <summary>
        /// This event is raised when a new client is connected.
        /// </summary>
        public event EventHandler<ServerClientEventArgs> ClientConnected;

        /// <summary>
        /// This event is raised when a client disconnected from the server.
        /// </summary>
        public event EventHandler<ServerClientEventArgs> ClientDisconnected;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets/sets wire protocol that is used while reading and writing messages.
        /// </summary>
        public IWireProtocolFactory WireProtocolFactory { get; set; }

        /// <summary>
        /// A collection of clients that are connected to the server.
        /// </summary>
        public ConcurrentDictionary<long, IServerClient> Clients { get; private set; }

        #endregion

        #region Private properties

        /// <summary>
        /// This object is used to listen incoming connections.
        /// </summary>
        private IConnectionListener _connectionListener;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ServerBase()
        {
            this.Clients = new ConcurrentDictionary<long, IServerClient>();
            this.WireProtocolFactory = WireProtocolManager.GetDefaultWireProtocolFactory();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts the server.
        /// </summary>
        public virtual void Start()
        {
            _connectionListener = CreateConnectionListener();
            _connectionListener.CommunicationChannelConnected += HandleCommunicationChannelConnected;
            _connectionListener.Start();
        }

        /// <summary>
        /// Stops the server.
        /// </summary>
        public virtual void Stop()
        {
            if (_connectionListener != null)
            {
                _connectionListener.Stop();
            }

            foreach (var client in Clients.Values)
            {
                client.Disconnect();
            }
        }

        #endregion

        #region Protected abstract methods

        /// <summary>
        /// This method is implemented by derived classes to create appropriate connection listener to listen incoming connection requets.
        /// </summary>
        /// <returns></returns>
        protected abstract IConnectionListener CreateConnectionListener();

        #endregion

        #region Private methods

        /// <summary>
        /// Handles CommunicationChannelConnected event of _connectionListener object.
        /// </summary>
        /// <param name="sender">Source of event</param>
        /// <param name="e">Event arguments</param>
        private void HandleCommunicationChannelConnected(object sender, CommunicationChannelEventArgs e)
        {
            var client = new ServerClient(e.Channel)
            {
                ClientId = ServerManager.GetClientId(),
                WireProtocol = WireProtocolFactory.CreateWireProtocol()
            };

            client.Disconnected += HandleClientDisconnected;
            this.Clients[client.ClientId] = client;
            this.OnClientConnected(client);
            e.Channel.Start();
        }

        /// <summary>
        /// Handles Disconnected events of all connected clients.
        /// </summary>
        /// <param name="sender">Source of event.</param>
        /// <param name="e">Event arguments.</param>
        private void HandleClientDisconnected(object sender, EventArgs e)
        {
            var client = (IServerClient) sender;
            if (Clients.TryRemove(client.ClientId, out var removedItem))
            {
                this.OnClientDisconnected(removedItem);
            }
        }

        #endregion

        #region Event raising methods

        /// <summary>
        /// Raises ClientConnected event.
        /// </summary>
        /// <param name="client">Connected client</param>
        protected virtual void OnClientConnected(IServerClient client)
        {
            var handler = this.ClientConnected;
            if (handler != null)
            {
                handler(this, new ServerClientEventArgs(client));
            }
        }

        /// <summary>
        /// Raises ClientDisconnected event.
        /// </summary>
        /// <param name="client">Disconnected client</param>
        protected virtual void OnClientDisconnected(IServerClient client)
        {
            var handler = this.ClientDisconnected;
            if (handler != null)
            {
                handler(this, new ServerClientEventArgs(client));
            }
        }

        #endregion
    }
}