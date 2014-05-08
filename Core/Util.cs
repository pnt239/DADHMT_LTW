using System;
using System.Collections.Generic;
using System.Drawing;

namespace TabletC.Core
{
    public class Util
    {
        // Tao ra 1 diem moi de co duoc vung bao quanh la 1 hinh vuong
        public static Point CreateSnapPoint(int x, int y, Point anchor)
        {
            var p = new Point(x, y);
            int dx, dy, adx, ady;

            if ((adx = Math.Abs(dx = anchor.X - p.X)) >
                (ady = Math.Abs(dy = anchor.Y - p.Y)))
                p.Y = anchor.Y - (dy == 0 ? 0 : adx * dy / ady);
            else
                p.X = anchor.X - (dx == 0 ? 0 : ady * dx / adx);

            return p;
        }

        // Tao vung chu nhat bao boc xung quanh
        public static Rectangle CreateBorder(IShape shape)
        {
            switch (shape.GetShapeType())
            {
                case ShapeType.Rectangle:
                case ShapeType.Polygon:
                    return CreatePolygonBorder(shape.Vertices);
                case ShapeType.Circle:
                case ShapeType.Ellipse:
                    return new Rectangle(shape.StartVertex.X, shape.StartVertex.Y,
                        Math.Abs(shape.EndVertex.X - shape.StartVertex.X),
                        Math.Abs(shape.EndVertex.Y - shape.StartVertex.Y));
                default:
                    return new Rectangle();
            }
        }

        // Tao vung bao boc xung quanh da giac
        private static Rectangle CreatePolygonBorder(IList<Point> points)
        {
            int xmin = points[0].X, ymin = points[0].Y, xmax = xmin, ymax = ymin;
            foreach (var p in points)
            {
                if (p.X > xmax) xmax = p.X;
                if (p.X < xmin) xmin = p.X;
                if (p.Y > ymax) ymax = p.Y;
                if (p.Y < ymin) ymin = p.Y;
            }
            return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        // Kiem tra 1 diem co nam trong da giac khong
        public static bool CheckInnerPoint(IList<Point> points, Point point)
        {
            PointF currentPoint = point;
            //Ray-cast algorithm is here onward
            int k, j = points.Count - 1;
            var oddNodes = false; //to check whether number of intersections is odd

            for (k = 0; k < points.Count; k++)
            {
                //fetch adjucent points of the polygon
                PointF polyK = points[k];
                PointF polyJ = points[j];

                //check the intersections
                if (((polyK.Y > currentPoint.Y) != (polyJ.Y > currentPoint.Y)) &&
                 (currentPoint.X < (polyJ.X - polyK.X) * (currentPoint.Y - polyK.Y) / (polyJ.Y - polyK.Y) + polyK.X))
                    oddNodes = !oddNodes; //switch between odd and even
                j = k;
            }

            //if odd number of intersections
            if (oddNodes)
            {
                //mouse point is inside the polygon
                return true;
            }

            //if even number of intersections
            return false;
        }
    }
}
