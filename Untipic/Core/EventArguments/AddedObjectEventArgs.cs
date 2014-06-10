using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Core.EventArguments
{
    public class AddedObjectEventArgs : EventArgs
    {
        public AddedObjectEventArgs(IDrawingObject obj)
        {
            _object = obj;
        }

        public IDrawingObject Object { get { return _object; } }

        private readonly IDrawingObject _object;
    }

    public delegate void AddedObjectEventHandler(Object sender, AddedObjectEventArgs e);
}
