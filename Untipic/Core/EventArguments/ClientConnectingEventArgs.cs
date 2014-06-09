using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Core.EventArguments
{
    public class ClientConnectingEventArgs : EventArgs
    {
        public ClientConnectingEventArgs(string client, bool accept)
        {
            ClientInfo = client;
            IsAccept = accept;
        }

        public string ClientInfo { get; set; }

        public bool IsAccept { get; set; }
    }

    public delegate void ClientConnectingEventHandler(Object sender, ClientConnectingEventArgs clientAdmitEventArgs);
}
