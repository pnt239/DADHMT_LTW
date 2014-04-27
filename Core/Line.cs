using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Line : IShape
    {
        private List<Point> _vertices;
        private FillType _fill;

        public Line()
        {
            _vertices = new List<Point> { new Point(), new Point()};
            ShapePen = new Pen(Color.Black);
            _fill = FillType.NoFill;
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

        [Browsable(false)]
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
            get { return _vertices[1]; }
            set { _vertices[1] = value; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Line"; }
        }

        public void FinishEdition()
        {
            //
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Line;
        }

        public IShape Clone()
        {
            var obj = new Line
            {
                Fill = _fill
            };
            return obj;
        }
    }
}
