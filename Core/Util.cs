using System;
using System.Collections.Generic;
using System.Drawing;

namespace TabletC.Core
{
    public class Util
    {
        public const float Epsilon = 1.0e-15f;

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
        public static RectangleF CreateShapeBound(IShape shape)
        {
            switch (shape.GetShapeType())
            {
                case ShapeType.Triangle:
                case ShapeType.Rectangle:
                case ShapeType.RegPolygon:
                case ShapeType.Polygon:
                    return CreatePolygonBorder(shape.Vertices);
                case ShapeType.Circle:
                case ShapeType.Ellipse:
                    return new RectangleF((float) shape.StartVertex.X, (float) shape.StartVertex.Y,
                        (float) Math.Abs(shape.EndVertex.X - shape.StartVertex.X),
                        (float) Math.Abs(shape.EndVertex.Y - shape.StartVertex.Y));
                default:
                    return new Rectangle();
            }
        }

        public static Rectangle CreateShapeBound(Point start, Point end)
        {
            var rec = new Rectangle
            {
                X = Math.Min(start.X, end.X),
                Y = Math.Min(start.Y, end.Y),
                Width = Math.Abs(start.X - end.X),
                Height = Math.Abs(start.Y - end.Y)
            };

            return rec;
        }

        // Tao vung bao boc xung quanh da giac
        private static RectangleF CreatePolygonBorder(IVertexCollection points)
        {
            Double xmin = points[0].X, ymin = points[0].Y, xmax = xmin, ymax = ymin;
            foreach (var p in points)
            {
                if (p.X > xmax) xmax = p.X;
                if (p.X < xmin) xmin = p.X;
                if (p.Y > ymax) ymax = p.Y;
                if (p.Y < ymin) ymin = p.Y;
            }
            return new RectangleF((float) xmin, (float) ymin, (float) (xmax - xmin), (float) (ymax - ymin));
        }

        // Kiem tra 1 diem co nam trong da giac khong
        public static bool CheckInnerPoint(IVertexCollection points, IVertex point)
        {
            IVertex currentPoint = point;
            //Ray-cast algorithm is here onward
            int k, j = points.Count - 1;
            var oddNodes = false; //to check whether number of intersections is odd

            for (k = 0; k < points.Count; k++)
            {
                //fetch adjucent points of the polygon
                IVertex polyK = points[k];
                IVertex polyJ = points[j];

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

        public static string GetUnitSign(MessureUnit unit)
        {
            switch (unit)
            {
                case MessureUnit.Milimeters:
                    return "mm";
                default:
                    return "";
            }
        }

        public static Double ConvertFromMilimeter(MessureUnit unit, double value)
        {
            Double ret = 0;
            switch (unit)
            {
                case MessureUnit.Milimeters:
                    ret = value;
                    break;
                case MessureUnit.Centimeters:
                    ret = value/10.0;
                    break;
                case MessureUnit.Inches:
                    ret = value/254;
                    break;
            }
            return ret;
        }

        public static Double GetDefaultScale(MessureUnit unit)
        {
            // 790 is width default in pixel
            return 790.0/ConvertFromMilimeter(unit, 210.0);
        }

        public static Double WinToView(double value, MessureUnit unit)
        {
            return Math.Round(GetDefaultScale(unit)*value);
        }

        public static Double ViewToWin(double value, MessureUnit unit)
        {
            return value/GetDefaultScale(unit);
        }

        public static int DtoI(double dec)
        {
            return (int) Math.Round(dec);
        }

        public static int FtoI(float dec)
        {
            return (int)Math.Round(dec);
        }

        public static Rectangle RecFToRec(RectangleF rec)
        {
            return new Rectangle(FtoI(rec.X), FtoI(rec.Y),
                FtoI(rec.Width), FtoI(rec.Height));
        }
    }
}
