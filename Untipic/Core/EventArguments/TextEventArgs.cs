using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Core.EventArguments
{
    public class TextEventArgs : EventArgs
    {
        public TextEventArgs(TextObject obj)
        {
            TextObject = obj;
        }

        public TextObject TextObject { get; set; }
    }

    public delegate void TextEventHandler(Object sender, TextEventArgs e);
}
