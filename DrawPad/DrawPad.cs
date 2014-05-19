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

    public partial class DrawPad: UserControl
    {
        public DrawPad()
        {
            InitializeComponent();

            _viewPort = new ViewPort();
            _transformBox = new TransformBox();
            _currentPen = new Pen(Color.Black, 1.0F);
            _currentBrush = new SolidBrush(Color.White);
            _currentPage = null;
            _currentLayer = null;
            _cache = null;
            _drawingbyclick = false;

            IsShift = false;
            _isSelected = false;
            _canMove = false;
            _canResize = false;
            _isResized = false;

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

        public event EventHandler ShapeCreated;

        protected virtual void OnShapeCreated()
        {
            EventHandler handler = ShapeCreated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public IPage CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;

                CurrentLayer = _currentPage.Layers[0];

                _viewPort = new ViewPort(_currentPage.Units, _currentPage.PageSize);

                _transformBox.ViewPort = _viewPort;
                _cache = new ImageCache(ref _viewPort);

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

        public SelectMode SelectMode
        {
            get { return _transformBox.SelectMode; }
            set { _transformBox.SelectMode = value; }
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

        public ViewPort ViewPort
        {
            get { return _viewPort; }
        }

        public event MouseEventHandler MouseChanged;

        private void ChangeLayout()
        {
            if (_currentPage == null) return;

            tlyMain.RowStyles[1].Height = ViewPort.Height;
            tlyMain.ColumnStyles[1].Width = ViewPort.Width;
            AutoScrollMinSize = new Size(ViewPort.Width + 30, ViewPort.Height + 30);
        }

        private void ctrDrawArea_Paint(object sender, PaintEventArgs e)
        {
            if (_currentPage == null)
                return;

            ViewPort.Graphic = e.Graphics;
            _cache.Render();

            _transformBox.Draw(e.Graphics);
            //if (_isSelected)
            //    _resizeBox.Draw(e.Graphics);
        }

        private void ctrDrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectMode == SelectMode.Selection)
            {
                //// There isn't any selected shape
                //if (!_isSelected) return;

                //if (_canResize)
                //{
                //    // Calculate some default distance before resize
                //    _resizeBox.SetDistanceSquare(e.X, e.Y);
                //    _isResized = true;
                //}
                //else
                //{
                //    // Calculate some default distance before move
                //    _resizeBox.SetOrginalPoint(new Point(e.X, e.Y));
                //    _canMove = true;
                //}
                //return;
            }

            // Continue drawing shape
            if (_drawingbyclick)
                return;

            // Prototype method
            _transformBox.CurentShape = _currentShape = _currentShape.Clone();
            _currentShape.ShapePen = (Pen)_currentPen.Clone();
            _currentShape.ShapeBrush = (Brush) _currentBrush.Clone();

            if (_currentShape.GetShapeType() == ShapeType.Polygon)
            {
                // Draw by click
                _drawingbyclick = true;
            }
            // Assign start and end point
            _currentShape.StartVertex = _currentShape.EndVertex = new Vertex(e.Location);

            _transformBox.Recalculate();
            _transformBox.ShowControlPoint = false;
            
        }

        private void ctrDrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            // Fire Event
            MouseEventHandler oMouseChanged = this.MouseChanged;
            if (oMouseChanged != null)
                oMouseChanged(this, new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta));

            if (SelectMode == SelectMode.Selection)
            {
                //if (_isSelected && _isResized)
                //{
                //    _resizeBox.MoveSquare(e.X, e.Y); // Resize object
                //    _currentLayer.IsRendered = false;
                //    ctrDrawArea.Invalidate();
                //}
                //else if (_isSelected && _canMove)
                //{
                //    _resizeBox.MoveShape(new Point(e.X, e.Y)); // Move object
                //    _currentLayer.IsRendered = false;
                //    ctrDrawArea.Invalidate();
                //}
                //else if (_isSelected)
                //{
                //    // Select object
                //    int cur;
                //    if ((cur = _resizeBox.HitTest(e.X, e.Y)) != 0)
                //    {
                //        // One of the control points is hovered
                //        // Set cursor
                //        switch (cur)
                //        {
                //            case 1:
                //                ctrDrawArea.Cursor = Cursors.SizeNWSE;
                //                break;
                //            case 2:
                //                ctrDrawArea.Cursor = Cursors.SizeNESW;
                //                break;
                //            default:
                //                ctrDrawArea.Cursor = Cursors.SizeAll;
                //                break;
                //        }
                //        // Set flag can resize
                //        _canResize = true;
                //    }
                //    else
                //    {
                //        // No control point is hovered
                //        ctrDrawArea.Cursor = Cursors.Default;
                //        _canResize = false;
                //    }
                //}
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (SelectMode == SelectMode.Selection)
                {
                    //// In select mode
                    //if (_isResized)
                    //{
                    //    // Leave resize mode
                    //    _isResized = false;
                    //}
                    //else
                    //{
                    //    // Leave move mode or deselect object
                    //    _canMove = _isSelected = false;
                    //    foreach (var shape in _currentLayer.Shapes.Where(shape => shape.HitTest(new Point(e.X, e.Y))))
                    //    {
                    //        _currentShape = shape;
                    //        // Shape is behind mouse
                    //        _isSelected = true;
                    //        // Load shape into resizebox
                    //        _resizeBox.LoadShape(shape);
                    //        break;
                    //    }
                    //    // No object is selected
                    //    if (!_isSelected)
                    //        _resizeBox.LoadShape(null);
                    //}
                    return;
                }
                //var p = new Point(e.Location.X, e.Location.Y);
                var p = new Vertex(e.Location);
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

                _transformBox.Recalculate();
                ctrDrawArea.Invalidate();
            }
            else if (_drawingbyclick)
            {
                _currentShape.EndVertex = new Vertex(e.Location);
                CurrentLayer.IsRendered = false;

                _transformBox.Recalculate();
                ctrDrawArea.Invalidate();
            }
            
        }

        private void ctrDrawArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectMode == SelectMode.Selection)
            {
                //// In select mode
                //if (_isResized)
                //{
                //    // Leave resize mode
                //    _isResized = false;
                //}
                //else
                //{
                //    // Leave move mode or deselect object
                //    _canMove = _isSelected = false;
                //    foreach (var layer in _currentPage.Layers)
                //        foreach (var shape in layer.Shapes.Where(shape => shape.HitTest(new Point(e.X, e.Y))))
                //        {
                //            _currentShape = shape;
                //            // Shape is behind mouse
                //            _isSelected = true;
                //            // Load shape into resizebox
                //            _resizeBox.LoadShape(shape);

                //            // Select Layer In listbox
                            

                //            ctrDrawArea.Invalidate();
                //            break;
                //        }
                //    // No object is selected
                //    if (!_isSelected)
                //    {
                //        _resizeBox.LoadShape(null);
                //        _currentLayer.IsRendered = false;
                //        ctrDrawArea.Invalidate();
                //    }
                //}
                return;
            }

            if (_drawingbyclick)
            {
                // Draw by click
                // Add new point
                if (_currentShape.Vertices.Count > 0 && Math.Abs((int)_currentShape.Vertices[0].X - e.Location.X) < 5 &&
                        Math.Abs((int)_currentShape.Vertices[0].Y - e.Location.Y) < 5)
                {
                    _currentShape.EndVertex = new Vertex(-1, -1);
                    _drawingbyclick = false;
                    CurrentLayer.IsRendered = false;
                    ctrDrawArea.Invalidate();
                }
                else
                {
                    _currentShape.Vertices.Add(new Vertex(e.Location));
                    CurrentLayer.IsRendered = false;
                    //return;
                }
                
            }

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

            // Update point
            OnShapeCreated();
            _currentShape.ReCalculateVertices();

            _transformBox.ShowControlPoint = true;
            ctrDrawArea.Invalidate();
        }

        private void ctrDrawArea_Click(object sender, EventArgs e)
        {
            //
        }

        private void ctrDrawArea_KeyDown(object sender, KeyEventArgs e)
        {
            _isShift = e.Shift;
        }

        private ViewPort _viewPort;
        private readonly TransformBox _transformBox;
        private IPage _currentPage;
        private Layer _currentLayer;
        private IShape _currentShape;
        private Pen _currentPen;
        private Brush _currentBrush;
        private bool _isShift;
        private ImageCache _cache;

        // ResizeBox flag
        private bool _isSelected;
        private bool _canMove;
        private bool _canResize;
        private bool _isResized;

        private bool _drawingbyclick;


        private readonly Dictionary<ShapeType, int> _nameCount;
    }
}
