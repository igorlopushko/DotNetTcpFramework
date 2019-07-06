using TCP.Framework.Core.Communication.Messengers;

namespace TCP.Framework.Core.Client
{
    

    /// <summary>
    /// Represents a client to connect to server.
    /// </summary>
    public interface IClient : IMessenger, IConnectableClient
    {
        //Does not define any additional member
    }
}