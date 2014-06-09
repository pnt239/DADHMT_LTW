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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Untipic.Controls;
using Untipic.Core.EventArguments;
using Untipic.MetroUI;
using Untipic.Util;

namespace Untipic
{
    public partial class MainGui : MetroForm
    {
        public MainGui()
        {
            InitializeComponent();

            // connect tools toolstrip with drawpad
            PerformConnect();
            // Start manager
            StartManager();
        }

        private void MainGui_FormClosing(object sender, FormClosingEventArgs e)
        {
            _appManament.DisconnectAll();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            using (var frmNew = new Forms.NewForm(21F, 29.7F, 37.62F))
                if (frmNew.ShowDialog() == DialogResult.OK)
                {
                    drawPad.CreateNewPage(frmNew.WinWidth, frmNew.WinHeight, frmNew.Unit, frmNew.Resolution);
                    if (frmNew.Ip != null)
                    {
                        _appManament.CreateServer(frmNew.Ip);

                        ShowListAccount();
                    }
                }
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            using (var frmOpen = new Forms.OpenForm())
            if (frmOpen.ShowDialog() == DialogResult.OK)
            {
                //drawPad.CreateNewPage(frmNew.WinWidth, frmNew.WinHeight, frmNew.Unit, frmNew.Resolution);
                if (!frmOpen.IsLocal)
                {
                    _appManament.CreateClient(frmOpen.IpAddress);

                    ShowListAccount();
                }
            }
        }

        private void tsbAccounts_Click(object sender, EventArgs e)
        {
            if (_listClients == null)
                return;

            _listClients.Visible = true;
        }

        private void _listClients_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _listClients.Visible = false;
        }

        private void PerformConnect()
        {
            // Tools toolstrip manager initialize
            _toolsManager = new ToolStripMananer(true);
            // connect selection
            _toolsManager.Connect(tsbToolSelection,
                new DrawPadTools.CommandObject(DrawPadTools.DrawPadCommand.Selection), drawPad.ChangeTool);
            // connect direct selection
            _toolsManager.Connect(tsbToolDirectSel,
                new DrawPadTools.CommandObject(DrawPadTools.DrawPadCommand.DirectSelection), drawPad.ChangeTool);
            // connect draw text
            _toolsManager.Connect(tsbToolText,
                new DrawPadTools.CommandObject(DrawPadTools.DrawPadCommand.DrawText), drawPad.ChangeTool);
            // connect draw shape
            _toolsManager.Connect(tsbToolShape,
                new DrawPadTools.CommandObject(DrawPadTools.DrawPadCommand.DrawShape,
                    new Core.Line()), drawPad.ChangeTool);
            // connect crop
            _toolsManager.Connect(tsbToolCrop,
                new DrawPadTools.CommandObject(DrawPadTools.DrawPadCommand.Crop), drawPad.ChangeTool);

            _colorManager = new ToolStripMananer();
            // connect change outline
            _colorManager.Connect(tsbToolOutline,
                new DrawPadTools.CommandObject(DrawPadTools.DrawPadCommand.ChangeOutline), drawPad.ChangeTool);
            // connect change fill
            _colorManager.Connect(tsbToolFill,
                new DrawPadTools.CommandObject(DrawPadTools.DrawPadCommand.ChangeFill), drawPad.ChangeTool);
        }

        private void ShowListAccount()
        {
            if (_listClients != null)
            {
                _listClients.Close();
                _listClients.Dispose();
            }

            _listClients = new Forms.ClientsForm();
            _listClients.Size = new Size(_listClients.Width, Height);
            _listClients.FormClosing += _listClients_FormClosing;
            _listClients.Show();
        }

        private void StartManager()
        {
            _appManament = new Engine.AppManament();
            _appManament.ShapeDrawer = drawPad.ShapeDrawer;
            _appManament.UserAdmiting += AppManament_UserAdmiting;
            _appManament.UserConnected += AppManament_UserConnected;
            _appManament.UserDisconnected += AppManament_UserDisconnected;
            _appManament.UserAdded += AppManament_UserAdded;
            _appManament.UserRemoved += AppManament_UserRemoved;
            _appManament.RePaint += AppManament_RePaint;
        }

        private void drawPad_GdiMouseMove(object sender, MouseEventArgs e)
        {
            _appManament.SendMouseMove(e.Location);
        }

        private void drawPad_GdiPaint(object sender, PaintEventArgs e)
        {
            _appManament.DrawPointer(e.Graphics);
            _appManament.DrawControlBox(e.Graphics);
        }

        private void drawPad_GdiControlBoxLoad(object sender, EventArgs e)
        {
            _appManament.LoadControlBox(drawPad.DrawingControl);
        }

        private void drawPad_GdiControlBoxUpdated(object sender, EventArgs e)
        {
            _appManament.SendControlBox(drawPad.DrawingControl);
        }

        private void drawPad_GdiAddedVertex(object sender, MouseEventArgs e)
        {
            _appManament.AddVertexControlBox(drawPad.DrawingControl, e.Location);
        }

        private void AppManament_UserAdmiting(object sender, ClientConnectingEventArgs e)
        {
            if (
                MessageBox.Show(
                    @"Client '" + e.ClientInfo + @"' is joining room. Do you want allow him or her admit room?",
                    @"Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.IsAccept = false;
        }

        private void AppManament_UserConnected(object sender, Engine.UserInfoEventArgs e)
        {
            if (_listClients != null)
                _listClients.AddUser(e.User);
        }

        private void AppManament_UserDisconnected(object sender, Engine.UserInfoEventArgs e)
        {
            if (_listClients != null)
                _listClients.RemoveUser(e.User);
        }

        private void AppManament_UserAdded(object sender, Engine.UserInfoEventArgs e)
        {
            if (_listClients != null)
                _listClients.AddUser(e.User);
        }

        void AppManament_UserRemoved(object sender, Engine.UserInfoEventArgs e)
        {
            if (_listClients != null)
                _listClients.RemoveUser(e.User);
        }

        private void AppManament_RePaint(object sender, EventArgs e)
        {
            drawPad.RePaint();
        }

        private ToolStripMananer _toolsManager;
        private ToolStripMananer _colorManager;
        private Engine.AppManament _appManament;
        private Forms.ClientsForm _listClients;
    }
}
