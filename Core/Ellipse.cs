using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TabletC.Core;

namespace TabletC.Core
{
    public class Ellipse : IShape
    {
        private List<Point> _vertices;

        public Ellipse(Point start, Point end)
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);

            _vertices.Add(start);
            _vertices.Add(end);
        }

        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Pen ShapePen { get; set; }

        public int MajorAxis
        {
            get { return Math.Abs(EndVertex.X - StartVertex.X) / 2; }
        }

        public int MinorAxis
        {
            get { return Math.Abs(EndVertex.Y - StartVertex.Y) / 2; }
        }

        public Point StartVertex
        {
            get { return _vertices[0]; }
            set { _vertices[0] = value; }
        }

        public Point EndVertex
        {
            get { return _vertices[1]; }
            set { _vertices[1] = value; }
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Ellipse;
        }

        public IShape Clone()
        {
            return new Ellipse(new Point(), new Point());
        }
    }
}
