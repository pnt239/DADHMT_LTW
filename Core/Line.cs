using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Line : IShape
    {
        private List<Point> _vertices;
        private Pen _shapePen ;

        public Line(Point start, Point end)
        {
            _vertices = new List<Point> { start, end };
            _shapePen = new Pen(Color.Black);
        }

        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Pen ShapePen
        {
            get
            {
                return _shapePen;
            }
            set
            {
                _shapePen = value;
            }
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

        public string Name
        {
            get { return "Line"; }
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Line;
        }

        public IShape Clone()
        {
            return new Line(new Point(), new Point());
        }
    }
}
