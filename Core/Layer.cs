using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Layer
    {
        private Bitmap _imageBuffer;
        private Graphics _graphicsBuffer;
        private List<IShape> _shapes;
        private Size _layerSize;
        private bool _isRendered;
        private string _name;
        private Bitmap _thumb;

        public Layer(Size size)
        {
            _layerSize = size;
            _isRendered = false;
            _name = "";

            _imageBuffer = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _graphicsBuffer = Graphics.FromImage(_imageBuffer);
            Shapes = new List<IShape>();
        }

        public Size LayerSize
        {
            get { return _layerSize; }
            set { _layerSize = value; }
        }

        public Bitmap ImageBuffer
        {
            get { return _imageBuffer; }
            set { _imageBuffer = value; }
        }

        public bool IsRendered
        {
            get { return _isRendered; }
            set { _isRendered = value; }
        }

        public List<IShape> Shapes
        {
            get { return _shapes; }
            set { _shapes = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Bitmap Thumb
        {
            get { return _thumb; }
            set { _thumb = value; }
        }

        public Graphics GraphicsBuffer
        {
            get { return _graphicsBuffer; }
        }
    }
}
