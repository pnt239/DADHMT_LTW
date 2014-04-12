using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class CustomPage : IPage
    {
        public CustomPage()
        {
            _orientation = PageOrientation.Horizontal;
        }

        public CustomPage(Size size)
        {
            _pageSize = size;
            _orientation = PageOrientation.Horizontal;

            _layers = new BindingList<Layer>();

            var newPlayer = new Layer(_pageSize) {Name = "Background Layer"};

            _layers.Add(newPlayer);
        }

        public Size PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
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
            return new CustomPage();
        }

        private Size _pageSize;
        private PageOrientation _orientation;
        private BindingList<Layer> _layers;
    }
}
