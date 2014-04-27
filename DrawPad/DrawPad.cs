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
            _currentBursh = new SolidBrush(Color.FromArgb(0, Color.White));
            _currentPage = null;
            CurrentLayer = null;
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

        public Layer CurrentLayer { get; set; }

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

        public Brush CurrentBursh
        {
            get { return _currentBursh; }
            set { _currentBursh = value; }
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
            var lr = new LayerRenderer();

            gp.DrawImageUnscaled(_cache.ImageBuffer, 0, 0);

            foreach (Layer layer in _currentPage.Layers)
            {
                lr.Render(layer);

                if (!_cache.IsExist(layer))
                    gp.DrawImageUnscaled(layer.ImageBuffer, 0, 0);
            }
        }

        private void ctrDrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (DrawMode == DrawMode.Select)
            {
                //
                return;
            }

            if (_drawingbyclick)
                return;

            _lastShape = _currentShape.Clone();
            _lastShape.ShapePen = (Pen)_currentPen.Clone();
            _lastShape.ShapeBrush = (Brush) _currentBursh.Clone();

            if (_lastShape.GetShapeType() == ShapeType.Polygon)
            {
                _drawingbyclick = true;
            }
            
            _lastShape.StartVertex = _lastShape.EndVertex = e.Location;
            

            // Add Shape to Layer
            CurrentLayer = new Layer(_currentPage.PageSize)
            {
                Name =
                    _lastShape.Name + " " + _nameCount[_lastShape.GetShapeType()].ToString(CultureInfo.InvariantCulture)
            };
            _nameCount[_lastShape.GetShapeType()] += 1;

            _currentPage.Layers.Add(CurrentLayer);

            CurrentLayer.Shapes.Add(_lastShape);
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
                if (IsShift && _lastShape.GetShapeType() != ShapeType.RegPolygon)
                {
                    var deltaX = _lastShape.StartVertex.X - p.X;
                    var deltaY = _lastShape.StartVertex.Y - p.Y;
                    var asbX = Math.Abs(deltaX);
                    var asbY = Math.Abs(deltaY);

                    if (Math.Abs(deltaX) > Math.Abs(deltaY))
                    {
                        p.Y = _lastShape.StartVertex.Y;
                        if (
                            !((_lastShape.GetShapeType() == ShapeType.Line) &&
                              ((double) asbY/asbX < Math.Tan(Math.PI/8))))
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
                        if (
                            !((_lastShape.GetShapeType() == ShapeType.Line) &&
                              ((double) asbX/asbY < Math.Tan(Math.PI/8))))
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
                CurrentLayer.IsRendered = false;
                ctrDrawArea.Invalidate();
            }
            else if (_drawingbyclick)
            {
                _lastShape.EndVertex = e.Location;
                CurrentLayer.IsRendered = false;
                ctrDrawArea.Invalidate();
            }
            
        }

        private void ctrDrawArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (_drawingbyclick)
            {
                if (_lastShape.Vertices.Count > 0 && Math.Abs(_lastShape.Vertices[0].X - e.Location.X) < 5 &&
                        Math.Abs(_lastShape.Vertices[0].Y - e.Location.Y) < 5)
                {
                    _lastShape.EndVertex = _lastShape.Vertices[0];
                    _drawingbyclick = false;
                    CurrentLayer.IsRendered = false;
                    ctrDrawArea.Invalidate();
                }
                else
                {
                    _lastShape.Vertices.Add(e.Location);
                    CurrentLayer.IsRendered = false;
                    return;
                }
                
            }

            // Release shape and push into cache
            _cache.AddLayer(CurrentLayer);

            // Update point
            _lastShape.FinishEdition();
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
        private IShape _currentShape;
        private Pen _currentPen;
        private Brush _currentBursh;
        private IShape _lastShape;
        private DrawMode _drawMode;
        private bool _isShift;
        private ImageCache _cache;

        private bool _drawingbyclick;


        private readonly Dictionary<ShapeType, int> _nameCount;
    }
}
