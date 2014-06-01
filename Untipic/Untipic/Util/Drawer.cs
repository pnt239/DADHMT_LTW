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

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Untipic.Util
{
    /// <summary>
    /// Utility methods for draw or create shape.
    /// </summary>
    public class Drawer
    {
        /// <summary>
        /// Creates the leaf shape path
        /// </summary>
        /// <param name="rec">The boundary of leaf shape</param>
        /// <param name="round">The round edge of leaf</param>
        /// <returns>Then GraphicsPath of leaf shape</returns>
        public static GraphicsPath CreateLeaf(Rectangle rec, float round)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddLine(rec.X, rec.Y + rec.Height, rec.X, rec.Y + round);
            path.AddArc(rec.X, rec.Y, round, round, 180F, 90F);
            path.AddLine(rec.X + round, rec.Y, rec.X + rec.Width, rec.Y);
            path.AddLine(rec.X + rec.Width, rec.Y, rec.X + rec.Width, rec.Y + rec.Height - round);
            path.AddArc(rec.X + rec.Width - round, rec.Y + rec.Height - round, round, round, 0F, 90F);
            path.CloseFigure();

            return path;
        }
    }
}
