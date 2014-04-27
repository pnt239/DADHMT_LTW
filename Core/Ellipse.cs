using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using TabletC.Core;

namespace TabletC.Core
{
    public class Ellipse : IShape
    {
        private List<Point> _vertices;
        private FillType _fill;

        public Ellipse()
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);
            _fill = FillType.NoFill;

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
        public int MajorAxis
        {
            get { return Math.Abs(EndVertex.X - StartVertex.X) / 2; }
        }

        [Browsable(false)]
        public int MinorAxis
        {
            get { return Math.Abs(EndVertex.Y - StartVertex.Y) / 2; }
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
            get { return _vertices[1]; }
            set { _vertices[1] = value; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Ellipse"; }
        }

        public void FinishEdition()
        {
            //
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Ellipse;
        }

        public IShape Clone()
        {
            var obj = new Ellipse
            {
                Fill = _fill,
                ShapePen = ShapePen,
                ShapeBrush = ShapeBrush
            };
            return obj;
        }
    }
}
