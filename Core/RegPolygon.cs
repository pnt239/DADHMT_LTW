using System;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    public class RegPolygon : IShape
    {
        private IVertexCollection _vertices;
        private int _sides;
        private IVertex _start;
        private IVertex _end;
        private FillType _fill;

        public RegPolygon()
        {
            _vertices = new VertexCollection();

            ShapePen = new Pen(Color.Black);
            ShapeBrush = Brushes.Transparent;
            _fill = FillType.NoFill;

            _sides = 5;
            _start = new Vertex();
            _end = new Vertex();
        }

        [Browsable(false)]
        public IVertexCollection Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        [Browsable(false)]
        public Pen ShapePen { get; set; }

        [Browsable(false)]
        public Brush ShapeBrush { get; set; }

        public FillType Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        [Browsable(false)]
        public IVertex StartVertex
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                ReCalculateVertices();
            }
        }

        [Browsable(false)]
        public IVertex EndVertex {
            get { return _end; }
            set
            {
                _end = value;
                ReCalculateVertices();
            }
        }

        public int Sides
        {
            get { return _sides; }
            set
            {
                _sides = value;
                if (_sides < 3) _sides = 3;
                if (_sides > 100) _sides = 100;

                _vertices.Clear();
                for (int i = 0; i < _sides; i++)
                    _vertices.Add();
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Regular Polygon"; }
        }

        public bool HitTest(IVertex point)
        {
            return Util.CheckInnerPoint(_vertices, point);
        }

        public void ReCalculateVertices()
        {
            float step = 360.0f / _sides;

            var radius = Math.Sqrt((StartVertex.X - EndVertex.X)*(StartVertex.X - EndVertex.X) +
                                   (StartVertex.Y - EndVertex.Y)*(StartVertex.Y - EndVertex.Y));
            var startingAngle = XYToDegrees(_end, _start);

            var angle = startingAngle; //starting angle
            double i;
            int k;

            for (i = startingAngle, k = 0; i < startingAngle + 360.0; i += step) //go in a full circle
            {
                _vertices[k++] = DegreesToXY(angle, radius, _start); //code snippet from above
                angle += step;
            }
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.RegPolygon;
        }

        public IShape Clone()
        {
            var obj = new RegPolygon
            {
                Sides = _sides,
                ShapePen = (Pen)ShapePen.Clone(),
                ShapeBrush = (Brush)ShapeBrush.Clone(),
                Fill = _fill
            };
            return obj;
        }

        private IVertex DegreesToXY(double degrees, double radius, IVertex origin)
        {
            var xy = new Vertex();
            double radians = degrees * Math.PI / 180.0;

            xy.X = Math.Cos(radians) * radius + origin.X;
            xy.Y = Math.Sin(-radians) * radius + origin.Y;

            return xy;
        }

        private double XYToDegrees(IVertex xy, IVertex origin)
        {
            double deltaX = origin.X - xy.X;
            double deltaY = origin.Y - xy.Y;

            double radAngle = Math.Atan2(deltaY, deltaX);
            double degreeAngle = radAngle * 180.0 / Math.PI;

            return (180.0 - degreeAngle);
        }
    }
}
