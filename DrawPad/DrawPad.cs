using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public enum DrawMode
    {
        Select = 1, Draw, Paint
    }

    public partial class DrawPad: UserControl
    {
        public DrawPad()
        {
            InitializeComponent();

            _currentPen = new Pen(Color.Black, 1.0F);
            _currentBrush = new SolidBrush(Color.White);
            _currentPage = null;
            _currentLayer = null;
            _cache = null;
            _drawingbyclick = false;

            DrawMode = DrawMode.Select;
            IsShift = false;

            _nameCount = new Dictionary<ShapeType, int>
            {
                {ShapeType.Line, 1},
                {ShapeType.Rectangle, 1},
                {ShapeType.Ellipse, 1},
                {ShapeType.Triangle, 1},
                {ShapeType.RegPolygon, 1},
                {ShapeType.Polygon, 1}
            };
        }

        public IPage CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                CurrentLayer = _currentPage.Layers[0];
                _cache = new ImageCache(_currentPage.PageSize);
                ChangeLayout();
            }
        }

        public Layer CurrentLayer
        {
            get { return _currentLayer; }
            set { _currentLayer = value; }
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

        public Brush CurrentBrush
        {
            get { return _currentBrush; }
            set { _currentBrush = value; }
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

            _cache.Render(e.Graphics);
        }

        private void ctrDrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (DrawMode == DrawMode.Select)
            {
                //
                return;
            }

            // Continue drawing shape
            if (_drawingbyclick)
                return;

            // Prototype method
            _currentShape.ShapePen = (Pen)_currentPen.Clone();
            _currentShape.ShapeBrush = (Brush) _currentBrush.Clone();

            if (_currentShape.GetShapeType() == ShapeType.Polygon)
            {
                // Draw by click
                _drawingbyclick = true;
            }
            // Assign start and end point
            _currentShape.StartVertex = _currentShape.EndVertex = e.Location;
            

            // Create new layer
            CurrentLayer = new Layer(_currentPage.PageSize)
            {
                Name =
                    _currentShape.Name + " " + _nameCount[_currentShape.GetShapeType()].ToString(CultureInfo.InvariantCulture)
            };
            // Assign NO. for new layer, uing for display on Bindlist
            _nameCount[_currentShape.GetShapeType()] += 1;
            // Add new layer into current page
            _currentPage.Layers.Add(CurrentLayer);
            // Add new shape into new layer
            CurrentLayer.Shapes.Add(_currentShape);
            // Push layer into cache
            _cache.PushLayer(ref _currentLayer);
        }

        private void ctrDrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (DrawMode == DrawMode.Select)
            {
                //
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (_drawMode == DrawMode.Select)
                {
                    //
                    return;
                }
                var p = new Point(e.Location.X, e.Location.Y);
                if (IsShift && _currentShape.GetShapeType() != ShapeType.RegPolygon)
                {
                    // Snap point
                    var deltaX = _currentShape.StartVertex.X - p.X;
                    var deltaY = _currentShape.StartVertex.Y - p.Y;
                    var asbX = Math.Abs(deltaX);
                    var asbY = Math.Abs(deltaY);

                    if (Math.Abs(deltaX) > Math.Abs(deltaY))
                    {
                        // Tinh y theo x
                        p.Y = _currentShape.StartVertex.Y; // Default is horital
                        if (
                            !((_currentShape.GetShapeType() == ShapeType.Line) &&
                              ((double) asbY/asbX < Math.Tan(Math.PI/8))))
                        {
                            // if deltaY = 0 then line is horital
                            // else if deltaY != then
                            //      if shape is triangle then
                            //          heigth of triangle is y*sin(60)
                            //      else
                            //          line is crisscross
                            p.Y -= (deltaY == 0
                                ? 0
                                : (int)
                                    (asbX*(_currentShape.GetShapeType() == ShapeType.Triangle ? Math.Sin(Math.PI/3) : 1))*
                                  deltaY/asbY);
                        }
                    }
                    else
                    {
                        // Tinh x theo y
                        p.X = _currentShape.StartVertex.X; // Default is vertical
                        if (
                            !((_currentShape.GetShapeType() == ShapeType.Line) &&
                              ((double) asbX/asbY < Math.Tan(Math.PI/8))))
                        {
                            p.X -= (deltaX == 0
                                ? 0
                                : (int)
                                    (asbY/(_currentShape.GetShapeType() == ShapeType.Triangle ? Math.Sin(Math.PI/3) : 1))*
                                  deltaX/asbX);
                        }
                    }

                }

                _currentShape.EndVertex = p;
                CurrentLayer.IsRendered = false;
                ctrDrawArea.Invalidate();
            }
            else if (_drawingbyclick)
            {
                _currentShape.EndVertex = e.Location;
                CurrentLayer.IsRendered = false;
                ctrDrawArea.Invalidate();
            }
            
        }

        private void ctrDrawArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (DrawMode == DrawMode.Select)
            {
                //
                return;
            }

            if (_drawingbyclick)
            {
                // Draw by click
                // Add new point
                if (_currentShape.Vertices.Count > 0 && Math.Abs(_currentShape.Vertices[0].X - e.Location.X) < 5 &&
                        Math.Abs(_currentShape.Vertices[0].Y - e.Location.Y) < 5)
                {
                    _currentShape.EndVertex = new Point(-1, -1);
                    _drawingbyclick = false;
                    CurrentLayer.IsRendered = false;
                    ctrDrawArea.Invalidate();
                }
                else
                {
                    _currentShape.Vertices.Add(e.Location);
                    CurrentLayer.IsRendered = false;
                    //return;
                }
                
            }

            // Update point
            _currentShape.FinishEdition();
        }

        private void ctrDrawArea_Click(object sender, EventArgs e)
        {
            //
        }

        private void ctrDrawArea_KeyDown(object sender, KeyEventArgs e)
        {
            _isShift = e.Shift;
        }

        private IPage _currentPage;
        private Layer _currentLayer;
        private IShape _currentShape;
        private Pen _currentPen;
        private Brush _currentBrush;
        private DrawMode _drawMode;
        private bool _isShift;
        private ImageCache _cache;

        private bool _drawingbyclick;


        private readonly Dictionary<ShapeType, int> _nameCount;
    }
}
