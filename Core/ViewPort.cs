using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TabletC.Core
{
    public class ViewPort
    {
        public ViewPort()
        {
            _viewWidth = _viewHeight = 0;
            _zoom = 1;
            Graphic = null;
        }

        public ViewPort(MessureUnit unit, Double winWidth, Double winHeight)
        {
            Unit = unit;
            _zoom = 1;
            SetWindow(winWidth, winHeight);
            Graphic = null;
        }

        public ViewPort(MessureUnit unit, SizeF size)
        {
            Unit = unit;
            _zoom = 1;
            SetWindow(size);
            Graphic = null;
        }

        public MessureUnit Unit { get; set; }

        public int Width
        {
            get { return _viewWidth; }
        }

        public int Height
        {
            get { return _viewHeight; }
        }

        public Double Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                ReCalculateSize();
            }
        }

        public Graphics Graphic { get; set; }

        public Image Image { get; set; }

        public void SetWindow(SizeF size)
        {
            SetWindow(size.Width, size.Height);
        }

        public void SetWindow(double width, double heigth)
        {
            _realWidth = width;
            _realHeight = heigth;
            ReCalculateSize();
        }

        public int WinToView(double value)
        {
            return Util.WinToView(value*Zoom, Unit);
        }

        public Point WinToView(IVertex vertex)
        {
            return new Point(
                    Util.WinToView(vertex.X * Zoom, Unit),
                    Util.WinToView(vertex.Y * Zoom, Unit)
                );
        }

        public IList<Point> WinToView(IVertexCollection vertices)
        {
            return vertices.Select(WinToView).ToList();
        }

        public Rectangle WinToView(RectangleF rec)
        {
            return new Rectangle(
                Util.WinToView(rec.X*Zoom, Unit),
                Util.WinToView(rec.Y*Zoom, Unit),
                Util.WinToView(rec.Width*Zoom, Unit),
                Util.WinToView(rec.Height*Zoom, Unit)
                );
        }

        public Double ViewToWin(int value)
        {
            return Util.ViewToWin(value, Unit)/Zoom;
        }

        public IVertex ViewToWin(Point point)
        {
            IVertex vertex = new Vertex();
            vertex.X = Util.ViewToWin(point.X, Unit)/Zoom;
            vertex.Y = Util.ViewToWin(point.Y, Unit)/Zoom;
            return vertex;
        }

        private void ReCalculateSize()
        {
            _viewWidth = Util.WinToView(_realWidth * Zoom, Unit);
            _viewHeight = Util.WinToView(_realHeight * Zoom, Unit);
        }

        private double _realWidth;
        private double _realHeight;
        private int _viewWidth;
        private int _viewHeight;
        private double _zoom;
    }
}
