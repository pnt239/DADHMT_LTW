using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using TabletC.Core;

namespace TabletC.DrawPad
{

    class ShapeFiller
    {
        private Queue<Point> _queue = new Queue<Point>();  //Queue chứa điểm cần xét

        private Color _BorderColor;   //Màu đường biên
        private Color _FillColor;  //Màu cần tô
        private Point _point;  //Điểm click

        public void FloodFill(Layer layer, IShape shape, Point? pstart)
        {
            if (pstart == null)
                pstart = GetInnerPoint(shape);
            
            Color _colorBg;
            BitmapData pixelData = layer.ImageBuffer.LockBits(new Rectangle(0, 0, layer.ImageBuffer.Width, layer.ImageBuffer.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                int* pData = (int*)pixelData.Scan0.ToPointer();
                pData += pstart.Value.X * layer.ImageBuffer.Width + pstart.Value.Y;
                _colorBg = Color.FromArgb(*pData);

                if (_colorBg == _BorderColor)
                    return;
                else
                    _queue.Enqueue(_point);
                while (_queue.Count() != 0)
                {
                    Point _ptmp = _queue.Dequeue();
                    pData += _ptmp.X * layer.ImageBuffer.Width + _ptmp.Y;
                    _colorBg = Color.FromArgb(*pData);
                    if (_colorBg != _BorderColor)
                    {
                        layer.ImageBuffer.SetPixel(_ptmp.X, _ptmp.Y, _FillColor);
                        GetNeighbour(_ptmp);
                    }
                }
            }
            layer.ImageBuffer.UnlockBits(pixelData);
           
            //Old Code
            //_colorBg = layer.ImageBuffer.GetPixel(pstart.Value.X, pstart.Value.Y);
 
            //if (_colorBg == _BorderColor)
            //    return;
            //else
            //    _queue.Enqueue(_point);
            //while(_queue.Count() != 0)
            //{
            //    Point _ptmp = _queue.Dequeue();
            //    _colorBg = layer.ImageBuffer.GetPixel(_ptmp.X, _ptmp.Y);
            //    if(_colorBg != _BorderColor)
            //    {
            //        layer.ImageBuffer.SetPixel(_ptmp.X, _ptmp.Y, _FillColor);
            //        GetNeighbour(_ptmp);
            //    }
            //}
        }

        private void GetNeighbour(Point _ptmp)  //Ham lay lien thong 4 dua cac diem do vao queue
        {
            Point tmp = _ptmp;
            tmp.X -= 1;
            _queue.Enqueue(tmp);

            tmp.X += 2;
            _queue.Enqueue(tmp);
            tmp.X -= 1;

            tmp.Y += 1;
            _queue.Enqueue(tmp);

            tmp.Y -= 2;
            _queue.Enqueue(tmp);
        }

        private Point GetInnerPoint(IShape shape)
        {
            return new Point();
        }
    }
}
