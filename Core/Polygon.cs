using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Polygon : IShape
    {
        private IVertexCollection _vertices;
        private IVertex _start;
        private IVertex _end;
        private FillType _fill;

        public Polygon()
        {
            _vertices = new VertexCollection();

            ShapePen = new Pen(Color.Black);
            ShapeBrush = Brushes.Transparent;
            _fill = FillType.NoFill;

            _start = _end = new Vertex();
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
            set { _end = value; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Polygon"; }
        }

        public bool HitTest(IVertex point)
        {
            return Util.CheckInnerPoint(_vertices, point);
        }

        public void ReCalculateVertices()
        {
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Polygon;
        }

        public IShape Clone()
        {
            var obj = new Polygon
            {
                ShapePen = (Pen)ShapePen.Clone(),
                ShapeBrush = (Brush)ShapeBrush.Clone(),
                Fill = _fill
            };
            return obj;
        }
    }
}
