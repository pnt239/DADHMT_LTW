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
        IVertexCollection Vertices { get; set; }

        /* Pen to draw shape. Having width, color */
        Pen ShapePen { get; set; }

        Brush ShapeBrush { get; set; }

        FillType Fill { get; set; }
        
        /* Mouse start point */
        IVertex StartVertex { get; set; }

        /* Mouse end point */
        IVertex EndVertex { get; set; }

        string Name { get; }

        bool HitTest(IVertex point);

        void ReCalculateVertices();

        /* Get type of shape (line, rectable, circle, ellipse */
        ShapeType GetShapeType();

        /* Prototype Design method */
        IShape Clone();
    }
}
