using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    public class A4Page : IPage
    {
        private readonly SizeF _pageSize;
        private PageOrientation _orientation;
        private BindingList<Layer> _layers;
        private double _scale;

        public A4Page()
        {
            _layers = new BindingList<Layer>();

            Units = MessureUnit.Milimeters;
            _pageSize = new SizeF(210, 297);
            _orientation = PageOrientation.Horizontal;

            _layers = new BindingList<Layer>
            {
                //new Layer(_pageSize) {Name = "Background"}
            };
        }

        public SizeF PageSize
        {
            get { return _pageSize; }
            set {}
        }

        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public MessureUnit Units { get; set; }

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

        public PageType GetPageType()
        {
            return PageType.A4;
        }

        public IPage Clone()
        {
            return new A4Page();
        }
    }
}
