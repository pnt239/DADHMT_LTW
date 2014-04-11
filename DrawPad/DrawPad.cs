using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public enum DrawMode
    {
        Select = 1, Draw
    }

    public partial class DrawPad: UserControl
    {
        public DrawPad()
        {
            InitializeComponent();

            _shapeDrawer = new ShapeDrawer();
            _currentPen = new Pen(Color.Black, 1.0F);
            _currentPage = null;
            DrawMode = DrawMode.Select;
            IsShift = false;
        }

        public IPage CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                ChangeLayout();
            }
        }

        public Pen CurrentPen
        {
            get { return _currentPen; }
            set { _currentPen = value; }
        }

        public IShape CurrentShape
        {
            get { return _currentShape; }
            set { _currentShape = value; }
        }

        public DrawMode DrawMode
        {
            get { return _drawMode; }
            set { _drawMode = value; }
        }

        public bool IsShift
        {
            get { return _isShift; }
            set { _isShift = value; }
        }

        private void ChangeLayout()
        {
            if (_currentPage == null) return;

            tlyMain.RowStyles[1].Height = _currentPage.PageSize.Height;
            tlyMain.ColumnStyles[1].Width = _currentPage.PageSize.Width;
            AutoScrollMinSize = new Size(_currentPage.PageSize.Width + 30, _currentPage.PageSize.Height + 30);
        }

        private void ctrDrawArea_Paint(object sender, PaintEventArgs e)
        {
            if (_currentPage == null)
                return;

            Graphics gp = e.Graphics;

            foreach (IShape shape in _currentPage.Shapes)
            {
                _shapeDrawer.Draw(gp, shape);
            }
        }

        private void ctrDrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (DrawMode == DrawMode.Select)
            {
                //
                return;
            }
            _lastShape = _currentShape.Clone();
            _lastShape.StartVertex = _lastShape.EndVertex = e.Location;
            _lastShape.ShapePen = (Pen)_currentPen.Clone();
            _currentPage.Shapes.Add(_lastShape);
        }

        private void ctrDrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_drawMode == DrawMode.Select)
                {
                    //
                    return;
                }
                var p = new Point(e.Location.X, e.Location.Y);
                if (IsShift)
                {
                    var deltaX = _lastShape.StartVertex.X - p.X;
                    var deltaY = _lastShape.StartVertex.Y - p.Y;
                    var asbX = Math.Abs(deltaX);
                    var asbY = Math.Abs(deltaY);

                    if (Math.Abs(deltaX) > Math.Abs(deltaY))
                    {
                        p.Y = _lastShape.StartVertex.Y;
                        if (!((_lastShape.GetShapeType() == ShapeType.Line) && ((double)asbY/asbX < Math.Tan(Math.PI/8))))
                        {
                            p.Y -= (deltaY == 0
                                ? 0
                                : (int)
                                    (asbX*(_lastShape.GetShapeType() == ShapeType.Triangle ? Math.Sin(Math.PI/3) : 1))*
                                  deltaY/asbY);
                        }
                    }
                    else
                    {
                        p.X = _lastShape.StartVertex.X;
                        if (!((_lastShape.GetShapeType() == ShapeType.Line) && ((double)asbX / asbY < Math.Tan(Math.PI / 8))))
                        {
                            p.X -= (deltaX == 0
                                ? 0
                                : (int)
                                    (asbY/(_lastShape.GetShapeType() == ShapeType.Triangle ? Math.Sin(Math.PI/3) : 1))*
                                  deltaX/asbX);
                        }
                    }
                    
                }

                _lastShape.EndVertex = p;
                ctrDrawArea.Invalidate();
            }
        }

        private void ctrDrawArea_KeyDown(object sender, KeyEventArgs e)
        {
            _isShift = e.Shift;
        }

        private IPage _currentPage;
        private IShape _currentShape;
        private Pen _currentPen;
        private IShape _lastShape;
        private readonly ShapeDrawer _shapeDrawer;
        private DrawMode _drawMode;
        private bool _isShift;
    }
}
