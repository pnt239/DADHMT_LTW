using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TabletC.Core
{
    public class Polygon : IShape
    {
        private List<Point> _vertices;
        private Pen _shapePen ;

        public Polygon(Point start, Point end)
        {
            _vertices = new List<Point> { start, end };
            _shapePen = new Pen(Color.Black);
        }

        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
            //test
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

        public ShapeType GetShapeType()
        {
            return ShapeType.Polygon;
        }

        public IShape Clone()
        {
            return new Polygon(new Point(), new Point());
        }
    }
}
