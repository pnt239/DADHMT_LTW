using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TabletC.Core
{
    public enum ShapeType
    {
        Line = 1, Rectangle, Circle, Ellipse, Triangle
    }

    public interface IShape
    {
        /* List of vertices */
        List<Point> Vertices { get; set; }

        /* Pen to draw shape. Having width, color */
        Pen ShapePen { get; set; }
        
        /* Mouse start point */
        Point StartVertex { get; set; }

        /* Mouse end point */
        Point EndVertex { get; set; }

        /* Get type of shape (line, rectable, circle, ellipse */
        ShapeType GetShapeType();

        /* Prototype Design method */
        IShape Clone();
    }
}
