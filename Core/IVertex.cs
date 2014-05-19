using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public interface IVertex
    {
        // Property: X 
        // Gets a value indicating whether this vertex is empty.
        Boolean IsEmpty { get; }

        // Property: X 
        // Gets or sets the vertex's X position.
        Double X { get; set; }

        // Property: Y 
        // Gets or sets the vertex's Y position.
        Double Y { get; set; }

        // Method: ToPoint
        // Convert Vertex to Point
        Point ToPoint();
    }
}
