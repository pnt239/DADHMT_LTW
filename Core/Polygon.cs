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
        private List<Point> _vertices;
        private Point _startVertex;
        private Point _endVertex;
        private FillType _fill;

        public Polygon()
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);
            _fill = FillType.NoFill;

            _startVertex = new Point();
            _endVertex = new Point();
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
            get { return _startVertex; }
            set { _startVertex = value; }
        }

        [Browsable(false)]
        public Point EndVertex
        {
            get { return _endVertex; }
            set { _endVertex = value; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Polygon"; }
        }

        public bool HitTest(Point point)
        {
            return Util.CheckInnerPoint(_vertices, point);
        }

        public void FinishEdition()
        {
            //
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Polygon;
        }

        public IShape Clone()
        {
            var obj = new Polygon
            {
                Fill = _fill
            };
            return obj;
        }
    }
}
