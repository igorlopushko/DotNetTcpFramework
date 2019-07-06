using System;

namespace TCP.Framework.Core.Communication.Messages
{
    /// <summary>
    /// Stores message to be used by an event.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Message object that is associated with this event.
        /// </summary>
        public IMessage Message { get; private set; }

        /// <summary>
        /// Creates a new MessageEventArgs object.
        /// </summary>
        /// <param name="message">Message object that is associated with this event</param>
        public MessageEventArgs(IMessage message)
        {
            this.Message = message;
        }
    }
}