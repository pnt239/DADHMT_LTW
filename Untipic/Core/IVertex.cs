using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public interface IVertex
    {
        // Property: X 
        // Gets a value indicating whether this vertex is empty.
        bool IsEmpty { get; }

        // Property: X 
        // Gets or sets the vertex's X position.
        float X { get; set; }

        // Property: Y 
        // Gets or sets the vertex's Y position.
        float Y { get; set; }

        // Method: ToPoint
        // Convert Vertex to Point
        Point ToPoint();

        // Method: Clone
        // Create a clone of this vertex
        IVertex Clone();
    }
}
