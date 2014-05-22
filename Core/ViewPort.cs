using System;
using System.Drawing;

namespace TabletC.Core
{
    public class ViewPort
    {
        public ViewPort()
        {
            Unit = MessureUnit.Milimeters;
            _viewWidth = _viewHeight = 0;
            _iViewWidth = _iViewHeight = 0;
            _scale = 1;
            _zoom = 1;
        }

        public ViewPort(MessureUnit unit, Double winWidth, Double winHeight)
        {
            Unit = unit;
            _zoom = 1;
            SetWindow(winWidth, winHeight);
        }

        public ViewPort(MessureUnit unit, SizeF size)
        {
            Unit = unit;
            _zoom = 1;
            SetWindow(size);
        }

        public MessureUnit Unit { get; set; }

        public double Width
        {
            get { return _viewWidth; }
        }

        public double Height
        {
            get { return _viewHeight; }
        }

        public int IWidth
        {
            get { return _iViewWidth; }
        }

        public int IHeight
        {
            get { return _iViewHeight; }
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

        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

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

        /* Win To View */
        public Double WinToView(double value)
        {
            return Util.WinToView(value*Zoom, Unit);
        }

        public IVertex WinToView(IVertex vertex)
        {
            return new Vertex(WinToView(vertex.X), WinToView(vertex.Y));
        }

        public IVertexCollection WinToView(IVertexCollection vertices)
        {
            IVertexCollection ret = new VertexCollection();
            foreach (Vertex vertex in vertices)
            {
                ret.Add(WinToView(vertex));
            }
            return ret;
        }

        public RectangleF WinToView(RectangleF rec)
        {
            return new RectangleF(
                (float)WinToView(rec.X),
                (float)WinToView(rec.Y),
                (float)WinToView(rec.Width),
                (float)WinToView(rec.Height)
                );
        }

        public IShape WinToView(IShape shape)
        {
            IShape newShape = shape.Clone();
            newShape.StartVertex = WinToView(shape.StartVertex);
            newShape.EndVertex = WinToView(shape.EndVertex);
            newShape.Vertices = WinToView(shape.Vertices);
            return newShape;
        }

        /* View To Win */
        public Double ViewToWin(double value)
        {
            return Util.ViewToWin(value, Unit)/Zoom;
        }

        public IVertex ViewToWin(IVertex vertex)
        {
            return new Vertex(ViewToWin(vertex.X), ViewToWin(vertex.Y));
        }

        public IVertexCollection ViewToWin(IVertexCollection vertices)
        {
            IVertexCollection ret = new VertexCollection();
            foreach (Vertex vertex in vertices)
            {
                ret.Add(ViewToWin(vertex));
            }
            return ret;
        }

        public IShape ViewToWin(IShape shape)
        {
            IShape newShape = shape.Clone();
            newShape.StartVertex = ViewToWin(shape.StartVertex);
            newShape.EndVertex = ViewToWin(shape.EndVertex);
            newShape.Vertices = ViewToWin(shape.Vertices);
            return newShape;
        }


        private void ReCalculateSize()
        {
            _iViewWidth = Util.DtoI(_viewWidth = WinToView(_realWidth));
            _iViewHeight = Util.DtoI(_viewHeight = WinToView(_realHeight));
        }

        private double _realWidth;
        private double _realHeight;
        private double _viewWidth;
        private double _viewHeight;
        private int _iViewWidth;
        private int _iViewHeight;
        private double _scale;
        private double _zoom;
    }
}
