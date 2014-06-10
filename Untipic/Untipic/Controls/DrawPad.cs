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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Untipic.Core;
using Untipic.Core.EventArguments;
using Untipic.DrawPadTools;
using Untipic.Engine;
using Untipic.EventArguments;
using Untipic.Visualization;

namespace Untipic.Controls
{
    public partial class DrawPad : UserControl
    {
        public DrawPad()
        {
            InitializeComponent();

            Zoom = 1;

            _shapeDrawer = new ShapeDrawer();
            _filler = new Filler();
            _drawingControl = new DrawingControl();
            _drawingControl.SetShapDrawer(_shapeDrawer);
            _drawingControl.ShapeCreated += DrawingControl_ShapeCreated;

            _textControl = new TextControl(gdiArea);
            _textControl.TextCreated += TextControl_TextCreated;
            _textControl.TextChanged += TextControl_TextChanged;

            _currentCommand = DrawPadCommand.None;
            _currentShape = null;

            _outlineWidth = 2F;
            _outlineColor = Color.Black;
            _outlineDash = DashStyle.Solid;
            _fillColor = Color.Transparent;
            _textFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);

            _shapeArea = 0;
        }

        public event MouseEventHandler GdiMouseMove = null;
        public event MouseEventHandler GdiAddedVertex = null;
        public event EventHandler GdiControlBoxLoad = null;
        public event EventHandler GdiControlBoxUpdated = null;
        public event CommandChangedEventHandler CommandChanged = null;
        public event ShapeCreatedEventHandler ShapeCreated = null;
        public event TextEventHandler TextCreated = null;
        public event TextEventHandler TextChanged = null;
        

        public ShapeDrawer ShapeDrawer
        { 
            get { return _shapeDrawer; }
        }

        public DrawingControl DrawingControl
        {
            get { return _drawingControl; }
        }

        public TextControl TextControl {get { return _textControl; }}

        public DrawPadCommand CurrentCommand
        {
            get { return _currentCommand; }
            set
            {
                _currentCommand = value;
                OnCommandChanged(new CommandChangedEventArgs(_currentCommand));
            }
        }

        public Viewport Viewport
        {
            get
            {
                return _viewport;
            }
            set
            {
                _viewport = value;
                _drawingControl.Viewport = _viewport;
            }
        }

        public float Resolution { get; set; }

        public float Zoom { get; set; }

        public int ViewportWidth { get; set; }

        public int ViewportHeith { get; set; }

        public MessureUnit Unit {get { return _page.Unit; }}

        public float OutlineWidth
        {
            get { return _outlineWidth; }
            set { _outlineWidth = value; }
        }

        public Color OutlineColor
        {
            get { return _outlineColor; }
            set { _outlineColor = value; }
        }

