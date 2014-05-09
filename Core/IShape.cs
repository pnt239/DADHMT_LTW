using System.Collections.Generic;
using System.Drawing;

namespace TabletC.Core
{
    public enum ShapeType
    {
        Line = 1, Rectangle, Circle, Ellipse, Triangle, RegPolygon, Polygon
    }

    public enum FillType
    {
        NoFill, FloodFill, ScanlineFill
    }

    public interface IShape
    {
        /* List of vertices */
        List<Point> Vertices { get; set; }

        /* Pen to draw shape. Having width, color */
        Pen ShapePen { get; set; }

        Brush ShapeBrush { get; set; }

        FillType Fill { get; set; }
        
        /* Mouse start point */
        Point StartVertex { get; set; }

        /* Mouse end point */
        Point EndVertex { get; set; }

        string Name { get; }

        bool HitTest(Point point);

        void FinishEdition();

        /* Get type of shape (line, rectable, circle, ellipse */
        ShapeType GetShapeType();

        /* Prototype Design method */
        IShape Clone();
    }
}
