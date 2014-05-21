using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    [Serializable]
    public class Quad : IShape
    {
        private IVertexCollection _vertices;
        private Pen _pen;
        private Brush _brush;
        private FillType _fill;

        public Quad()
        {
            _vertices = new VertexCollection();

            ShapePen = new Pen(Color.Black);
            ShapeBrush = Brushes.Transparent;
            _fill = FillType.NoFill;

            _vertices.Add();
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
        public Pen ShapePen
        {
            get { return _pen; }
            set { _pen = value; }
        }

        [Browsable(false)]
        public Brush ShapeBrush
        {
            get { return _brush; }
            set { _brush = value; }
        }

        public FillType Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        [Browsable(false)]
        public IVertex StartVertex
        {
            get { return _vertices[0]; }
            set
            {
                _vertices[0] = value;
                ReCalculateVertices();
            }
        }

        [Browsable(false)]
        public IVertex EndVertex
        {
            get { return _vertices[2]; }
            set
            {
                _vertices[2] = value;
                ReCalculateVertices();
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Rectangle"; }
        }

        public bool HitTest(IVertex point)
        {
            return Util.CheckInnerPoint(_vertices, point);
        }

        public void ReCalculateVertices()
        {
            _vertices[1] = new Vertex(_vertices[2].X, _vertices[0].Y);
            _vertices[3] = new Vertex(_vertices[0].X, _vertices[2].Y);
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Rectangle;
        }

        public IShape Clone()
        {
            var obj = new Quad
            {
                ShapePen = (Pen)ShapePen.Clone(),
                ShapeBrush = (Brush)ShapeBrush.Clone(),
                Fill = _fill,
                StartVertex = StartVertex.Clone(),
                EndVertex = EndVertex.Clone(),
                Vertices = _vertices.Clone()
            };
            return obj;
        }
    }
}
