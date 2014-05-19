using System;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    public class Line : IShape
    {
        private readonly IVertexCollection _vertices;
        private FillType _fill;

        public Line()
        {
            _vertices = new VertexCollection();
            _vertices.Add();
            _vertices.Add();

            ShapePen = new Pen(Color.Black);
            ShapeBrush = Brushes.Transparent;
            _fill = FillType.NoFill;
        }

        [Browsable(false)]
        public IVertexCollection Vertices
        {
            get { return _vertices; }
        }

        [Browsable(false)]
        public Pen ShapePen { get; set; }

        [Browsable(false)]
        public Brush ShapeBrush { get; set; }

        [Browsable(false)]
        public FillType Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        [Browsable(false)]
        public IVertex StartVertex
        {
            get { return _vertices[0]; }
            set { _vertices[0] = value; }
        }

        [Browsable(false)]
        public IVertex EndVertex
        {
            get { return _vertices[1]; }
            set { _vertices[1] = value; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Line"; }
        }

        public bool HitTest(IVertex point)
        {
            var vtpt = new Point((int) (_vertices[0].Y - _vertices[1].Y), (int) (_vertices[1].X - _vertices[0].X));
            var xmin = _vertices[0].X < _vertices[1].X ? _vertices[0].X : _vertices[1].X;
            var xmax = _vertices[0].X < _vertices[1].X ? _vertices[1].X : _vertices[0].X;

            var d = Math.Abs(vtpt.X * (point.X - _vertices[0].X) + vtpt.Y * (point.Y - _vertices[0].Y)) /
                      Math.Sqrt(vtpt.X * vtpt.X + vtpt.Y * vtpt.Y);
            if (d < 10 && point.X >= xmin && point.X <= xmax)
                return true;

            return false;
        }

        public void ReCalculateVertices()
        {
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Line;
        }

        public IShape Clone()
        {
            var obj = new Line
            {
                ShapePen = (Pen)ShapePen.Clone(),
                ShapeBrush = (Brush)ShapeBrush.Clone(),
                Fill = _fill
            };
            return obj;
        }
    }
}
