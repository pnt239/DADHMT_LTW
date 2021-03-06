﻿using System;

namespace Untipic.Core.EventArguments
{
    public class NetworkErrorEventArgs : EventArgs
    {
        public NetworkErrorEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }

    public delegate void NetworkErrorEventHandler(Object sender, NetworkErrorEventArgs networkError);
}
