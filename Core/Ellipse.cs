using System;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    public class Ellipse : IShape
    {
        private IVertexCollection _vertices;
        private FillType _fill;
        private IVertex _start;
        private IVertex _end;
        private IVertex _orginal;
        private Double _major;
        private Double _minor;

        public Ellipse()
        {
            _vertices = new VertexCollection();
            _vertices.Add();
            _vertices.Add();
            _vertices.Add();
            _vertices.Add();
            
            ShapePen = new Pen(Color.Black);
            ShapeBrush = Brushes.Transparent;
            _fill = FillType.NoFill;

            _start = new Vertex();
            _end = new Vertex();
            _orginal = new Vertex();
            _major = _minor = 0;
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
        public IVertex Orginal
        {
            get
            {
                return _orginal;
            }
        }

        [Browsable(false)]
        public Double MajorAxis
        {
            get { return _major; }
        }

        [Browsable(false)]
        public Double MinorAxis
        {
            get { return _minor; }
        }

        [Browsable(false)]
        public IVertex StartVertex
        {
            get { return _start; }
            set
            {
                _start = value;
                ReCalculateVertices();
            }
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
            get { return "Ellipse"; }
        }

        public bool HitTest(IVertex point)
        {
            double d = Math.Pow(point.X - Orginal.X, 2) / Math.Pow(MajorAxis, 2) +
                       Math.Pow(point.Y - Orginal.Y, 2) / Math.Pow(MinorAxis, 2);
            return d <= 1;
        }

        public void ReCalculateVertices()
        {
            _orginal.X = (_start.X + _end.X) / 2;
            _orginal.Y = (_start.Y + _end.Y) / 2;

            _major = Math.Abs(_start.X - _end.X) / 2;
            _minor = Math.Abs(EndVertex.Y - StartVertex.Y)/2;

            for (int i = 0; i < 4; i++)
                _vertices[i] = new Vertex(_orginal.X + _major*Math.Cos(i*Math.PI/2),
                    _orginal.Y + _minor*Math.Sin(i*Math.PI/2));
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Ellipse;
        }

        public IShape Clone()
        {
            var obj = new Ellipse
            {
                ShapePen = (Pen)ShapePen.Clone(),
                ShapeBrush = (Brush)ShapeBrush.Clone(),
                Fill = _fill
            };
            return obj;
        }
    }
}
