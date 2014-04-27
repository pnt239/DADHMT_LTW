using System;
using System.Collections.Generic;
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

        public Polygon(Point start, Point end)
        {
            _vertices = new List<Point>();
            _startVertex = start;
            _endVertex = end;
            FileType = FillType.NoFill;
        }

        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Pen ShapePen { get; set; }

        public Brush ShapeBrush { get; set; }

        public FillType FileType { get; set; }

        public Point StartVertex
        {
            get { return _startVertex; }
            set { _startVertex = value; }
        }

        public Point EndVertex
        {
            get { return _endVertex; }
            set { _endVertex = value; }
        }

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
            var obj = new Polygon(_startVertex, _endVertex) {FileType = FileType};
            return obj;
        }
    }
}
