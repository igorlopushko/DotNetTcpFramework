using System;
using TCP.Framework.Core.Communication;
using TCP.Framework.Core.Communication.Channels;
using TCP.Framework.Core.Communication.Messages;
using TCP.Framework.Core.Communication.Protocols;

namespace TCP.Framework.Core.Server
{
    using TCP.Framework.Core.Communication.EndPoints;

    /// <summary>
    /// This class represents a client in server side.
    /// </summary>
    internal class ServerClient : IServerClient
    {
        #region Public events

        /// <summary>
        /// This event is raised when a new message is received.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;

        /// <summary>
        /// This event is raised when a new message is sent without any error.
        /// It does not guaranties that message is properly handled and processed by remote application.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageSent;

        /// <summary>
        /// This event is raised when client is disconnected from server.
        /// </summary>
        public event EventHandler Disconnected;

        #endregion

        #region Public properties

        /// <summary>
        /// Unique identifier for this client in server.
        /// </summary>
        public long ClientId { get; set; }

        /// <summary>
        /// Gets the communication state of the Client.
        /// </summary>
        public CommunicationStates CommunicationState
        {
            get
            {
                return _communicationChannel.CommunicationState;
            }
        }
        
        /// <summary>
        /// Gets/sets wire protocol that is used while reading and writing messages.
        /// </summary>
        public IWireProtocol WireProtocol
        {
            get { return _communicationChannel.WireProtocol; }
            set { _communicationChannel.WireProtocol = value; }
        }

        /// <summary>
        /// Gets endpoint of remote application.
        /// </summary>
        public BaseEndPoint RemoteEndPoint
        {
            get { return _communicationChannel.RemoteEndPoint; }
        }

        /// <summary>
        /// Gets the time of the last succesfully received message.
        /// </summary>
        public DateTime LastReceivedMessageTime
        {
            get
            {
                return _communicationChannel.LastReceivedMessageTime;
            }
        }

        /// <summary>
        /// Gets the time of the last succesfully received message.
        /// </summary>
        public DateTime LastSentMessageTime
        {
            get
            {
                return _communicationChannel.LastSentMessageTime;
            }
        }

        #endregion

        #region Private fields

        /// <summary>
        /// The communication channel that is used by client to send and receive messages.
        /// </summary>
        private readonly ICommunicationChannel _communicationChannel;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new Client object.
        /// </summary>
        /// <param name="communicationChannel">The communication channel that is used by client to send and receive messages</param>
        public ServerClient(ICommunicationChannel communicationChannel)
        {
            _communicationChannel = communicationChannel;
            _communicationChannel.MessageReceived += CommunicationChannel_MessageReceived;
            _communicationChannel.MessageSent += CommunicationChannel_MessageSent;
            _communicationChannel.Disconnected += CommunicationChannel_Disconnected;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Disconnects from client and closes underlying communication channel.
        /// </summary>
        public void Disconnect()
        {
            _communicationChannel.Disconnect();
        }

        /// <summary>
        /// Sends a message to the client.
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public void SendMessage(IMessage message)
        {
            _communicationChannel.SendMessage(message);
        }

        #endregion

        #region Private methods
        
        /// <summary>
        /// Handles Disconnected event of _communicationChannel object.
        /// </summary>
        /// <param name="sender">Source of event</param>
        /// <param name="e">Event arguments</param>
        private void CommunicationChannel_Disconnected(object sender, EventArgs e)
        {
            OnDisconnected();
        }

        /// <summary>
        /// Handles MessageReceived event of _communicationChannel object.
        /// </summary>
        /// <param name="sender">Source of event</param>
        /// <param name="e">Event arguments</param>
        private void CommunicationChannel_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message is PingMessage)
            {
                _communicationChannel.SendMessage(new PingMessage { RepliedMessageId = message.MessageId });
                return;
            }

            OnMessageReceived(message);
        }

        /// <summary>
        /// Handles MessageSent event of _communicationChannel object.
        /// </summary>
        /// <param name="sender">Source of event</param>
        /// <param name="e">Event arguments</param>
        private void CommunicationChannel_MessageSent(object sender, MessageEventArgs e)
        {
            OnMessageSent(e.Message);
        }

        #endregion

        #region Event raising methods

        /// <summary>
        /// Raises Disconnected event.
        /// </summary>
        private void OnDisconnected()
        {
            var handler = Disconnected;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises MessageReceived event.
        /// </summary>
        /// <param name="message">Received message</param>
        private void OnMessageReceived(IMessage message)
        {
            var handler = MessageReceived;
            if (handler != null)
            {
                handler(this, new MessageEventArgs(message));
            }
        }

        /// <summary>
        /// Raises MessageSent event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnMessageSent(IMessage message)
        {
            var handler = MessageSent;
            if (handler != null)
            {
                handler(this, new MessageEventArgs(message));
            }
        }

        #endregion
    }
}