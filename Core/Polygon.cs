using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TabletC.Core
{
    public class Polygon : IShape
    {
        private List<Point> _vertices;
        private int _sides;

        public Polygon(Point start, Point end)
        {
            _vertices = new List<Point> { start, end };
            ShapePen = new Pen(Color.Black);
            FileType = FillType.Outline;

            _sides = 5;
        }

        [Browsable(false)]
        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
            //test
        }

        [Browsable(false)]
        public Pen ShapePen { get; set; }

        public Brush ShapeBrush { get; set; }

        public FillType FileType { get; set; }

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

        public int Sides
        {
            get { return _sides; }
            set
            {
                _sides = value;
                if (_sides < 3) _sides = 3;
                if (_sides > 100) _sides = 100;
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Polygon"; }
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
            var obj = new Polygon(new Point(), new Point());
            obj.Sides = _sides;
            return obj;
        }
    }
}
