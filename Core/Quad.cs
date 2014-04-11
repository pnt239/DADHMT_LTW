using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Quad : IShape
    {
        private List<Point> _vertices;

        public Quad(Point start, Point end)
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);

            _vertices.Add(start);
            _vertices.Add(end);
            _vertices.Add(new Point(end.X, start.Y));
            _vertices.Add(new Point(start.X, end.Y));
        }

        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Pen ShapePen { get; set; }

        public Point StartVertex
        {
            get { return _vertices[0]; }
            set
            {
                _vertices[0] = value;
            }
        }

        public Point EndVertex
        {
            get { return _vertices[1]; }
            set
            {
                _vertices[1] = value;
                _vertices[2] = new Point(_vertices[1].X, _vertices[0].Y);
                _vertices[3] = new Point(_vertices[0].X, _vertices[1].Y);
            }
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Rectangle;
        }

        public IShape Clone()
        {
            return new Quad(new Point(), new Point());
        }
    }
}
