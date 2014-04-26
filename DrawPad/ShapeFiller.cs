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

    class ShapeFiller
    {
        public void FloodFill(Layer layer, IShape shape, Point? pstart)
        {
            if (pstart == null)
                pstart = GetInnerPoint(shape);

            if (shape.GetShapeType() == ShapeType.Line)
                return;

            if (shape.StartVertex.X == shape.EndVertex.X ||
                shape.StartVertex.Y == shape.EndVertex.Y)
                return;

            var colorFill = new CColor(((SolidBrush)shape.ShapeBrush).Color);
            var colorBound = new CColor(shape.ShapePen.Color);

            BitmapData pixelData = layer.ImageBuffer.LockBits(new Rectangle(0, 0, layer.ImageBuffer.Width, layer.ImageBuffer.Height), ImageLockMode.ReadOnly, layer.ImageBuffer.PixelFormat);

            IntPtr ptr = pixelData.Scan0;

            int bytes = pixelData.Stride * layer.ImageBuffer.Height;
            var rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Fill at here
            int w = layer.ImageBuffer.Width;
            int h = layer.ImageBuffer.Height;
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

        public void QueueFloodFill4(ref byte[] arr, ref Queue<Point> queue, int x, int y, int w, int h, int stride, CColor cfill, CColor cbound)
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
            }
            return ret;
        }
    }
}
