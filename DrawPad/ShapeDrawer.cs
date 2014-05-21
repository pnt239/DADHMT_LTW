using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Drawing;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public class ShapeDrawer
    {
        private Graphics _graphs;

        public ShapeDrawer()
        {
            UseLibrary = false;
        }

        /* true - use sharpGL to draw */
        public bool UseLibrary { get; set; }

        public void Draw(IShape shape, Graphics graphic)
        {
            if (shape.EndVertex.Equals(shape.StartVertex) && shape.Vertices.Count == 2)
                return;

            _graphs = graphic;

            /* Draw shape*/
            switch (shape.GetShapeType())
            {
                case ShapeType.Line:
                case ShapeType.Rectangle:
                case ShapeType.Triangle:
                case ShapeType.RegPolygon:
                    DrawClosedShape(shape, true);
                    break;
                case ShapeType.Polygon:
                    DrawPolygon((Polygon)shape);
                    break;
                case ShapeType.Ellipse:
                    DrawEllipse((Ellipse) shape);
                    break;
            }
        }

        public void DrawClosedShape(IShape shape, bool closed)
        {
            GraphicsPath path = new GraphicsPath();
            if (closed) path.StartFigure();
            path.AddLines(shape.Vertices.ToPoints());
            if (closed) path.CloseFigure();

            _graphs.DrawPath(shape.ShapePen, path);
        }

        /*
        private void DrawLine(Line line)
        {
            _graphic.DrawLine(line.ShapePen, line.StartVertex, line.EndVertex);
        }

        private void DrawRectangle(Quad rectangle)
        {
            _graphic.DrawRectangle(rectangle.ShapePen, CreateShapeArea(rectangle.StartVertex, rectangle.EndVertex));
        }
         
        private void DrawRegPolygon(RegPolygon polygon)
        {
            //var radius = (int)Math.Sqrt((polygon.StartVertex.X - polygon.EndVertex.X)*(polygon.StartVertex.X - polygon.EndVertex.X) + (polygon.StartVertex.Y - polygon.EndVertex.Y)*(polygon.StartVertex.Y - polygon.EndVertex.Y));
            //var startAngle = (int)XYToDegrees(polygon.EndVertex, polygon.StartVertex);

            //_polygonPoints = CalculateVertices(polygon.Sides, radius, startAngle, polygon.StartVertex);

            _graphic.DrawPolygon(polygon.ShapePen, polygon.Verticess.ToArray());
        }
        */
        private void DrawPolygon(Polygon polygon)
        {
            if (polygon.Vertices == null || polygon.Vertices.Count < 1)
                return;

            // polygon.EndVertex.X != -1
            bool endpol = polygon.EndVertex.Equals(polygon.StartVertex);

            if (!endpol)
                _graphs.DrawLine(polygon.ShapePen,
                    polygon.Vertices[polygon.Vertices.Count - 1].ToPoint(), 
                    polygon.EndVertex.ToPoint());

            if (polygon.Vertices.Count < 2)
                return;

            if (endpol)
                DrawClosedShape(polygon, true);
            else
                DrawClosedShape(polygon, false);
        }
     
        private void DrawEllipse(Ellipse ellipse)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(Util.CreateShapeBound(ellipse.StartVertex.ToPoint(),
                ellipse.EndVertex.ToPoint()));

            _graphs.DrawPath(ellipse.ShapePen, path);
        }
        /*
        private void DrawTriangle(Triangle triangle)
        {
            //Rectangle rec = CreateShapeArea(triangle.StartVertex, triangle.EndVertex);
            //_graphic.DrawLine(triangle.ShapePen, rec.X, rec.Y + rec.Height, rec.X + rec.Width, rec.Y + rec.Height);
            //_graphic.DrawLine(triangle.ShapePen, rec.X, rec.Y + rec.Height, rec.X + rec.Width/2, rec.Y);
            //_graphic.DrawLine(triangle.ShapePen, rec.X + rec.Width/2, rec.Y, rec.X + rec.Width, rec.Y + rec.Height);
            _graphic.DrawLines(triangle.ShapePen, new Point[] { triangle.Verticess[0], triangle.Verticess[1], triangle.Verticess[2], triangle.Verticess[0] });
        }

        public static Rectangle CreateShapeArea(Point p1, Point p2)
        {
            var rec = new Rectangle
            {
                X = p1.X < p2.X ? p1.X : p2.X,
                Y = p1.Y < p2.Y ? p1.Y : p2.Y,
                Width = Math.Abs(p1.X - p2.X),
                Height = Math.Abs(p1.Y - p2.Y)
            };

            return rec;
        }

        private Point DegreesToXY(float degrees, float radius, Point origin)
        {
            var xy = new Point();
            double radians = degrees * Math.PI / 180.0;

            xy.X = (int)(Math.Cos(radians) * radius + origin.X);
            xy.Y = (int)(Math.Sin(-radians) * radius + origin.Y);

            return xy;
        }

        private float XYToDegrees(Point xy, Point origin)
        {
            int deltaX = origin.X - xy.X;
            int deltaY = origin.Y - xy.Y;

            double radAngle = Math.Atan2(deltaY, deltaX);
            double degreeAngle = radAngle * 180.0 / Math.PI;

            return (float)(180.0 - degreeAngle);
        }

        private Point[] CalculateVertices(int sides, int radius, int startingAngle, Point center)
        {
            if (sides < 3)
                throw new ArgumentException("Polygon must have 3 sides or more.");

            var points = new List<Point>();
            float step = 360.0f / sides;

            float angle = startingAngle; //starting angle
            for (double i = startingAngle; i < startingAngle + 360.0; i += step) //go in a full circle
            {
                points.Add(DegreesToXY(angle, radius, center)); //code snippet from above
                angle += step;
            }

            return points.ToArray();
        }
         */
    }
}
