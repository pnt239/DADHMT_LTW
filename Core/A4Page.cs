using System.Collections.Generic;
using System.Drawing;

namespace TabletC.Core
{
    public class A4Page : IPage
    {
        private Size _pageSize;
        private List<IShape> _shapes;
        private PageOrientation _orientation;
        private List<Layer> _layers;

        public A4Page()
        {
            _shapes = new List<IShape>();
            _layers = new List<Layer>();

            _pageSize = new Size(21, 29);
            _orientation = PageOrientation.Horizontal;
        }

        public Size PageSize
        {
            get { return _pageSize; }
            set {}
        }

        public PageOrientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; }
        }

        public List<IShape> Shapes
        {
            get { return _shapes; }
            set { _shapes = value; }
        }

        public List<Layer> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        public IPage Clone()
        {
            return new A4Page();
        }
    }
}
