using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Untipic.Core.EventArguments
{
    public class ClientDisconnectedEventArgs : EventArgs
    {
        public ClientDisconnectedEventArgs(int clientId)
        {
            ClientId = clientId;
        }

        public int ClientId { get; set; }
    }

    public delegate void ClientDisconnectedEventHandler(Object sender, ClientDisconnectedEventArgs eventArgs);
}
