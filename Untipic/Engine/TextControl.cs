using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Untipic.Engine
{
    public class TextControl
    {
        public TextControl(Control gdicontrol)
        {
            _control = gdicontrol;
            _startPoint = Point.Empty;
            _isTyping = false;

            _textbox = new TextBox();
            _textbox.BorderStyle = BorderStyle.None;
            _textbox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _textbox.Location = new Point(12, 12);
            _textbox.Multiline = true;
            _textbox.Name = "textTool";
            _textbox.Size = new Size(100, 20);
            _textbox.TabIndex = 0;
            _textbox.Visible = false;
            _textbox.TextChanged += Textbox_TextChanged;

            _control.Controls.Add(_textbox);
        }

        public event Core.EventArguments.TextEventHandler TextCreated = null;
        public event Core.EventArguments.TextEventHandler TextChanged = null;

        public bool IsTyping
        {
            get { return _isTyping; }
            set { _isTyping = value; }
        }

        public string Text {get { return _textbox.Text; }}

        public void Load()
        {
            //
        }

        public void BeginTypeText(Point p, Font font, Color color)
        {
            Size size = TextRenderer.MeasureText("  ", font);

            _startPoint = p;
            _textbox.Text = "  ";
            _textbox.ForeColor = color;
            _textbox.Location = p;
            _textbox.Font = font;
            _textbox.Visible = true;
            _textbox.Size = size;
            _textbox.Focus();

            _isTyping = true;
        }

        public bool CheckOutSide(Point p)
        {
            return !_textbox.DisplayRectangle.Contains(p);
        }

        public void EndTypeText()
        {
            _isTyping = false;
            _textbox.Visible = false;
            _control.Parent.Focus();

            OnTextCreate(
                new Core.EventArguments.TextEventArgs(new Core.TextObject
                {
                    Location = _textbox.Location,
                    Size = _textbox.Size,
                    Text = _textbox.Text,
                    Font = _textbox.Font,
                    Color = _textbox.ForeColor
                }));
        }

        private void Textbox_TextChanged(object sender, EventArgs e)
        {
            Size size = TextRenderer.MeasureText(_textbox.Text, _textbox.Font);
            _textbox.Size = size;

            OnTextChanged(
                new Core.EventArguments.TextEventArgs(new Core.TextObject
                {
                    Location = _textbox.Location,
                    Size = _textbox.Size,
                    Text = _textbox.Text,
                    Font = _textbox.Font
                }));
        }

        private void OnTextCreate(Core.EventArguments.TextEventArgs e)
        {
            if (TextCreated != null)
                TextCreated(this, e);
        }

        private void OnTextChanged(Core.EventArguments.TextEventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        private bool _isTyping;
        private readonly TextBox _textbox;
        private readonly Control _control;
        private Point _startPoint;

    }
}
