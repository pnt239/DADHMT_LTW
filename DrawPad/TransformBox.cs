using System;
using System.Collections.Generic;
using System.Drawing;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public enum SelectMode
    {
        None, Draw, Selection, DirectSelection
    }
    public class TransformBox
    {
        public TransformBox()
        {
            _shapeDraw = new ShapeDrawer();

            // 9 is center point
            _transformPoint = new Point[CountTransPoint];
            _controlPoint = new List<Point>();

            _selectMode = SelectMode.None;
            ShowControlPoint = false;
            _penBorder = new Pen(Color.FromArgb(0xff, 0x4f, 0x80, 0xff));
            _penTransPoint = new Pen(Color.FromArgb(0xff, 0x4f, 0x80, 0xff));
            _penCtrlPoint = new Pen(Color.FromArgb(0xff, 0x4f, 0x80, 0xff));
            _brushControlPoint = new SolidBrush(Color.FromArgb(0xff, 0x4f, 0x80, 0xff));
        }

        public ViewPort ViewPort
        {
            get { return _viewPort; }
            set { _viewPort = value; }
        }

        public IShape CurentShape
        {
            get { return _curentShape; }
            set
            {
                _curentShape = value;
                _reviewShape = _curentShape.Clone();
                _reviewShape.ShapePen = _penBorder;
                Recalculate();
            }
        }

        public IShape ReviewShape
        {
            get { return _reviewShape; }
        }

        public SelectMode SelectMode
        {
            get { return _selectMode; }
            set { _selectMode = value; }
        }

        public bool ShowControlPoint
        {
            get { return _showControlPoint; }
            set { _showControlPoint = value; }
        }

        public void Draw(Graphics graphs)
        {
            if (SelectMode == SelectMode.None || _curentShape == null)
                return;

            ReviewShape.ShapePen = _penBorder;
            _shapeDraw.Draw(ReviewShape, graphs);

            if (SelectMode == SelectMode.Selection)
            {
                graphs.DrawRectangle(_penBorder, _recTransBorder);
                // Draw 8 transform point
                for (int i = 0; i < CountTransPoint; i++)
                    DrawPoint(_transformPoint[i], false, graphs);
            }
            else if (SelectMode == SelectMode.DirectSelection || ShowControlPoint)
            {
                foreach (IVertex vertex in _curentShape.Vertices)
                    DrawPoint(vertex.ToPoint(), true, graphs);
            }
        }

        public void DrawPoint(Point point, bool fill, Graphics graphs)
        {
            Rectangle rec = new Rectangle(point.X - _pointWith/2, point.Y - _pointHeight/2, _pointWith, _pointHeight);
            graphs.DrawRectangle(_penTransPoint, rec);

            if (fill)
                graphs.FillRectangle(_brushControlPoint, rec);
        }

        public void Recalculate()
        {
            if (_reviewShape.Vertices.Count == 0)
                return;
            RectangleF rec = Util.CreateShapeBound(_reviewShape);
            _recTransBorder = Util.RecFToRec(rec);

            RecalculateTransPoint();
        }

        public void RecalculateTransPoint()
        {
            float segmentX = (float) _recTransBorder.Width/2;
            float segmentY = (float) _recTransBorder.Height/2;
            int[] dx = {0, 1, 2, 2, 2, 1, 0, 0, 1};
            int[] dy = {0, 0, 0, 1, 2, 2, 2, 1, 1};
            for (int i = 0; i < CountTransPoint; i++)
            {
                _transformPoint[i] = new Point(_recTransBorder.X + (int) (dx[i]*segmentX),
                    _recTransBorder.Y + (int) (dy[i]*segmentY));
            }
        }

        public void FinishCreating()
        {
            _curentShape.Vertices = _reviewShape.Vertices;
            _curentShape.StartVertex = _reviewShape.StartVertex;
            _curentShape.EndVertex = _reviewShape.EndVertex;
        }

        private readonly ShapeDrawer _shapeDraw;
        private ViewPort _viewPort;

        private Point[] _transformPoint;
        private List<Point> _controlPoint;

        private SelectMode _selectMode;
        private bool _showControlPoint;
        private Pen _penBorder;
        private Pen _penTransPoint;
        private Pen _penCtrlPoint;
        private Brush _brushControlPoint;
        private Pen _penOutLine;

        private Rectangle _recTransBorder;

        private int _pointWith = 4;
        private int _pointHeight = 4;
        private const int CountTransPoint = 9;

        private IShape _curentShape;
        private IShape _reviewShape;
        /*
        public ResizeBox()
        {
            _borderPen = new Pen(Color.FromArgb(64, 64, 64));
            _borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            _shape = null;
            _rec = new Rectangle();
            _havingBorder = true;
            _controlPoints = new List<Point>();
            _dPoint = new List<Size>();
            _anchorPoint = new Point();
            _hitSize = new Size();
            _hitPosition = -1;
        }

        public bool Enable { get; set; }

        // Ve nhung thu can thiet
        public void Draw(Graphics graphic)
        {
            _graphic = graphic;

            if (_shape == null)
                return;

            if (_havingBorder)
                DrawBorder();

            foreach (var point in _controlPoints)
                DrawSmallQuare(point.X, point.Y);
        }

        // Nap shape can edit vao class
        public void LoadShape(IShape shape)
        {
            _shape = shape;
            if (_shape == null)
                return;

            _rec = Util.CreateBorder(shape);
            _controlPoints.Clear();
            _dPoint.Clear();
            switch (_shape.GetShapeType())
            {
                case ShapeType.Triangle:
                case ShapeType.Rectangle:
                case ShapeType.Ellipse:
                {
                    _controlPoints.Add(new Point(_rec.X, _rec.Y)); 
                    _controlPoints.Add(new Point(_rec.X + _rec.Width, _rec.Y)); 
                    _controlPoints.Add(new Point(_rec.X + _rec.Width, _rec.Y + _rec.Height)); 
                    _controlPoints.Add(new Point(_rec.X, _rec.Y + _rec.Height));

                    _dPoint.Add(new Size());
                    _dPoint.Add(new Size());

                    _isLine = false;
                    _havingBorder = true;
                }
                    break;
                case ShapeType.Line:
                case ShapeType.Polygon:
                {
                    foreach (var point in _shape.Verticess)
                    {
                        _controlPoints.Add(point);
                        _dPoint.Add(new Size());
                    }

                    _isLine = true;
                    _havingBorder = false;
                }
                    break;
            }
        }

        public void SetOrginalPoint(Point point)
        {
            if (_isLine)
            {
                for (int i = 0; i < _controlPoints.Count; i++)
                    _dPoint[i] = new Size(point.X - _controlPoints[i].X, point.Y - _controlPoints[i].Y);
            }
            else
            {
                _dPoint[0] = new Size(point.X - _shape.StartVertex.X, point.Y - _shape.StartVertex.Y);
                _dPoint[1] = new Size(point.X - _shape.EndVertex.X, point.Y - _shape.EndVertex.Y);
            }
        }

        public void SetDistanceSquare(int x, int y)
        {
            _hitSize.Width = x - _controlPoints[_hitPosition].X;
            _hitSize.Height = y - _controlPoints[_hitPosition].Y;

            if (!_isLine)
            {
                _anchorPoint = _controlPoints[(_hitPosition + 2)%4];
            }
        }

        public void MoveShape(Point point)
        {
            if (_isLine)
            {
                for (int i = 0; i < _controlPoints.Count; i++)
                {
                    _shape.Verticess[i] = _controlPoints[i] = new Point(point.X - _dPoint[i].Width, point.Y - _dPoint[i].Height);
                }
            }
            else
            {
                Update4Square(point);
            }
        }

        public void MoveSquare(int x, int y)
        {
            _controlPoints[_hitPosition] = new Point(x - _hitSize.Width, y - _hitSize.Height);

            if (_isLine)
                _shape.Verticess[_hitPosition] = _controlPoints[_hitPosition];
            else
            {
                if (_shape.GetShapeType() == ShapeType.Circle)
                    _controlPoints[_hitPosition] = Util.CreateSnapPoint(_controlPoints[_hitPosition].X,
                        _controlPoints[_hitPosition].Y, _anchorPoint);

                Update4Square(_hitPosition);
            }
        }

        public int HitTest(int x, int y)
        {
            // Return 1- SizeNWSE
            //        2- SizeNESW
            //        3- Size
            //        0- No hit
            const int dx = SmallQuareWidth/2;
            const int dy = SmallQuareHeight/2;

            for (var i = 0; i < _controlPoints.Count; i++)
                if (x >= _controlPoints[i].X - dx && x <= _controlPoints[i].X + dx && 
                    y <= _controlPoints[i].Y + dy && y >= _controlPoints[i].Y - dy)
                {
                    _hitPosition = i;
                    if (_isLine)
                        return 3;
                    return i%2 + 1;
                }
            return 0;
        }

        private void DrawBorder()
        {
            _graphic.DrawRectangle(_borderPen, _rec);
        }

        private void DrawSmallQuare(int xc, int yc)
        {
            int w = SmallQuareWidth, h = SmallQuareHeight;
            int xmid = w/2, ymid = h/2;
            xc -= xmid;
            yc -= ymid;

            _graphic.DrawRectangle(new Pen(Color.Black), xc, yc, w, h);
        }

        private void Update4Square(Point point)
        {
            var p = new Point(point.X - _dPoint[0].Width, point.Y - _dPoint[0].Height);
            var q = new Point(point.X - _dPoint[1].Width, point.Y - _dPoint[1].Height);
            _controlPoints[0] = p;
            _controlPoints[2] = q;

            _controlPoints[1] = new Point(q.X, p.Y);
            _controlPoints[3] = new Point(p.X, q.Y);

            UpdateBorder();
            UpdateShapeSize();
        }

        private void Update4Square(int control)
        {
            var p = _controlPoints[control];

            switch (_hitPosition)
            {
                case 0:
                    _controlPoints[1] = new Point(_controlPoints[1].X, p.Y);
                    _controlPoints[3] = new Point(p.X, _controlPoints[3].Y);
                    break;
                case 1:
                    _controlPoints[0] = new Point(_controlPoints[0].X, p.Y);
                    _controlPoints[2] = new Point(p.X, _controlPoints[2].Y);
                    break;
                case 2:
                    _controlPoints[1] = new Point(p.X, _controlPoints[1].Y);
                    _controlPoints[3] = new Point(_controlPoints[3].X, p.Y);
                    break;
                case 3:
                    _controlPoints[0] = new Point(p.X, _controlPoints[0].Y);
                    _controlPoints[2] = new Point(_controlPoints[2].X, p.Y);
                    break;
            }

            UpdateBorder();
            UpdateShapeSize();
        }

        private void UpdateBorder()
        {
            _rec.X = _controlPoints[0].X;
            _rec.Y = _controlPoints[0].Y;
            _rec.Width = _controlPoints[1].X - _controlPoints[0].X;
            _rec.Height = _controlPoints[0].Y - _controlPoints[3].Y;
        }

        private void UpdateShapeSize()
        {
            _shape.StartVertex = _controlPoints[0];
            _shape.EndVertex = _controlPoints[2];
        }

        private const int SmallQuareWidth = 4;
        private const int SmallQuareHeight = 4;

        private Graphics _graphic;
        private Pen _borderPen;
        private IShape _shape;
        private bool _isLine;
        private Rectangle _rec;
        private List<Point> _controlPoints;
        private List<Size> _dPoint;
        private Point _anchorPoint;
        private Size _hitSize;
        private int _hitPosition;
        private bool _havingBorder;
        */
    }
}
