using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class CustomPage : IPage
    {
        public CustomPage()
        {
            _shapes = new List<IShape>();
            _orientation = PageOrientation.Horizontal;
        }

        public CustomPage(Size size)
        {
            _pageSize = size;
            _shapes = new List<IShape>();
            _orientation = PageOrientation.Horizontal;
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

        public List<IShape> Shapes
        {
            get { return _shapes; }
            set { _shapes = value; }
        }

        public IPage Clone()
        {
            return new CustomPage();
        }

        private List<IShape> _shapes;
        private Size _pageSize;
        private PageOrientation _orientation;
    }
}
