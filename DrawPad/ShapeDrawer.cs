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
        private Graphics _graphic;
        private bool _useLibrary;

        public ShapeDrawer()
        {
            _useLibrary = false;
        }

        /* true - use sharpGL to draw */
        public bool UseLibrary
        {
            get { return _useLibrary; }
            set { _useLibrary = value; }
        }

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
            }
        }

        private void DrawLine(Line line)
        {
            _graphic.DrawLine(line.ShapePen, line.StartVertex, line.EndVertex);
        }

        private void DrawRectangle(Quad rectangle)
        {
            _graphic.DrawRectangle(rectangle.ShapePen, CreateShapeArea(rectangle.StartVertex, rectangle.EndVertex));
            // test
        }
        private void DrawPoplygon(Polygon polygon)
        {
            //_graphic.DrawPolygon(polygon.ShapePen, )
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
            Rectangle rec = CreateShapeArea(triangle.StartVertex, triangle.EndVertex);
            _graphic.DrawLine(triangle.ShapePen, rec.X, rec.Y + rec.Height, rec.X + rec.Width, rec.Y + rec.Height);
            _graphic.DrawLine(triangle.ShapePen, rec.X, rec.Y + rec.Height, rec.X + rec.Width/2, rec.Y);
            _graphic.DrawLine(triangle.ShapePen, rec.X + rec.Width/2, rec.Y, rec.X + rec.Width, rec.Y + rec.Height);
        }

        private Rectangle CreateShapeArea(Point p1, Point p2)
        {
            var rec = new Rectangle();
            rec.X = p1.X < p2.X ? p1.X : p2.X;
            rec.Y = p1.Y < p2.Y ? p1.Y : p2.Y;

            rec.Width = Math.Abs(p1.X - p2.X);
            rec.Height = Math.Abs(p1.Y - p2.Y);
            return rec;
        }
    }
}
