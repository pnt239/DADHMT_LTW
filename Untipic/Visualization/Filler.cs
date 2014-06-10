using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using Untipic.Core;

namespace Visualization
{
    public class Filler
    {
        public void FillByFlood(Bitmap image, Color color, Point pstart)
        {
            var colorFill = color;

            //Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            //BitmapData pixelData = bmp.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

            BitmapData pixelData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

            IntPtr ptr = pixelData.Scan0;
            int bytes = pixelData.Stride * pixelData.Height; //.ImageBuffer.Height;
            var rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Fill at here
            int stride = 4 * image.Width; // linesize

            var replaceColor = GetColorFromArray(ref rgbValues, pstart.X, pstart.X, stride);

            var queue = new Queue<Point>();

            //start the loop
            QueueFloodFill4(ref rgbValues, ref queue, pstart.X, pstart.Y, image.Width, image.Height, stride, colorFill, replaceColor);
            //call next item on queue
            while (queue.Count > 0)
            {
                var pt = queue.Dequeue();
                QueueFloodFill4(ref rgbValues, ref queue, pt.X, pt.Y, image.Width, image.Height, stride, colorFill, replaceColor);
            }
            // End Fill

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            image.UnlockBits(pixelData);
        }

        private void QueueFloodFill4(ref byte[] arr, ref Queue<Point> queue, int x, int y, int w, int h, int stride, Color cfill, Color replace)
        {
            //don't go over the edge
            if (x < 0 || y < 0 || x >= w || y >= h)
                return;

            //calculate pointer offset
            var color = GetColorFromArray(ref arr, x, y, stride);

            //if the pixel is within the color tolerance, fill it and branch out
            if ((color != cfill) && (color == replace))
            {
                SetColorToArray(ref arr, x, y, stride, ref cfill);

                queue.Enqueue(new Point(x + 1, y));
                queue.Enqueue(new Point(x, y + 1));
                queue.Enqueue(new Point(x - 1, y));
                queue.Enqueue(new Point(x, y - 1));
            }
        }

        private void SetColorToArray(ref byte[] arr, int x, int y, int stride, ref Color c)
        {
            arr[y * stride + x * 4] = c.B;
            arr[y * stride + x * 4 + 1] = c.G;
            arr[y * stride + x * 4 + 2] = c.R;
            arr[y * stride + x * 4 + 3] = c.A;
        }

        private Color GetColorFromArray(ref byte[] arr, int x, int y, int linesize)
        {
            return Color.FromArgb(arr[y * linesize + x * 4 + 3], arr[y * linesize + x * 4 + 2], arr[y * linesize + x * 4 + 1], arr[y * linesize + x * 4]);
        }
    }
}
