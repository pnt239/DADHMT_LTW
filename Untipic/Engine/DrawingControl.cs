#region Copyright (c) 2013 Pham Ngoc Thanh, https://github.com/panoti/DADHMT_LTW/
/**
 * MetroUI - Windows Modern UI for .NET WinForms applications
 * Copyright (c) 2014 Pham Ngoc Thanh, https://github.com/panoti/DADHMT_LTW/
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Untipic.Core;
using Untipic.Core.EventArguments;
using Untipic.Visualization;

namespace Untipic.Engine
{

    public enum ControlMode
    {
        Transformation = 0,
        EditPoint,
        CreateShape,
        ShowText,
        None
    }

    public abstract class RecBoxBase
    {
        public event EventHandler ChangedX = null;
        public event EventHandler ChangedY = null;
        public event EventHandler LocationChanged = null;
        public event EventHandler SizeChanged = null;

        public int X
        {
            get { return _rec.X; }
            set
            {
                if (!IsLockX && (Parrent == null || Parrent.Width >= 15))
                {
                    _rec.X = value;
                    OnChangeX(value);

                    OnLocationChanged();
                }
            }
        }

        public int Y
        {
            get { return _rec.Y; }
            set
            {
                if (!IsLockY && (Parrent == null || Parrent.Height > 16))
                {
                    _rec.Y = value;
                    OnChangeY(value);

                    OnLocationChanged();
                }
            }
        }

        public int Width
        {
            get { return _rec.Width; }
            set
            {
                _rec.Width = value;
                if (SizeChanged != null)
                    SizeChanged(this, EventArgs.Empty);
            }
        }

        public int Height
        {
            get { return _rec.Height; }
            set
            {
                _rec.Height = value;
                if (SizeChanged != null)
                    SizeChanged(this, EventArgs.Empty);
            }
        }

        public Rectangle Rec
        {
            get { return _rec; }
            set
            {
                _rec = value;
                OnChangeRec();
            }
        }

        public bool IsLockX { get; set; }

        public bool IsLockY { get; set; }

        public Cursor Cursor { get; set; }

        public RecBoxBase Parrent { get; set; }

        public object Tag { get; set; }

        public void SetAnchor(Point p)
        {
            _dx = p.X - X;
            _dy = p.Y - Y;
        }

        public void MoveTo(Point p)
        {
            X = p.X - _dx;
            Y = p.Y - _dy;
        }

        public virtual void OnChangeX(int val)
        {
            if (ChangedX != null)
                ChangedX(this, EventArgs.Empty);
        }

        public virtual void OnChangeY(int val)
        {
            if (ChangedY != null)
                ChangedY(this, EventArgs.Empty);
        }

        public virtual void OnChangeRec()
        {
        }

        public virtual RecBoxBase HitTest(Point p)
        {
            if (_rec.Contains(p))
                return this;
            return null;
        }

        public void SetX(int x)
        {
            _rec.X = x;
            OnLocationChanged();
        }

        public void SetY(int y)
        {
            _rec.Y = y;
            OnLocationChanged();
        }

        public void SetWidth(int w) { _rec.Width = w; }
        public void SetHeight(int h) { _rec.Height = h; }
        public void SetRec(Rectangle rec) { _rec = rec; }

        private void OnLocationChanged()
        {
            if (LocationChanged != null)
                LocationChanged(this, EventArgs.Empty);
        }

        private int _dx;
        private int _dy;

        private Rectangle _rec;
    }

    public class ControlPoint : RecBoxBase
    {
        public ControlPoint()
        {
            _controlDependX = new List<ControlPoint>();
            _controlDependY = new List<ControlPoint>();
        }

        public ControlPoint(Cursor cursor) : this()
        {
            Cursor = cursor;
        }

        public ControlPoint(int x, int y, RecBoxBase parrent) : this()
        {
            _x = x;
            _y = y;
            Parrent = parrent;

            SetRec(new Rectangle(x - 2, y - 2, 5, 5));
        }

        public int CenterX
        {
            get { return _x; }
            set
            {
                _x = value;
                SetX(_x - 2);
            }
        }

        public int CenterY
        {
            get { return _y; }
            set
            {
                _y = value;
                SetY(_y - 2);
            }
        }

        public void AddControlDependX(ControlPoint point)
        {
            _controlDependX.Add(point);
        }

        public void AddControlDependY(ControlPoint point)
        {
            _controlDependY.Add(point);
        }

        public void Draw(Graphics g, Color color)
        {
            using (var b = new SolidBrush(Color.White))
            using (var p = new Pen(color, 2F))
            {
                g.FillRectangle(b, Rec);
                g.DrawRectangle(p, Rec);
            }
        }

        public Point Location()
        {
            return new Point(_x, _y);
        }

        public override void OnChangeX(int val)
        {
            for (int i = 0; i < _controlDependX.Count; i++)
            {
                _controlDependX[i].CenterX = val+2;
            }
            _x = X + 2;

            base.OnChangeX(val);
        }

        public override void OnChangeY(int val)
        {
            for (int i = 0; i < _controlDependY.Count; i++)
            {
                _controlDependY[i].CenterY = val+2;
            }
            _y = Y + 2;

            base.OnChangeY(val);
        }

        private int _x;
        private int _y;

        private readonly List<ControlPoint> _controlDependX;
        private readonly List<ControlPoint> _controlDependY;
    }

    public class ShapeBox : RecBoxBase
    {
        public ShapeBox()
        {
            _controlPoints = new List<ControlPoint>();
            LockSize = false;

            int[] dx = {X, MidX, X + Width, X + Width, X + Width, MidX, X, X};
            int[] dy = { Y, Y, Y, MidY, Y + Height, Y + Height, Y + Height, MidY };

            for (int i = 0; i < 8; i++)
                _controlPoints.Add(new ControlPoint(dx[i], dy[i], this));

            _controlPoints[0].Cursor = Cursors.SizeNWSE;
            _controlPoints[0].AddControlDependX(_controlPoints[6]);
            _controlPoints[0].AddControlDependY(_controlPoints[2]);
            _controlPoints[0].ChangedX += ControlPoint_Changed;
            _controlPoints[0].ChangedY += ControlPoint_Changed;

            _controlPoints[1].Cursor = Cursors.SizeNS;
            _controlPoints[1].IsLockX = true;
            _controlPoints[1].AddControlDependY(_controlPoints[0]);
            _controlPoints[1].AddControlDependY(_controlPoints[2]);
            _controlPoints[1].ChangedY += ControlPoint_Changed;

            _controlPoints[2].Cursor = Cursors.SizeNESW;
            _controlPoints[2].AddControlDependX(_controlPoints[4]);
            _controlPoints[2].AddControlDependY(_controlPoints[0]);
            _controlPoints[2].ChangedX += ControlPoint_Changed;
            _controlPoints[2].ChangedY += ControlPoint_Changed;

            _controlPoints[3].Cursor = Cursors.SizeWE;
            _controlPoints[3].IsLockY = true;
            _controlPoints[3].AddControlDependX(_controlPoints[2]);
            _controlPoints[3].AddControlDependX(_controlPoints[4]);
            _controlPoints[3].ChangedX += ControlPoint_Changed;

            _controlPoints[4].Cursor = Cursors.SizeNWSE;
            _controlPoints[4].AddControlDependX(_controlPoints[2]);
            _controlPoints[4].AddControlDependY(_controlPoints[6]);
            _controlPoints[4].ChangedX += ControlPoint_Changed;
            _controlPoints[4].ChangedY += ControlPoint_Changed;

            _controlPoints[5].Cursor = Cursors.SizeNS;
            _controlPoints[5].IsLockX = true;
            _controlPoints[5].AddControlDependY(_controlPoints[6]);
            _controlPoints[5].AddControlDependY(_controlPoints[4]);
            _controlPoints[5].ChangedY += ControlPoint_Changed;

            _controlPoints[6].Cursor = Cursors.SizeNESW;
            _controlPoints[6].AddControlDependX(_controlPoints[0]);
            _controlPoints[6].AddControlDependY(_controlPoints[4]);
            _controlPoints[6].ChangedX += ControlPoint_Changed;
            _controlPoints[6].ChangedY += ControlPoint_Changed;

            _controlPoints[7].Cursor = Cursors.SizeWE;
            _controlPoints[7].IsLockY = true;
            _controlPoints[7].AddControlDependX(_controlPoints[0]);
            _controlPoints[7].AddControlDependX(_controlPoints[6]);
            _controlPoints[7].ChangedX += ControlPoint_Changed;
            Cursor = Cursors.SizeAll;
        }

        public int MidX {get { return X + Width/2; }}

        public int MidY { get { return Y + Height / 2; } }

        public override void OnChangeX(int val)
        {
            base.OnChangeX(val);

            int[] dx = { X, MidX, X + Width, X + Width, X + Width, MidX, X, X };
            for (int i = 0; i < 8; i++)
                _controlPoints[i].CenterX = dx[i];
        }

        public override void OnChangeY(int val)
        {
            base.OnChangeY(val);

            int[] dy = { Y, Y, Y, MidY, Y + Height, Y + Height, Y + Height, MidY };
            for (int i = 0; i < 8; i++)
                _controlPoints[i].CenterY = dy[i];
        }

        public override void OnChangeRec()
        {
            UpdateControl();
        }

        public override RecBoxBase HitTest(Point p)
        {
            if (!LockSize)
            {
                RecBoxBase ret;
                for (int i = 0; i < 8; i++)
                    if ((ret = _controlPoints[i].HitTest(p)) != null)
                        return ret;
            }

            return base.HitTest(p);
        }

        public bool LockSize { get; set; }

        public bool Visible { get; set; }

        public void UpdateControl()
        {
            int[] dx = { X, MidX, X + Width, X + Width, X + Width, MidX, X, X };
            int[] dy = { Y, Y, Y, MidY, Y + Height, Y + Height, Y + Height, MidY };

            for (int i = 0; i < 8; i++)
            {
                _controlPoints[i].CenterX = dx[i];
                _controlPoints[i].CenterY = dy[i];
            }
        }

        public void Draw(Graphics g, Color color)
        {
            if (!Visible)
                return;

            using (var p = new Pen(color, 2F))
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                p.DashPattern = new float[] { 2, 5 };
                g.DrawRectangle(p, Rec);
            }

            for (int i = 0; i < 8; i++)
                _controlPoints[i].Draw(g, color);
        }

        private void ControlPoint_Changed(object sender, EventArgs e)
        {
            _controlPoints[1].CenterY = _controlPoints[0].CenterY;
            _controlPoints[1].CenterX = (_controlPoints[0].CenterX + _controlPoints[2].CenterX)/2;

            _controlPoints[3].CenterX = _controlPoints[2].CenterX;
            _controlPoints[3].CenterY = (_controlPoints[2].CenterY + _controlPoints[4].CenterY) / 2;

            _controlPoints[5].CenterY = _controlPoints[4].CenterY;
            _controlPoints[5].CenterX = (_controlPoints[6].CenterX + _controlPoints[4].CenterX) / 2;

            _controlPoints[7].CenterX = _controlPoints[0].CenterX;
            _controlPoints[7].CenterY = (_controlPoints[0].CenterY + _controlPoints[6].CenterY) / 2;

            SetX(_controlPoints[0].CenterX);
            SetY(_controlPoints[0].CenterY);

            Width = _controlPoints[4].CenterX - _controlPoints[0].CenterX;
            Height = _controlPoints[4].CenterY - _controlPoints[0].CenterY;
        }

        private List<ControlPoint> _controlPoints;
    }

    public class DirectBox : RecBoxBase
    {
        public DirectBox()
        {
            _controlPoints = new List<ControlPoint>();
            LockSize = false;
        }

        public bool LockSize { get; set; }

        public bool Visible { get; set; }

        public bool ShowPosition { get; set; }

        public void LoadShape(ShapeBase shape, Viewport viewport)
        {
            _shape = shape;
            _viewport = viewport;

            foreach (var ctrl in _controlPoints)
            {
                ctrl.LocationChanged -= Control_LocationChanged;
            }
            _controlPoints.Clear();

            foreach (var v in _shape.Vertices)
            {
                var control = new ControlPoint((int)Math.Round(v.X), (int)Math.Round(v.Y), null);
                control.LocationChanged += Control_LocationChanged;
                control.Tag = v;
                control.Cursor = Cursors.SizeAll;
                _controlPoints.Add(control);
            }

            if (_shape.GetShapeType() == ShapeType.Ellipse)
            {
                _controlPoints.Add(new ControlPoint((int) Math.Round(_shape.Location.X + _shape.Size.Width/2),
                    (int) Math.Round(_shape.Location.Y), null));

                _controlPoints.Add(new ControlPoint((int)Math.Round(_shape.Location.X + _shape.Size.Width),
                    (int)Math.Round(_shape.Location.Y + _shape.Size.Height/2), null));

                _controlPoints.Add(new ControlPoint((int)Math.Round(_shape.Location.X + _shape.Size.Width/2),
                    (int)Math.Round(_shape.Location.Y + _shape.Size.Height), null));

                _controlPoints.Add(new ControlPoint((int)Math.Round(_shape.Location.X),
                    (int)Math.Round(_shape.Location.Y + _shape.Size.Height/2), null));


            }
        }

        public void Draw(Graphics g, Color color)
        {
            if (!Visible)
                return;

            for (int i = 0; i < _controlPoints.Count; i++)
            {
                _controlPoints[i].Draw(g, color);

                using (var b = new SolidBrush(Color.Red))
                using (var f = new Font("Arial", 9))
                {
                // Create point for upper-left corner of drawing.
                    PointF drawPoint = new PointF(_controlPoints[i].CenterX + 2, _controlPoints[i].Y + 1);

                    g.DrawString(
                        string.Format("({0:F2}, {1:F2})", _viewport.ViewToWin(_controlPoints[i].CenterX),
                            _viewport.ViewToWin(_controlPoints[i].CenterY)), f, b, drawPoint);
                }
            }
        }

        public override RecBoxBase HitTest(Point p)
        {
            if (!LockSize)
            {
                RecBoxBase ret;
                for (int i = 0; i < _controlPoints.Count; i++)
                    if ((ret = _controlPoints[i].HitTest(p)) != null)
                        return ret;
            }

            return null;
        }

        private void Control_LocationChanged(object sender, EventArgs e)
        {
            var control = sender as ControlPoint;
            if (control == null) return;

            var vertex = control.Tag as Vertex;
            if (vertex == null) return;

            vertex.X = control.CenterX;
            vertex.Y = control.CenterY;
        }

        private List<ControlPoint> _controlPoints;
        private ShapeBase _shape;
        private Viewport _viewport;
    }

    public class DrawingControl
    {
        public DrawingControl()
        {
            _shapeDrawer = new ShapeDrawer();
            _transformBox = new ShapeBox();
            _directBox = new DirectBox();
            _transformBox.LocationChanged += ControlBox_LocationChanged;
            _transformBox.SizeChanged += ControlBox_SizeChanged;
            //_shapeDrawer = shapeDrawer;
            _controlMode = ControlMode.None;
            //_isEditing = false;
            _isSelected = false;
            _isShowBox = false;
            IsRegularShape = false;
            Visible = true;

            _text = "";

            _selectionRecColor = Color.Blue;
            _selectionRecWidth = 1F;
        }

        public event ShapeCreatedEventHandler ShapeCreated = null;

        public ControlMode ControlMode
        {
            get { return _controlMode; }
            set { _controlMode = value; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        public bool IsCreatedShape { get { return _isCreatedShape; }}

        public bool IsInitializedShape {get { return _isInitShape; }}

        public bool IsDirectlyUsing {get { return _isDirectlyUsing; }}

        public bool IsRegularShape { get; set; }

        public bool Visible { get; set; }

        public Point StartPoint 
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        public Point EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }

        public Point LastPoint
        {
            get { return _lastPoint; }
            set { _lastPoint = value; }
        }

        public ShapeBase ReviewShape
        {
            get { return _shape; }
        }

        public RecBoxBase HitControl {get { return _hitControl; }}

        public Viewport Viewport
        {
            get { return _viewport; }
            set { _viewport = value; }
        }

        public void Draw(Graphics grahps)
        {
            if (!Visible)
                return;

            if (_controlMode == ControlMode.CreateShape || _controlMode == ControlMode.Transformation || _controlMode == ControlMode.EditPoint)
            {
                _shapeDrawer.Draw(_shape, grahps);
                //using (var p = new Pen(Color.Black, 1F))
                //    grahps.DrawRectangle(p, _recSelection);
                if (_controlMode == ControlMode.CreateShape && _shape.GetShapeType() == ShapeType.Polygon &&
                    _lastPoint != Point.Empty)
                {
                    using (var p = new Pen(_selectionRecColor, _selectionRecWidth))
                        grahps.DrawLine(p, _lastPoint, _endPoint);
                }

                DrawBox(grahps);
            }
            else if (_controlMode == ControlMode.ShowText)
            {
                using (var b = new SolidBrush(_selectionRecColor))
                    grahps.DrawString(_text, _textFont, b, _textLocation);
            }
            
        }

        public void DrawBox(Graphics g)
        {
            if (!_isShowBox) return;

            _transformBox.Draw(g, _selectionRecColor);

            _directBox.Draw(g, _selectionRecColor);
        }

        public void SetShapDrawer(ShapeDrawer shapDrawer)
        {
            _shapeDrawer = shapDrawer;
        }

        public void LoadShape(ShapeBase shape, bool useDirectly = false)
        {
            if (!_isCreatedShape && _isInitShape && _shape != null)
            {
                OnShapeCreated(new ShapeCreatedEventArgs(_shape));

                _isCreatedShape = true;
                _isInitShape = false;

                SelectShape(false);
            }

            _shape = shape;
            _isDirectlyUsing = useDirectly;

            if (!useDirectly)
            {
                _shape.OutlineColor = _selectionRecColor;
                _shape.OutlineWidth = _selectionRecWidth;
            }
        }

        public void LoadShapeSelect(ShapeBase shape)
        {
            if (shape == null)
            {
                _directBox.Visible = false;
                _directBox.ShowPosition = false;
                _isShowBox = false;
                return;
            }

            _directBox.LoadShape(shape, _viewport);
            _directBox.Visible = true;
            _directBox.ShowPosition = true;
            _isShowBox = true;
        }

        public void LoadFont(Font font)
        {
            //_textFont = font;
        }

        public void BeginCreateShape(Point position)
        {
            _startPoint = position;
            _endPoint = position;

            _isCreatedShape = false;
            _isInitShape = true;
            SelectShape(false);

            if (_shape.GetShapeType() == ShapeType.Line)
            {
                _shape.Vertices[0] = new Vertex(position);
                _shape.Vertices[1] = new Vertex(position);
            }
        }

        public bool CreateVertext(Point position)
        {
            var vcount = _shape.Vertices.Count;
            if (vcount == 0)
                _startPoint = position;

            if (Util.GetDistance(_startPoint, position) < 5 && vcount != 0)
            {
                //EndCreateShape();
                return false;
            }

            var bound = GetShapeBound();
            _shape.Location = bound.Location;
            _shape.Size = bound.Size;

            _lastPoint = position;
            _shape.Vertices.Add(new Vertex(position));

            return true;
        }

        public bool BeginTranslation(Point p)
        {
            if (_controlMode == ControlMode.Transformation)
                _hitControl = _transformBox.HitTest(p);
            else if (_controlMode == ControlMode.EditPoint)
                _hitControl = _directBox.HitTest(p);

            if (_hitControl == null)
                return false;

            _hitControl.SetAnchor(p);
            return true;
        }

        /// <summary>
        /// Use for a action can update a control
        /// </summary>
        /// <param name="shapeType">Type of the shape.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public void UpdateControl(ShapeType shapeType, Point start, Point end)
        {
            if (_shape == null || shapeType != _shape.GetShapeType())
                LoadShape(ShapeFactory.CreateShape(shapeType));

            if (_shape != null && _shape.Vertices.Count > 0 && _shape.GetShapeType() != ShapeType.Polygon)
                _shape.Vertices[0] = new Vertex(start);
            _startPoint = start;

            ControlMode = ControlMode.CreateShape;

            UpdateShape(end);
        }

        public void UpdateText(string text, Font font, PointF location)
        {
            _text = text;
            _textFont = font;
            _textLocation = location;

            if (_text == "")
                Visible = false;
            else
                Visible = true;

            ControlMode = ControlMode.ShowText;
        }

        public void UpdateShape(Point position)
        {
            UpdateMouse(position);

            _recSelection = CalcRectangleSelection();
            _shape.Location = _recSelection.Location;
            _shape.Size = _recSelection.Size;

            switch (_shape.GetShapeType())
            {
                case ShapeType.Line:
                    _shape.Vertices[0] = new Vertex(_startPoint);
                    _shape.Vertices[1] = new Vertex(_endPoint);
                    break;
                case ShapeType.IsoscelesTriangle:
                    _shape.Vertices[0] = new Vertex(_recSelection.X + _recSelection.Width / 2, _recSelection.Y);
                    _shape.Vertices[1] = new Vertex(_recSelection.X + _recSelection.Width, _recSelection.Y + _recSelection.Height);
                    _shape.Vertices[2] = new Vertex(_recSelection.X, _recSelection.Y + _recSelection.Height);
                    break;
                case ShapeType.Oblong:
                    _shape.Vertices[0] = new Vertex(_recSelection.X, _recSelection.Y);
                    _shape.Vertices[1] = new Vertex(_recSelection.X + _recSelection.Width, _recSelection.Y);
                    _shape.Vertices[2] = new Vertex(_recSelection.X + _recSelection.Width, _recSelection.Y + _recSelection.Height);
                    _shape.Vertices[3] = new Vertex(_recSelection.X, _recSelection.Y + _recSelection.Height);
                    break;
                case ShapeType.FreePencil:
                    _shape.Vertices.Add(new Vertex(position));
                    break;
            }
        }

        public void UpdateMouse(Point position)
        {
            _endPoint = IsRegularShape ? PointSnapAngle(position) : position;
        }

        public void UpdateTranslation(Point p)
        {
            if (_shape.CanMove)
                _hitControl.MoveTo(p);
        }

        public void EndCreateShape()
        {
            if (_shape.GetShapeType() == ShapeType.Polygon)
                ((PolygonBase) _shape).IsClosedFigure = true;

            var bound = GetShapeBound();
            _shape.Location = bound.Location;
            _shape.Size = bound.Size;

            SelectShape();

            _startPoint = _endPoint = _lastPoint = Point.Empty;
        }

        public void EndTranslation()
        {
            //
        }

        public RecBoxBase GetHit(Point p)
        {
            if (_controlMode == ControlMode.Transformation)
                return _transformBox.HitTest(p);

            if (_controlMode == ControlMode.EditPoint)
                return _directBox.HitTest(p);

            return null;
        }

        public void SwitchToDirectTrans()
        {
            if (_isSelected)
            {
                if (_controlMode == ControlMode.Transformation)
                {
                    _transformBox.Visible = true;
                    _directBox.Visible = false;
                }
                else if (_controlMode == ControlMode.EditPoint)
                {
                    _transformBox.Visible = false;
                    _directBox.Visible = true;
                }
            }
        }

        private void ControlBox_SizeChanged(object sender, EventArgs e)
        {
            //if (_shape != null)
            //    _shape.Location = _controlBox.Rec.Location;
        }

        private void ControlBox_LocationChanged(object sender, EventArgs e)
        {
            _startPoint.X = _transformBox.Rec.Location.X;
            _startPoint.Y = _transformBox.Rec.Location.Y;

            _endPoint.X = _transformBox.Rec.Location.X + _transformBox.Rec.Width;
            _endPoint.Y = _transformBox.Rec.Location.Y + _transformBox.Rec.Height;

            UpdateShape(_endPoint);
        }

        private void OnShapeCreated(ShapeCreatedEventArgs e)
        {
            if (ShapeCreated != null)
                ShapeCreated(this, e);
        }

        private void SelectShape(bool state = true)
        {
            _isSelected = state;
            _isShowBox = state;
            if (state)
            {
                _transformBox.Rec = GetShapeBound();
                _transformBox.LockSize = !_shape.CanResize;
                _transformBox.Visible = true;

                if (_shape.Vertices.Count > 0)
                {
                    _directBox.LoadShape(_shape, _viewport);
                    _directBox.Visible = false;
                }

                _controlMode = ControlMode.Transformation;
            }
            else
            {
                _transformBox.Visible = false;
                _directBox.Visible = false;
            }
        }

        private Rectangle CalcRectangleSelection()
        {
            var minp = new Point
            {
                X = _startPoint.X < _endPoint.X ? _startPoint.X : _endPoint.X,
                Y = _startPoint.Y < _endPoint.Y ? _startPoint.Y : _endPoint.Y
            };

            return new Rectangle(minp, new Size(Math.Abs(_startPoint.X - _endPoint.X),
                Math.Abs(_startPoint.Y - _endPoint.Y)));
        }

        private Rectangle GetShapeBound()
        {

            if (_shape.GetShapeType() == ShapeType.Ellipse)
                return new Rectangle(Point.Round(_shape.Location), Size.Round(_shape.Size));

            if (_shape.Vertices.Count < 1)
                return new Rectangle();

            int minx = (int)_shape.Vertices[0].X;
            int miny = (int)_shape.Vertices[0].Y;
            int maxx = minx, maxy = miny;

            for (int i = 0; i < _shape.Vertices.Count; i++)
            {
                // find min x
                if (_shape.Vertices[i].X < minx)
                    minx = (int)_shape.Vertices[i].X;
                // find max x
                if (_shape.Vertices[i].X > maxx)
                    maxx = (int)_shape.Vertices[i].X;
                // find min y
                if (_shape.Vertices[i].Y < miny)
                    miny = (int)_shape.Vertices[i].Y;
                // find max y
                if (_shape.Vertices[i].Y > maxy)
                    maxy = (int)_shape.Vertices[i].Y;
            }

            return new Rectangle(minx, miny, maxx - minx, maxy - miny);
        }

        private Point PointSnapAngle(Point position)
        {
            var p = position;
            // Snap point
            var deltaX = _startPoint.X - p.X;
            var deltaY = _startPoint.Y - p.Y;
            var asbX = Math.Abs(deltaX);
            var asbY = Math.Abs(deltaY);

            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                // Tinh y theo x
                p.Y = _startPoint.Y; // Default is horital
                if (
                    !((_shape.GetShapeType() == ShapeType.Line) &&
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
                            (asbX*
                             (_shape.GetShapeType() == ShapeType.IsoscelesTriangle ? Math.Sin(Math.PI/3) : 1))*
                          deltaY/asbY);
                }
            }
            else
            {
                // Tinh x theo y
                p.X = _startPoint.X; // Default is vertical
                if (
                    !((_shape.GetShapeType() == ShapeType.Line) &&
                      ((double) asbX/asbY < Math.Tan(Math.PI/8))))
                {
                    p.X -= (deltaX == 0
                        ? 0
                        : (int)
                            (asbY/
                             (_shape.GetShapeType() == ShapeType.IsoscelesTriangle ? Math.Sin(Math.PI/3) : 1))*
                          deltaX/asbX);
                }
            }

            return p;
        }

        private ControlMode _controlMode;
        //private bool _isEditing;
        private bool _isSelected;
        private bool _isShowBox;
        private bool _isCreatedShape;
        private bool _isInitShape;
        private bool _isDirectlyUsing;
        private readonly Color _selectionRecColor;
        private readonly float _selectionRecWidth;

        private Viewport _viewport;
        private ShapeBase _shape;
        private readonly ShapeBox _transformBox;
        private readonly DirectBox _directBox;
        private RecBoxBase _hitControl;
        //private int _shapeId;
        private string _text;
        private Font _textFont;
        private PointF _textLocation;
        private Point _startPoint;
        private Point _endPoint;
        private Point _lastPoint;
        private Rectangle _recSelection;

        private ShapeDrawer _shapeDrawer;
    }
}
