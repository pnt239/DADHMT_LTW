using System;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    [Serializable]
    public class CustomPage : IPage
    {
        public CustomPage()
        {
            Units = MessureUnit.Milimeters;
            _orientation = PageOrientation.Horizontal;
        }

        public CustomPage(SizeF size)
        {
            Units = MessureUnit.Milimeters;
            _pageSize = size;
            _orientation = PageOrientation.Horizontal;

            _layers = new BindingList<Layer>
            {
                new Layer(_pageSize) {Name = "Layer 1"}
            };
        }

        public SizeF PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public MessureUnit Units
        {
            get { return _units; }
            set { _units = value; }
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

        public PageType GetPageType()
        {
            return PageType.Custom;
        }

        public IPage Clone()
        {
            return new CustomPage();
        }

        private SizeF _pageSize;
        private PageOrientation _orientation;
        private BindingList<Layer> _layers;
        private MessureUnit _units;
        private double _scale;
    }
}
