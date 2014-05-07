using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using TabletC.Core;

namespace TabletC.DrawPad
{
    internal class CColor
    {
        protected bool Equals(CColor other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CColor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = R.GetHashCode();
                hashCode = (hashCode*397) ^ G.GetHashCode();
                hashCode = (hashCode*397) ^ B.GetHashCode();
                hashCode = (hashCode*397) ^ A.GetHashCode();
                return hashCode;
            }
        }

        public CColor()
        {
            R = G = B = A = 0;
        }

        public CColor(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public CColor(Color c)
        {
            A = c.A;
            R = c.R;
            G = c.G;
            B = c.B;
        }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public static bool operator ==(CColor c1, CColor c2)
        {
            if (ReferenceEquals(null, c1)) return false;
            if (ReferenceEquals(null, c2)) return false;
            return (c1.A == c2.A) || (c1.R == c2.R) || (c1.G == c2.G) || (c1.B == c2.B);
        }

        public static bool operator !=(CColor c1, CColor c2)
        {
            if (ReferenceEquals(null, c1)) return false;
            if (ReferenceEquals(null, c2)) return false;
            return (c1.A != c2.A) || (c1.R != c2.R) || (c1.G != c2.G) || (c1.B != c2.B);
        }
    }

    internal class CActiveEdge : IComparable<CActiveEdge>
    {
        public int YUper { get; set; }
        public double XIntersection { get; set; }
        public double ReciSlope { get; set; }

        public int CompareTo(CActiveEdge other)
        {
            if (XIntersection < other.XIntersection)
                return -1;
            if (XIntersection > other.XIntersection)
                return 1;
            return 0;
        }
    }

    class ShapeFiller
    {
        public void FillByFlood(Layer layer, IShape shape, Point? pstart)
        {
            var st = shape.GetShapeType();
            if ((st == ShapeType.Line) ||
                (st != ShapeType.Polygon 
                && (shape.StartVertex.X == shape.EndVertex.X || shape.StartVertex.Y == shape.EndVertex.Y)) ||
                (st == ShapeType.Polygon && shape.EndVertex.X != -1))
                return;

            if (pstart == null)
                pstart = GetInnerPoint(shape);

            var colorFill = new CColor(((SolidBrush)shape.ShapeBrush).Color);
            var colorBound = new CColor(shape.ShapePen.Color);
            var shapeBorder = Util.CreateBorder(shape);

            int w = layer.ImageBuffer.Width;
            int h = layer.ImageBuffer.Height;

            BitmapData pixelData = layer.ImageBuffer.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, layer.ImageBuffer.PixelFormat);

            IntPtr ptr = pixelData.Scan0;
            int bytes = pixelData.Stride * pixelData.Height; //.ImageBuffer.Height;
            var rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            
            // Fill at here
            int stride = 4 * w; // linesize


            var queue = new Queue<Point>();

            //start the loop
            QueueFloodFill4(ref rgbValues, ref queue, pstart.Value.X, pstart.Value.Y, w, h, stride, colorFill, colorBound);
			//call next item on queue
            while (queue.Count > 0)
            {
                var pt = queue.Dequeue();
                QueueFloodFill4(ref rgbValues, ref queue, pt.X, pt.Y, w, h, stride, colorFill, colorBound);
            }
            // End Fill

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            layer.ImageBuffer.UnlockBits(pixelData);
        }

        public void GdiFill(Layer layer, IShape shape)
        {
            layer.GraphicsBuffer.FillRectangle(shape.ShapeBrush,
                ShapeDrawer.CreateShapeArea(shape.StartVertex, shape.EndVertex));
        }



        private void QueueFloodFill4(ref byte[] arr, ref Queue<Point> queue, int x, int y, int w, int h, int stride, CColor cfill, CColor cbound)
        {
            //don't go over the edge
            if (x < 0 || y < 0 || x >= w || y >= h)
                return;

            //calculate pointer offset
            var color = GetColorFromArray(ref arr, x, y, stride);

            //if the pixel is within the color tolerance, fill it and branch out
            if ((color != cfill) && (color != cbound))
            {
                SetColorToArray(ref arr, x, y, stride, cfill);

                queue.Enqueue(new Point(x + 1, y));
                queue.Enqueue(new Point(x, y + 1));
                queue.Enqueue(new Point(x - 1, y));
                queue.Enqueue(new Point(x, y - 1));
            }
        }

