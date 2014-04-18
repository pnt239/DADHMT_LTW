﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
namespace TabletC.Core
{

    class ColorFill
    {
        Queue<Point> _queue = new Queue<Point>();  //Queue chứa điểm cần xét


        Color _BorderColor;   //Màu đường biên
        Color _FillColor;  //Màu cần tô
        Point _point;  //Điểm click
        Graphics _graphic;
        Bitmap _bitmap;

        void GetNeighbour(Point _ptmp)  //Ham lay lien thong 4 dua cac diem do vao queue
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
        void FloodFill()  //Thuật toán tô màu loang khử đệ quy, tìm wikipedia =v= (Nhớ xóa dòng này sau khi đọc)
        {
            Color _colortmp;
            _colortmp = _bitmap.GetPixel(_point.X, _point.Y);
            if (_colortmp == _BorderColor)
                return;
            else
                _queue.Enqueue(_point);
            while(_queue.Count() != 0)
            {
                Point _ptmp = _queue.Dequeue();
                _colortmp = _bitmap.GetPixel(_ptmp.X, _ptmp.Y);
                if(_colortmp != _BorderColor)
                {
                    _bitmap.SetPixel(_ptmp.X, _ptmp.Y, _FillColor);
                    GetNeighbour(_ptmp);
                }
            }
        }
    }
}