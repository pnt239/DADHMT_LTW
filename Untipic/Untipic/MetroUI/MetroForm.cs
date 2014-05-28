/**
 * 
 * See https://github.com/viperneo/winforms-modernui
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Untipic.MetroUI
{
    public class MetroForm : Form
    {
        #region Constructor

        public MetroForm()
        {
            // Init default field's value
            _backgroundColor = Color.White;
            _borderColor = Color.FromArgb(0xcc, 0xcc, 0xcc);

            // Set serveral option for paint
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |  // <-- prevents size handle artifacts
                ControlStyles.UserPaint, true);
            
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;

            // Build Windows;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //UpdateShadow(MetroFormShadowType.None);
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try // without ControlStyles.AllPaintingInWmPaint, we need our own error handling
            {
                // Clear all by white
                e.Graphics.Clear(_backgroundColor);

                // Draw border
                const float iborder = BORDER_WIDTH*2;
                using (var b = new LinearGradientBrush(new Point(0, 0), new Point(Width, Height),
                    _borderColor, Color.FromArgb(0xa4, 0xa4, 0xa4)))
                using (var p = new Pen(b, iborder))
                    e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width, Height));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                Invalidate();
            }
        }

        #endregion

        #region Privated Field

        /// <summary>
        /// The background color
        /// </summary>
        private Color _backgroundColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _borderColor;


        /// <summary>
        /// The border width
        /// </summary>
        private const int BORDER_WIDTH = 5;

        #endregion
    }
}