        public void PaintByScanline(ref Layer layer, IShape shape)
        {
            
            var colorFill = new CColor(((SolidBrush)shape.ShapeBrush).Color);
            Color fillColor = Color.FromArgb(colorFill.A, colorFill.R, colorFill.G, colorFill.B);

            switch (shape.GetShapeType())
            {
                case ShapeType.Rectangle:
                case ShapeType.Polygon:
                    ScanLineFillPolygon(ref layer, ref shape, fillColor);
                    break;
                case ShapeType.Ellipse:
                    ScanLineFillEllipse(ref layer, (Ellipse)shape, fillColor);
                    break;
            }


        }

        private void ScanLineFillPolygon(ref Layer layer, ref IShape shape, Color fillColor)
        {
            if ((shape.StartVertex.X == shape.EndVertex.X && shape.StartVertex.Y == shape.EndVertex.Y) ||
                (shape.GetShapeType() == ShapeType.Polygon && shape.EndVertex.X != -1))
                return;

            var rec = Util.CreateBorder(shape);
            var h = rec.Y + 1;
            var et = new SortedDoublyLinkedList<CActiveEdge>[h];
            var active = new SortedDoublyLinkedList<CActiveEdge>();

            for (int i = 0; i < h; i++)
                et[i] = new SortedDoublyLinkedList<CActiveEdge>();

            BuildEdgeList(shape.Vertices, ref et);

            for (int i = rec.Y - rec.Height; i < rec.Y; i++)
            {
                buildActiveList(ref active, ref et[i]);
                if (active.Count != 0)
                {
                    FillScan(i, ref active,layer,fillColor);
                    updateEdgeList(i, ref active);
                    active.Sort();
                }
            }
        }

        private void ScanLineFillEllipse(ref Layer layer, Ellipse shape, Color FillColor)
        {
            int rx = shape.MajorAxis, ry = shape.MinorAxis;
            var o = new Point((shape.EndVertex.X + shape.StartVertex.X) / 2,
                (shape.EndVertex.Y + shape.StartVertex.Y) / 2);

            int x = 0, y = ry;
            int c1 = 2 * ry * ry * x, c2 = 2 * rx * rx * y;
            float p = ry * ry - rx * rx * ry + 0.25F * rx * rx;

            while (c1 < c2)
            {
                Fill2Line(o.X, o.Y, x, y,layer,FillColor);

                x++;
                if (p < 0)
                {
                    c1 += 2 * ry * ry;
                    p += c1 + ry * ry;
                }
                else
                {
                    y--;
                    c1 += 2 * ry * ry;
                    c2 -= 2 * rx * rx;
                    p += c1 - c2 + ry * ry;
                }
            }

            c1 = 2 * rx * rx * y;
            c2 = 2 * ry * ry * x;
            p = ry * ry * (x + 0.5F) * (x + 0.5F) + rx * rx * (y - 1) * (y - 1) - rx * rx * ry * ry;

            while (y != 0)
            {
                Fill2Line(o.X, o.Y, x, y, layer, FillColor);

                y--;
                if (p > 0)
                {
                    c1 -= 2 * rx * rx;
                    p += rx * rx - c1;
                }
                else
                {
                    x++;
                    c1 -= 2 * rx * rx;
                    c2 += 2 * ry * ry;
                    p += c2 - c1 + rx * rx;
                }
            }
            Fill2Line(o.X, o.Y, x, y, layer, FillColor);
        }

        private void SetColorToArray(ref byte[] arr, int x, int y, int stride, CColor c)
        {
            arr[y * stride + x * 4] = c.B;
            arr[y * stride + x * 4 + 1] = c.G;
            arr[y * stride + x * 4 + 2] = c.R;
            arr[y * stride + x * 4 + 3] = c.A;
        }

        private CColor GetColorFromArray(ref byte[] arr, int x, int y, int linesize)
        {
            return new CColor(arr[y * linesize + x * 4 + 3], arr[y * linesize + x * 4 + 2], arr[y * linesize + x * 4 + 1], arr[y * linesize + x * 4]);
        }

        private Point GetInnerPoint(IShape shape)
        {
            var ret = new Point();
            switch (shape.GetShapeType())
            {
                case ShapeType.Rectangle:
                {
                    ret.X = (shape.Vertices[0].X + shape.Vertices[2].X)/2;
                    ret.Y = (shape.Vertices[0].Y + shape.Vertices[2].Y)/2;
                }
                    break;
                case ShapeType.Ellipse:
                {
                    ret.X = (shape.StartVertex.X + shape.EndVertex.X) / 2;
                    ret.Y = (shape.StartVertex.Y + shape.EndVertex.Y) / 2;
                }
                    break;
                case ShapeType.Triangle:
                {
                    ret.X = (shape.Vertices[0].X + shape.Vertices[1].X + shape.Vertices[2].X) / 3;
                    ret.Y = (shape.Vertices[0].Y + shape.Vertices[1].Y + shape.Vertices[2].Y) / 3;
                }
                    break;
                case ShapeType.RegPolygon:
                case ShapeType.Polygon:
                {
                    var finded = false;
                    for (int i = 0; i <  shape.Vertices.Count; i++)
                    {
                        for (int j = i+2; j < shape.Vertices.Count; j++)
                        {
                            ret.X = (shape.Vertices[i].X + shape.Vertices[j].X) / 2;
                            ret.Y = (shape.Vertices[i].Y + shape.Vertices[j].Y) / 2;
                            finded = Util.CheckInnerPoint(shape.Vertices, ret);
                            if (finded)
                                break;
                        }
                        if (finded)
                            break;
                    }
                }
                    break;
            }
            return ret;
        }

