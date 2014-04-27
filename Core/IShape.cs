using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TabletC.Core
{
    public enum ShapeType
    {
        Line = 1, Rectangle, Circle, Ellipse, Triangle, RegPolygon, Polygon
    }

    public enum FillType
    {
        Fill, NoFill
    }

    public interface IShape
    {
        /* List of vertices */
        List<Point> Vertices { get; set; }

        /* Pen to draw shape. Having width, color */
        Pen ShapePen { get; set; }

        Brush ShapeBrush { get; set; }

        FillType FileType { get; set; }
        
        /* Mouse start point */
        Point StartVertex { get; set; }

        /* Mouse end point */
        Point EndVertex { get; set; }

        string Name { get; }

        void FinishEdition();

        /* Get type of shape (line, rectable, circle, ellipse */
        ShapeType GetShapeType();

        /* Prototype Design method */
        IShape Clone();
    }
}