        public DashStyle OutlineDash
        {
            get { return _outlineDash; }
            set { _outlineDash = value; }
        }

        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value; }
        }

        public Font TextFont
        {
            get { return _textFont; }
            set { _textFont = value; }
        }

        public Page Page { get { return _page; } }

        public float ShapeArea
        {
            get { return _shapeArea; }
        }

        public event PaintEventHandler GdiPaint = null;

        public void ChangeTool(CommandObject command)
        {
            ShapeBase shape;
            switch (_currentCommand = command.Command)
            {
                case DrawPadCommand.Selection:
                    gdiArea.Cursor = _currentCursor = CursorTool.GetSelectionCursor();

                    _drawingControl.ControlMode = ControlMode.Transformation;
                    _drawingControl.SwitchToDirectTrans();
                    break;
                case DrawPadCommand.DirectSelection:
                    gdiArea.Cursor = _currentCursor = CursorTool.GetDirectSelectionCursor();

                    _drawingControl.ControlMode = ControlMode.EditPoint;
                    _drawingControl.SwitchToDirectTrans();
                    break;
                case DrawPadCommand.DrawText:
                    gdiArea.Cursor = _currentCursor = CursorTool.GetEditorCursor();
                    break;
                case DrawPadCommand.DrawShape:
                    shape = command.Reserve as ShapeBase;
                    if (shape != null) _currentShape = shape.Clone();
                    gdiArea.Cursor = _currentCursor = CursorTool.GetShapeCursor();
                    // init in drawing control
                    _drawingControl.ControlMode = ControlMode.CreateShape;
                    _drawingControl.LoadShape(_currentShape);
                    // fire event for app manament
                    if (GdiControlBoxLoad != null)
                        GdiControlBoxLoad(this, EventArgs.Empty);
                    break;
                case DrawPadCommand.Brush:
                    shape = command.Reserve as ShapeBase;
                    if (shape != null) LoadCurrentShape(shape.Clone());

                    gdiArea.Cursor = _currentCursor = CursorTool.GetBrushCursor();

                    // init in drawing control
                    shape.OutlineWidth = _outlineWidth;
                    shape.OutlineColor = _outlineColor;
                    shape.OutlineDash = _outlineDash;
                    _drawingControl.ControlMode = ControlMode.CreateShape;
                    _drawingControl.LoadShape(_currentShape, true);

                    // fire event for app manament
                    if (GdiControlBoxLoad != null)
                        GdiControlBoxLoad(this, EventArgs.Empty);
                    break;
                case DrawPadCommand.Eraser:
                    shape = command.Reserve as ShapeBase;
                    if (shape != null) LoadCurrentShape(shape.Clone());

                    gdiArea.Cursor = _currentCursor = CursorTool.GetEraserCursor();

                    _currentShape.OutlineColor = Color.White;
                    _drawingControl.ControlMode = ControlMode.CreateShape;
                    _drawingControl.LoadShape(_currentShape, true);
                    break;
                case DrawPadCommand.Bucket:
                    gdiArea.Cursor = _currentCursor = CursorTool.GetBucketCursor();
                    break;
                case DrawPadCommand.Crop:
                    gdiArea.Cursor = _currentCursor = CursorTool.GetCropCursor();
                    break;
            }
        }

        public void CreateNewPage(float winWidth, float winHeight, MessureUnit unit, float resolution)
        {
            _page = new Page(winWidth, winHeight, unit, resolution);
            Resolution = resolution;

            Viewport = new Viewport(resolution, 1F);

            ViewportWidth = (int)Viewport.WinToView(winWidth);
            ViewportHeith = (int)Viewport.WinToView(winHeight);
            SetViewSize(ViewportWidth, ViewportHeith);

            if (winWidth > 0 && winHeight > 0)
                _imageCache = new ImageCache(Viewport, _shapeDrawer, _filler, _page, ViewportWidth, ViewportHeith);
            else
                _imageCache = new ImageCache(Viewport, _shapeDrawer, _filler, _page, 1, 1);

            gdiArea.Visible = true;
            gdiArea.Invalidate();
        }

        public void AdjustPage()
        {
            Viewport = new Viewport(_page.Resolution, 1F);

            ViewportWidth = (int)Viewport.WinToView(_page.Size.Width);
            ViewportHeith = (int)Viewport.WinToView(_page.Size.Height);
            SetViewSize(ViewportWidth, ViewportHeith);

            _imageCache = new ImageCache(Viewport, _shapeDrawer, _filler, _page, ViewportWidth, ViewportHeith);

            gdiArea.Visible = true;
            gdiArea.Invalidate();
        }

        public void CloseForce()
        {
            gdiArea.Visible = false;
        }

        public void SavePage(Stream stream, System.Drawing.Imaging.ImageFormat format)
        {
            _imageCache.SaveFile(stream, format);
        }

        public void SetViewSize(int width, int height)
        {
            //SuspendLayout();
            //tlpMain.SuspendLayout();

            //gdiArea.Size = size;
            tlpMain.RowStyles[1].Height = height;
            tlpMain.ColumnStyles[1].Width = width;
            //tlpMain.Width = size.Width + 40;
            //tlpMain.Height = size.Height + 40;

            AutoScrollMinSize = new Size(width + 40, height + 40);

            //tlpMain.ResumeLayout(false);
            //ResumeLayout(false);
        }

        public void RePaint()
        {
            gdiArea.Invalidate();
        }

        private void gdiArea_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(Color.White);

            //DrawPage(e.Graphics);
            _imageCache.Render(e.Graphics);

            _drawingControl.Draw(e.Graphics);

            OnGdiPaint(sender, e);
        }

        private void gdiArea_Click(object sender, EventArgs e)
        {
            Focus();
        }

        private void gdiArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (_currentCommand)
                {
                    case DrawPadCommand.Brush:
                    case DrawPadCommand.Eraser:
                    case DrawPadCommand.DrawShape:
                        if (_currentShape.DrawMethod == DrawMethod.ByDragDrop)
                            _drawingControl.BeginCreateShape(e.Location);
                        else if (_currentShape.DrawMethod == DrawMethod.ByClick && !_drawingControl.IsInitializedShape)
                            _drawingControl.BeginCreateShape(e.Location);
                        break;
                    case DrawPadCommand.DirectSelection:
                    case DrawPadCommand.Selection:
                        if (_drawingControl.IsSelected)
                        {
                            if (!_drawingControl.BeginTranslation(e.Location))
                                OnCommandChanged(new CommandChangedEventArgs(DrawPadCommand.DrawShape));
                        }
                        else
                        {
                            ShapeBase shapeSel = ShapesHitTest(Viewport.ViewToWin(e.Location));
                            _drawingControl.LoadShapeSelect(Viewport.WinToView(shapeSel));
                            if (shapeSel != null)
                                _shapeArea = Core.Util.CalculateShapeArea(shapeSel);
                            else
                                _shapeArea = 0;
                        }

                        if (_textControl.IsTyping && _textControl.CheckOutSide(e.Location))
                            _textControl.EndTypeText();

                        break;
                    case DrawPadCommand.DrawText:
                        if (!_textControl.IsTyping)
                            _textControl.BeginTypeText(e.Location, _textFont, _outlineColor);
                        else if (_textControl.CheckOutSide(e.Location))
                            _textControl.EndTypeText();
                        break;
                }
            }
            gdiArea.Invalidate();
        }

        private void gdiArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Drag Mouse
                switch (_currentCommand)
                {
                    case DrawPadCommand.Brush:
                    case DrawPadCommand.Eraser:
                    case DrawPadCommand.DrawShape:
                        if (_currentShape.DrawMethod == DrawMethod.ByDragDrop && DrawingControl.IsInitializedShape)
                            _drawingControl.UpdateShape(e.Location);

                        if (GdiControlBoxUpdated != null)
                            GdiControlBoxUpdated(this, EventArgs.Empty);
                        break;
                    case DrawPadCommand.DirectSelection:
                    case DrawPadCommand.Selection:
                        if (_drawingControl.IsSelected)
                            _drawingControl.UpdateTranslation(e.Location);
                        break;
                }
            }
            gdiArea.Invalidate();

            if (_currentCommand == DrawPadCommand.DrawShape && _currentShape.DrawMethod == DrawMethod.ByClick)
            {
                _drawingControl.UpdateMouse(e.Location);
                gdiArea.Invalidate();
            }

            if (_drawingControl.IsSelected &&
                (_currentCommand == DrawPadCommand.Selection || _currentCommand == DrawPadCommand.DirectSelection))
            {
                var hit = _drawingControl.GetHit(e.Location);
                if (hit != null)
                    gdiArea.Cursor = hit.Cursor;
                else
                    gdiArea.Cursor = _currentCursor;
            }
            //if (_drawingControl.IsSelected && !_hitMove)
            //{
            //    RecBoxBase box;
            //    if ((box = _drawingControl.HitTest(e.Location)) != null)
            //        gdiArea.Cursor = box.Cursor;
            //    else
            //        gdiArea.Cursor = _currentCursor;
            //}

            OnGdiMouseMove(e);
        }

        private void gdiArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (_currentCommand)
                {
                    case DrawPadCommand.Brush:
                    case DrawPadCommand.Eraser:
                    case DrawPadCommand.DrawShape:
                        if (_currentShape.DrawMethod == DrawMethod.ByDragDrop && _drawingControl.IsInitializedShape)
                        {
                            _drawingControl.EndCreateShape();
                            // Auto update command to selection
                            OnCommandChanged(new CommandChangedEventArgs(DrawPadCommand.Selection));
                        }
                        else if (_currentShape.DrawMethod == DrawMethod.ByClick && _drawingControl.IsInitializedShape)
                        {
                            if (!_drawingControl.CreateVertext(e.Location)) 
                            {
                                _drawingControl.EndCreateShape();
                                // Auto update command to selection
                                OnCommandChanged(new CommandChangedEventArgs(DrawPadCommand.Selection));
                            }
                            else
                                if (GdiAddedVertex != null) GdiAddedVertex(this, e);
                        }
                        break;
                    case DrawPadCommand.DirectSelection:
                    case DrawPadCommand.Selection:
                        if (_drawingControl.IsSelected)
                            _drawingControl.EndTranslation();
                        break;
                    case DrawPadCommand.Bucket:
                        _filler.FillByFlood((Bitmap) _page.ImageBuffer, _fillColor, e.Location);
                        break;
                }
                gdiArea.Invalidate();
            } 
            else if (e.Button == MouseButtons.Right)
            {
                switch (_currentCommand)
                {
                    case DrawPadCommand.DrawShape:
                        if (_currentShape.DrawMethod == DrawMethod.ByClick)
                            _drawingControl.EndCreateShape();
                        break;
                }
                
                gdiArea.Invalidate();
            }
        }

        private void gdiArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift)
                _drawingControl.IsRegularShape = true;
        }

        private void gdiArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Shift)
                _drawingControl.IsRegularShape = false;
        }

        private void DrawingControl_ShapeCreated(object sender, ShapeCreatedEventArgs e)
        {
            var shape = e.Shape;
            if (!_drawingControl.IsDirectlyUsing)
            {
                shape.FillColor = _fillColor;
                shape.OutlineWidth = _outlineWidth;
                shape.OutlineColor = _outlineColor;
                shape.OutlineDash = _outlineDash;
            }

            if (ShapeCreated != null)
                ShapeCreated(this, new ShapeCreatedEventArgs(Viewport.ViewToWin(shape)));
            //_page.AddDrawingObject(shape);
        }

        private void TextControl_TextCreated(object sender, TextEventArgs e)
        {
            if (TextCreated != null)
                TextCreated(this, e);
        }

        private void TextControl_TextChanged(object sender, TextEventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        protected virtual void OnGdiPaint(object sender, PaintEventArgs e)
        {
            if (GdiPaint != null)
                GdiPaint(sender, e);
        }

        private void OnGdiMouseMove(MouseEventArgs e)
        {
            if (GdiMouseMove != null)
                GdiMouseMove(this, e);
        }

        private void OnCommandChanged(CommandChangedEventArgs e)
        {
            if (CommandChanged != null)
                CommandChanged(this, e);
        }

        private void LoadCurrentShape(ShapeBase shape)
        {
            _currentShape = shape;
            _currentShape.OutlineWidth = _outlineWidth;
            _currentShape.OutlineColor = _outlineColor;
            _currentShape.FillColor = _fillColor;
        }

        private ShapeBase ShapesHitTest(PointF p)
        {
            ShapeBase shape = null;

            foreach (var obj in _page.DrawingObjects)
            {
                if (obj.GetObjectType() == DrawingObjectType.Shape)
                {
                    shape = obj as ShapeBase;
                    if (shape == null) continue;

                    if (shape.GetShapeType() == ShapeType.Ellipse)
                    {
                        var e = shape as Ellipse;
                        var o = e.OrginalPoint.ToPoint();
                        var t = Math.Pow(p.X - o.X, 2)/Math.Pow(e.MajorAxis, 2) +
                                Math.Pow(p.Y - o.Y, 2)/Math.Pow(e.MinorAxis, 2);
                        if (t <= 1)
                            break;
                    }
                    else
                    {
                        if (Core.Util.CheckInnerPoint(shape.Vertices, new Vertex(p)))
                            break;
                    }

                    shape = null;
                }
            }
            return shape;
        }

        private Page _page;

        private DrawingControl _drawingControl;
        private TextControl _textControl;

        private DrawPadCommand _currentCommand;
        private Viewport _viewport;
        private ShapeBase _currentShape;
        private Cursor _currentCursor;

        private ShapeDrawer _shapeDrawer;
        private Filler _filler;
        private ImageCache _imageCache;

        private float _outlineWidth;
        private Color _outlineColor;
        private DashStyle _outlineDash;
        private Color _fillColor;
        private Font _textFont;

        private float _shapeArea;
    }
}
