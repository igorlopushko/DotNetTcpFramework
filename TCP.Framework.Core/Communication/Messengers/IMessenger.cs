﻿using System;
using TCP.Framework.Core.Communication.Messages;
using TCP.Framework.Core.Communication.Protocols;

namespace TCP.Framework.Core.Communication.Messengers
{
    /// <summary>
    /// Represents an object that can send and receive messages.
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// This event is raised when a new message is received.
        /// </summary>
        event EventHandler<MessageEventArgs> MessageReceived;

        /// <summary>
        /// This event is raised when a new message is sent without any error.
        /// It does not guaranties that message is properly handled and processed by remote application.
        /// </summary>
        event EventHandler<MessageEventArgs> MessageSent;

        /// <summary>
        /// Gets/sets wire protocol that is used while reading and writing messages.
        /// </summary>
        IWireProtocol WireProtocol { get; set; }

        /// <summary>
        /// Gets the time of the last succesfully received message.
        /// </summary>
        DateTime LastReceivedMessageTime { get; }

        /// <summary>
        /// Gets the time of the last succesfully sent message.
        /// </summary>
        DateTime LastSentMessageTime { get; }

        /// <summary>
        /// Sends a message to the remote application.
        /// </summary>
        /// <param name="message">Message to be sent.</param>
        void SendMessage(IMessage message);
    }
}