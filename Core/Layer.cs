using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Layer
    {
        private readonly List<IShape> _shapes;
        private SizeF _layerSize;
        private bool _isRendered;
        private string _name;
        private Bitmap _thumb;

        public Layer(SizeF size)
        {
            _layerSize = size;
            _shapes = new List<IShape>();

            _isRendered = false;
            _name = "";
        }

        public SizeF LayerSize
        {
            get { return _layerSize; }
            set { _layerSize = value; }
        }

        public bool IsRendered
        {
            get { return _isRendered; }
            set { _isRendered = value; }
        }

        public List<IShape> Shapes
        {
            get { return _shapes; }
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
    }
}
