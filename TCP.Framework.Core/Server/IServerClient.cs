using System;
using TCP.Framework.Core.Communication;
using TCP.Framework.Core.Communication.EndPoints;
using TCP.Framework.Core.Communication.Messengers;

namespace TCP.Framework.Core.Server
{
    /// <summary>
    /// Represents a client from a perspective of a server.
    /// </summary>
    public interface IServerClient : IMessenger
    {
        /// <summary>
        /// This event is raised when client disconnected from server.
        /// </summary>
        event EventHandler Disconnected;
        
        /// <summary>
        /// Unique identifier for this client in server.
        /// </summary>
        long ClientId { get; }

        /// <summary>
        /// Gets endpoint of remote application.
        /// </summary>
        BaseEndPoint RemoteEndPoint { get; }

        /// <summary>
        /// Gets the current communication state.
        /// </summary>
        CommunicationStates CommunicationState { get; }

        /// <summary>
        /// Disconnects from server.
        /// </summary>
        void Disconnect();
    }
}