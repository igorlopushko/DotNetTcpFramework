namespace TCP.Framework.Core.Client
{
    using TCP.Framework.Core.Communication.EndPoints;

    /// <summary>
    /// This class is used to create Clients to connect to a SCS server.
    /// </summary>
    public static class ClientFactory
    {
        /// <summary>
        /// Creates a new client to connect to a server using an end point.
        /// </summary>
        /// <param name="endpoint">End point of the server to connect it</param>
        /// <returns>Created TCP client</returns>
        public static IClient CreateClient(BaseEndPoint endpoint)
        {
            return endpoint.CreateClient();
        }

        /// <summary>
        /// Creates a new client to connect to a server using an end point.
        /// </summary>
        /// <param name="endpointAddress">End point address of the server to connect it</param>
        /// <returns>Created TCP client</returns>
        public static IClient CreateClient(string endpointAddress)
        {
            return CreateClient(BaseEndPoint.CreateEndPoint(endpointAddress));
        }
    }
}