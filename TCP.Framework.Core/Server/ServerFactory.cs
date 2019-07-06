using TCP.Framework.Core.Communication.EndPoints;

namespace TCP.Framework.Core.Server
{
    /// <summary>
    /// This class is used to create servers.
    /// </summary>
    public static class ServerFactory
    {
        /// <summary>
        /// Creates a new Server using an EndPoint.
        /// </summary>
        /// <param name="endPoint">Endpoint that represents address of the server.</param>
        /// <returns>Created TCP server</returns>
        public static IServer CreateServer(BaseEndPoint endPoint)
        {
            return endPoint.CreateServer();
        }
    }
}