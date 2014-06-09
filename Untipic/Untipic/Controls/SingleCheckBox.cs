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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Untipic.MetroUI;

namespace Untipic.Controls
{
    public class SingleCheckBox : Control, IButtonControl
    {
        public SingleCheckBox()
        {
            // Init default field's value
            _hoverBorderColor = _borderColor = Color.FromArgb(0xcc, 0xcc, 0xcc);
            _pressedBorderColor = Color.FromArgb(0xa4, 0xa4, 0xa4);

            _backColor = Color.White;
            _hoverBackColor = _pressedBackColor = Color.FromArgb(0xde, 0xde, 0xde);

            _seletedColor = Color.FromArgb(0x4d, 0x4d, 0x4d);

            // Set serveral option for paint
            //SetStyle(
            //    ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.OptimizedDoubleBuffer |
            //    ControlStyles.ResizeRedraw |
            //    ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //SetStyle(ControlStyles.Opaque, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);

            SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

            base.BackColor = Color.Transparent;

        }

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        [Category("Behavior"), DefaultValue(typeof(DialogResult), "None")]
        [Description("The dialog result produced in a modal form by clicking the button.")]
        public DialogResult DialogResult
        {
            get { return _dialogResult; }
            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                    _dialogResult = value;
            }
        }

        public void NotifyDefault(bool value)
        {
// ReSharper disable RedundantCheckBeforeAssignment
            if (_isDefault != value)
// ReSharper restore RedundantCheckBeforeAssignment
                _isDefault = value;
            Invalidate();
        }

        public void PerformClick()
        {
            if (CanSelect)
                base.OnClick(EventArgs.Empty);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Simulate Transparency
            System.Drawing.Drawing2D.GraphicsContainer g = pevent.Graphics.BeginContainer();
            Rectangle translateRect = this.Bounds;
            pevent.Graphics.TranslateTransform(-this.Left, -this.Top);
            PaintEventArgs pe = new PaintEventArgs(pevent.Graphics, translateRect);
            this.InvokePaintBackground(this.Parent, pe);
            this.InvokePaint(this.Parent, pe);
            pevent.Graphics.ResetTransform();
            pevent.Graphics.EndContainer(g);

            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var bolder = GetEffectiveBorderColor();
            var back = GetEffectiveBackColor();
            var rec = new Rectangle(1, 1, Width - 3, Height - 3);

            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //pevent.Graphics.Clear(Color.Transparent);

            using (var b = new SolidBrush(back))
                pevent.Graphics.FillEllipse(b, rec);

            if (_checked)
                using (var b = new SolidBrush(_seletedColor))
                    pevent.Graphics.FillEllipse(b, new Rectangle(1 + 7, 1 + 7, Width - 3 - 7 * 2, Height - 3 - 7 * 2));

            using (var p = new Pen(bolder, 2F))
                pevent.Graphics.DrawEllipse(p, rec);

        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            //var bolder = GetEffectiveBorderColor();
            //var back = GetEffectiveBackColor();
            //var rec = new Rectangle(1, 1, Width - 3, Height - 3);

            //pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            ////pevent.Graphics.Clear(Color.Transparent);

            //using (var b = new SolidBrush(back))
            //    pevent.Graphics.FillEllipse(b, rec);

            //if (_checked)
            //    using (var b = new SolidBrush(_seletedColor))
            //        pevent.Graphics.FillEllipse(b, new Rectangle(1 + 7, 1 + 7, Width - 3 - 7 * 2, Height - 3 - 7 * 2));

            //using (var p = new Pen(bolder, 2F))
            //    pevent.Graphics.DrawEllipse(p, rec);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TRANSPARENT = 0x20;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                return cp;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isPressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            _checked = !_checked;
            base.OnClick(e);
        }

        private Color GetEffectiveBorderColor()
        {
            if (_isPressed)
                return _pressedBorderColor;
            if (_isHovered)
                return _hoverBorderColor;

            return _borderColor;
        }

        private Color GetEffectiveBackColor()
        {
            if (_isPressed)
                return _pressedBackColor;
            if (_isHovered)
                return _hoverBackColor;

            return _backColor;
        }

        private bool _isHovered;
        private bool _isPressed;
        private bool _checked;

        private readonly Color _borderColor;
        private readonly Color _hoverBorderColor;
        private readonly Color _pressedBorderColor;

        private readonly Color _backColor;
        private readonly Color _hoverBackColor;
        private readonly Color _pressedBackColor;

        private readonly Color _seletedColor;

        private DialogResult _dialogResult;
        private bool _isDefault;
    }
}
