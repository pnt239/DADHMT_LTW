using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Untipic.Core.EventArguments
{
    public class DataSentEventArgs : EventArgs
    {
        public DataSentEventArgs(int clientId, NetworkStream stream)
        {
            ClientId = clientId;
            Stream = stream;
        }

        public int ClientId { get; set; }

        public NetworkStream Stream { get; set; }
    }

    public delegate void DataSentEventHandler(Object sender, DataSentEventArgs e);
}
