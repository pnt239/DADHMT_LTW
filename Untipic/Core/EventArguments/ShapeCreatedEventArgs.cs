using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Core.EventArguments
{

    public class ShapeCreatedEventArgs : EventArgs
    {
        public ShapeCreatedEventArgs(ShapeBase shape)
        {
            _shape = shape;
        }

        public ShapeBase Shape { get { return _shape; } }

        private readonly ShapeBase _shape;
    }

    public delegate void ShapeCreatedEventHandler(Object sender, ShapeCreatedEventArgs e);

}
