using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    [Serializable]
    public class Quad : IShape
    {
        private List<Point> _vertices;
        private FillType _fill;
        [NonSerialized]
        private Pen _pen;
        [NonSerialized]
        private Brush _brush;

        public Quad()
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);
            ShapeBrush = new SolidBrush(Color.White);
            _fill = FillType.NoFill;

            _vertices.Add(new Point());
            _vertices.Add(new Point());
            _vertices.Add(new Point());
            _vertices.Add(new Point());
        }

        [Browsable(false)]
        public List<Point> Vertices
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
        public Point StartVertex
        {
            get { return _vertices[0]; }
            set
            {
                _vertices[0] = value;
                FinishEdition();
            }
        }

        [Browsable(false)]
        public Point EndVertex
        {
            get { return _vertices[2]; }
            set
            {
                _vertices[2] = value;
                FinishEdition();
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Rectangle"; }
        }

        public bool HitTest(Point point)
        {
            return point.X >= _vertices[0].X && point.X <= _vertices[2].X && point.Y >= _vertices[0].Y &&
                   point.Y <= _vertices[2].Y;
        }

        public void FinishEdition()
        {
            _vertices[1] = new Point(_vertices[2].X, _vertices[0].Y);
            _vertices[3] = new Point(_vertices[0].X, _vertices[2].Y);
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Rectangle;
        }

        public IShape Clone()
        {
            var obj = new Quad
            {
                Fill = _fill,
            };
            return obj;
        }
    }
}