        private void BuildEdgeList(IList<Point> points, ref SortedDoublyLinkedList<CActiveEdge>[] et)
        {
            var cnt = points.Count;
            int i, yPrev = points[cnt - 2].Y;

            Point v1 = points[cnt - 1];
            for (i = 0; i < cnt; i++)
            {
                Point v2 = points[i];
                if (v1.Y != v2.Y)
                {
                    // Nonhorizontal line
                    if (v1.Y < v2.Y) // up-going edge
                        makeEdgeRec(ref v1, ref v2, yNext(i, cnt, ref points), ref et);
                    else             // down-going edge
                        makeEdgeRec(ref v2, ref v1, yPrev, ref et);
                }
                yPrev = v1.Y;
                v1 = v2;
            }
        }

        private void buildActiveList(ref SortedDoublyLinkedList<CActiveEdge> dest, ref SortedDoublyLinkedList<CActiveEdge> source)
        {
            foreach (var edge in source)
                dest.Add(edge);
        }

        private void FillScan(int line, ref SortedDoublyLinkedList<CActiveEdge> ae,Layer layer,Color FillColor)
        {
            var points = new CActiveEdge[2];
            int i = 0;

            foreach (var edge in ae)
            {
                points[i] = edge;

                if (i == 1)
                    FillLine((int)Math.Round(points[0].XIntersection, 0), (int)Math.Round(points[1].XIntersection, 0), line,layer,FillColor);

                i = (i + 1) % 2;
            }
        }

        private void FillLine(int x1, int x2, int y,Layer layer,Color FillColor)
        {
            //int w = layer.ImageBuffer.Width;
            //int h = layer.ImageBuffer.Height;
            //for (var j = x1; j < x2; j++)
            //{
            //    if (j < 0 || y < 0 || j >= w || y >= h)
            //        return;
            //    else layer.ImageBuffer.SetPixel(j, y, FillColor);
            //}
            layer.GraphicsBuffer.DrawLine(new Pen(FillColor), x1, y, x2, y);
            //_graphic.Vertex(j, y);
        }

        private void updateEdgeList(int line, ref SortedDoublyLinkedList<CActiveEdge> ae)
        {
            var p = ae.First;

            while (p != null)
            {
                if (line >= p.Value.YUper)
                {
                    ae.Remove(p);
                }
                else
                    p.Value.XIntersection += p.Value.ReciSlope;
                p = p.Next;
            }
        }

        private void makeEdgeRec(ref Point lower, ref Point upper, int yComp, ref SortedDoublyLinkedList<CActiveEdge>[] et)
        {
            var ae = new CActiveEdge
            {
                ReciSlope = (float)(upper.X - lower.X) / (upper.Y - lower.Y),
                XIntersection = lower.X
            };

            // Make shorter edge
            if (upper.Y < yComp)
                ae.YUper = upper.Y - 1;
            else
                ae.YUper = upper.Y;
            et[lower.Y].Add(ae);
        }

        private int yNext(int k, int cnt, ref IList<Point> points)
        {
            int j;

            if (k + 1 > cnt - 1)
                j = 0;
            else
                j = k + 1;

            while (points[k].Y == points[j].Y)
                if (j + 1 > cnt - 1)
                    j = 0;
                else
                    j++;

            return points[j].Y;
        }

        private void Fill4Line(int xc, int yc, int x, int y,Layer layer,Color FillColor)
        {
            FillLine(-x + xc, x + xc, y + yc, layer, FillColor);
            FillLine(-y + xc, y + xc, x + yc, layer, FillColor);
            FillLine(-y + xc, y + xc, -x + yc, layer, FillColor);
            FillLine(-x + xc, x + xc, -y + yc, layer, FillColor);
        }

        private void Fill2Line(int xc, int yc, int x, int y, Layer layer, Color FillColor)
        {
            FillLine(-x + xc, x + xc, y + yc, layer, FillColor);
            FillLine(-x + xc, x + xc, -y + yc, layer, FillColor);
        }
    }
}
