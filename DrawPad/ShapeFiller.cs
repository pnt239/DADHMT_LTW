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
            _colorBg = layer.ImageBuffer.GetPixel(pstart.Value.X, pstart.Value.X);
            if (_colorBg == _BorderColor)
                return;
            else
                _queue.Enqueue(_point);
            while(_queue.Count() != 0)
            {
                Point _ptmp = _queue.Dequeue();
                _colorBg = layer.ImageBuffer.GetPixel(_ptmp.X, _ptmp.Y);
                if(_colorBg != _BorderColor)
                {
                    layer.ImageBuffer.SetPixel(_ptmp.X, _ptmp.Y, _FillColor);
                    GetNeighbour(_ptmp);
                }
            }
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
