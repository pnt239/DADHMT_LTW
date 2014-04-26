using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public class ShapeDrawer
    {
        Point[] _polygonPoints;

        private Graphics _graphic;

        public ShapeDrawer()
        {
            UseLibrary = false;
        }

        /* true - use sharpGL to draw */
        public bool UseLibrary { get; set; }

        public void Draw(object graphic, IShape shape)
        {
            if (shape.EndVertex.Equals(shape.StartVertex))
                return;

            _graphic = (Graphics)graphic;

            /* Draw shape*/
            switch (shape.GetShapeType())
            {
                case ShapeType.Line:
                    DrawLine((Line)shape);
                    break;
                case ShapeType.Circle:
                    DrawCircle((Circle)shape);
                    break;
                case ShapeType.Rectangle:
                    DrawRectangle((Quad)shape);
                    break;
                case ShapeType.Ellipse:
                    DrawEllipse((Ellipse)shape);
                    break;
                case ShapeType.Triangle:
                    DrawTriangle((Triangle)shape);
                    break;
                case ShapeType.Polygon:
                    DrawPoplygon((Polygon)shape);
                    break;
            }
        }

        private void DrawLine(Line line)
        {
            _graphic.DrawLine(line.ShapePen, line.StartVertex, line.EndVertex);
        }

        private void DrawRectangle(Quad rectangle)
        {
            _graphic.DrawRectangle(rectangle.ShapePen, CreateShapeArea(rectangle.StartVertex, rectangle.EndVertex));
        }

        private void DrawPoplygon(Polygon polygon)
        {
            //Rectangle rec = CreateShapeArea(polygon.StartVertex, polygon.EndVertex);
  
            var radius = (int)Math.Sqrt((polygon.StartVertex.X - polygon.EndVertex.X)*(polygon.StartVertex.X - polygon.EndVertex.X) + (polygon.StartVertex.Y - polygon.EndVertex.Y)*(polygon.StartVertex.Y - polygon.EndVertex.Y));
            var startAngle = (int)XYToDegrees(polygon.EndVertex, polygon.StartVertex);

            _polygonPoints = CalculateVertices(polygon.Sides, radius, startAngle, polygon.StartVertex);

            _graphic.DrawPolygon(polygon.ShapePen, _polygonPoints);
        }

        private void DrawCircle(Circle circle)
        {
            _graphic.DrawEllipse(circle.ShapePen, CreateShapeArea(circle.StartVertex, circle.EndVertex));
        }

        private void DrawEllipse(Ellipse ellipse)
        {
            _graphic.DrawEllipse(ellipse.ShapePen, CreateShapeArea(ellipse.StartVertex, ellipse.EndVertex));
        }

        private void DrawTriangle(Triangle triangle)
        {
            //Rectangle rec = CreateShapeArea(triangle.StartVertex, triangle.EndVertex);
            //_graphic.DrawLine(triangle.ShapePen, rec.X, rec.Y + rec.Height, rec.X + rec.Width, rec.Y + rec.Height);
            //_graphic.DrawLine(triangle.ShapePen, rec.X, rec.Y + rec.Height, rec.X + rec.Width/2, rec.Y);
            //_graphic.DrawLine(triangle.ShapePen, rec.X + rec.Width/2, rec.Y, rec.X + rec.Width, rec.Y + rec.Height);
            _graphic.DrawLines(triangle.ShapePen, new Point[] { triangle.Vertices[0], triangle.Vertices[1], triangle.Vertices[2], triangle.Vertices[0] });
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
    }
}
