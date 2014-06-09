using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Untipic.Core.EventArguments
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public ClientConnectedEventArgs(Socket client, int id)
        {
            Client = client;
            Id = id;
        }

        public Socket Client { get; set; }

        public int Id { get; set; }
    }

    public delegate void ClientConnectedEventHandler
    (
        Object sender, 
        ClientConnectedEventArgs connectionEstablishedEventArgs
    );
}
