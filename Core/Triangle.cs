using System;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    public class Triangle : IShape
    {
        private IVertexCollection _vertices;
        private IVertex _start;
        private IVertex _end;
        private FillType _fill;

        public Triangle()
        {
            _vertices = new VertexCollection();

            ShapePen = new Pen(Color.Black);
            ShapeBrush = Brushes.Transparent;
            _fill = FillType.NoFill;

            _start = new Vertex();
            _end = new Vertex();
            // theo chieu kim dong ho
            _vertices.Add();
            _vertices.Add();
            _vertices.Add();
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
            get { return _start; }
            set { _start = value; }
        }

        [Browsable(false)]
        public IVertex EndVertex
        {
            get { return _end; }
            set
            {
                _end = value;
                ReCalculateVertices();
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Triangle"; }
        }

        public bool HitTest(IVertex point)
        {
            return Util.CheckInnerPoint(_vertices, point);
        }

        public void ReCalculateVertices()
        {
            double x = Math.Min(_start.X, _end.X);
            double y = Math.Min(_start.Y, _end.Y);
            double width = Math.Abs(_start.X - _end.X);
            double height = Math.Abs(_start.Y - _end.Y);

            _vertices[0] = new Vertex(x + width/2, y);
            _vertices[1] = new Vertex(x + width, y + height);
            _vertices[2] = new Vertex(x, y + height);
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Triangle;
        }

        public IShape Clone()
        {
            var obj = new Triangle
            {
                ShapePen = (Pen)ShapePen.Clone(),
                ShapeBrush = (Brush)ShapeBrush.Clone(),
                Fill = _fill
            };
            return obj;
        }
    }
}
