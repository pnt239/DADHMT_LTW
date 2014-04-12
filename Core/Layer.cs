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
        private List<IShape> _shapes;
        private Size _layerSize;

        public Layer(Size size)
        {
            _layerSize = size;

            ImageBuffer = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _shapes = new List<IShape>();
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
    }
}
