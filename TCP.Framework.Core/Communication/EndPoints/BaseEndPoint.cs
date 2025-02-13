﻿using System;
using TCP.Framework.Core.Client;
using TCP.Framework.Core.Server;

namespace TCP.Framework.Core.Communication.EndPoints
{
    /// <summary>
    /// Represents a server side end point.
    /// </summary>
    public abstract class BaseEndPoint
    {
        /// <summary>
        /// Create a End Point from a string.
        /// Address must be formatted as: protocol://address
        /// For example: tcp://89.43.104.179:10048 for a TCP endpoint with
        /// IP 89.43.104.179 and port 10048.
        /// </summary>
        /// <param name="endPointAddress">Address to create endpoint</param>
        /// <returns>Created end point</returns>
        public static BaseEndPoint CreateEndPoint(string endPointAddress)
        {
            // Check if end point address is null
            if (string.IsNullOrEmpty(endPointAddress))
            {
                throw new ArgumentNullException("endPointAddress");
            }

            // If not protocol specified, assume TCP.
            var endPointAddr = endPointAddress;
            if (!endPointAddr.Contains("://"))
            {
                endPointAddr = "tcp://" + endPointAddr;
            }

            // Split protocol and address parts
            var splittedEndPoint = endPointAddr.Split(new[] { "://" }, StringSplitOptions.RemoveEmptyEntries);
            if (splittedEndPoint.Length != 2)
            {
                throw new ApplicationException(string.Format("{0} is not a valid endpoint address.", endPointAddress));
            }

            // Split end point, find protocol and address
            var protocol = splittedEndPoint[0].Trim().ToLower();
            var address = splittedEndPoint[1].Trim();
            switch (protocol)
            {
                case "tcp":
                    return new TcpEndPoint(address);
                default:
                    throw new ApplicationException("Unsupported protocol " + protocol + " in end point " + endPointAddress);
            }
        }

        /// <summary>
        /// Creates a Server that uses this end point to listen incoming connections.
        /// </summary>
        /// <returns>Server instance</returns>
        internal abstract IServer CreateServer();

        /// <summary>
        /// Creates a Server that uses this end point to connect to server.
        /// </summary>
        /// <returns>Client instance</returns>
        internal abstract IClient CreateClient();
    }
}