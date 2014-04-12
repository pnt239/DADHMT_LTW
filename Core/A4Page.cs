using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    public class A4Page : IPage
    {
        private Size _pageSize;
        private PageOrientation _orientation;
        private BindingList<Layer> _layers;

        public A4Page()
        {
            _layers = new BindingList<Layer>();

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

        public BindingList<Layer> Layers
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
