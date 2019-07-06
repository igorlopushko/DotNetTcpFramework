using System;

namespace TCP.Framework.Core.Server
{
    /// <summary>
    /// Stores client information to be used by an event.
    /// </summary>
    public class ServerClientEventArgs : EventArgs
    {
        /// <summary>
        /// Client that is associated with this event.
        /// </summary>
        public IServerClient Client { get; private set; }

        /// <summary>
        /// Creates a new ServerClientEventArgs object.
        /// </summary>
        /// <param name="client">Client that is associated with this event</param>
        public ServerClientEventArgs(IServerClient client)
        {
            this.Client = client;
        }
    }
}