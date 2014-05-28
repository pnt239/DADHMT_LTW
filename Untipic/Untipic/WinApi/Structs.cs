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
using System.Runtime.InteropServices;

namespace Untipic.WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public Point ptReserved;
        /// <summary>
        ///     The size a window should be maximized to. This depends on the screen it will end up, 
        ///     so the window manager will request this info when we move the window around.
        /// </summary>
        public Size MaxSize;
        /// <summary>
        ///     The position of the window when maximized. Must be relative to the current screen, 
        ///     so it's often (0,0) or close to that if the task bar is in the way.
        /// </summary>
        public Point MaxPosition;
        /// <summary>
        ///     The minimum size a window should be allowed to be resized to by dragging it's border or resize handle.
        /// </summary>
        public Size MinTrackSize;
        /// <summary>
        ///     The maximum size a window should be allowed to be resized to by dragging it's border or resize handle.
        ///     This is usually the maximum dimensions of the virtual screen, i.e. the bounding box containing all screens.
        /// </summary>
        public Size MaxTrackSize;
    }
}
