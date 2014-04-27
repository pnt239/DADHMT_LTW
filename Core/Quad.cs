using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Quad : IShape
    {
        private List<Point> _vertices;
        private FillType _fill;

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
        public Pen ShapePen { get; set; }

        [Browsable(false)]
        public Brush ShapeBrush { get; set; }

        public FillType Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        [Browsable(false)]
        public Point StartVertex
        {
            get { return _vertices[0]; }
            set { _vertices[0] = value; }
        }

        [Browsable(false)]
        public Point EndVertex
        {
            get { return _vertices[2]; }
            set { _vertices[2] = value; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Rectangle"; }
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
