﻿#region Copyright (c) 2013 Pham Ngoc Thanh, https://github.com/panoti/DADHMT_LTW/
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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Untipic.Network;
using Untipic.Core;
using Untipic.Core.EventArguments;
using Untipic.Engine.Action;

namespace Untipic.Engine
{
    public class UserInfoEventArgs : EventArgs
    {
        public UserInfoEventArgs(UserInfo user)
        {
            User = user;
        }

        public UserInfo User { get; set; }
    }

    public delegate void UserInfoEventHandler(Object sender, UserInfoEventArgs e);

    public class AppManament
    {
        public AppManament()
        {
            _actionFactory = new ActionFactory(this);
            _clientList = new Dictionary<int, UserInfo>();
            _sendList = new Dictionary<int, Queue<IAction>>();

            _redoList = new Stack<IDrawingObject>();

            _id = -1;

            _isWorking = false;
            _isServer = false;
        }

        public event ClientConnectingEventHandler UserAdmiting = null;
        public event UserInfoEventHandler UserConnected = null;
        public event UserInfoEventHandler UserDisconnected = null;

        public event UserInfoEventHandler UserAdded = null;
        public event UserInfoEventHandler UserRemoved = null;

        public event EventHandler Disconnected = null;

        public event EventHandler RePaint = null;

        public Dictionary<int, UserInfo> ClientList
        {
            get { return _clientList; }
        }

        public Dictionary<int, Queue<IAction>> SendList { get { return _sendList; } }

        public Visualization.ShapeDrawer ShapeDrawer
        {
            get { return _shapeDrawer; }
            set { _shapeDrawer = value; }
        }

        internal Client Client {get { return _client; }}

        public Page Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public void SetPage(Page page)
        {
            _page = page;
        }

        public void CreateServer(IPAddress ip)
        {
            DisconnectAll();

            _server = new Server(ip, Port);
            _server.ClientDisconnected += Server_ClientDisconnected;
            _server.ClientConnected += Server_ClientConnected;
            _server.ClientConnecting += Server_ClientConnecting;
            _server.DataReceived += ClientServer_DataReceived;
            _server.DataSent += ClientServer_DataSent;

            _server.Listen();
            _clientList.Add(0, new UserInfo(null, 0, _shapeDrawer));
            _clientList[0].Name = "Server";

            _sendList.Add(0, new Queue<IAction>());

            _id = 0;

            _isWorking = true;
            _isServer = true;
        }

        public void CreateClient(string ipServer)
        {
            DisconnectAll();

            _client = new Client();
            _client.DataReceived += ClientServer_DataReceived;
            _client.DataSent += ClientServer_DataSent;
            _client.DisconnectedFromServer += Client_DisconnectedFromServer;
            _client.Connect(ipServer, Port);

            _isWorking = true;
            _isServer = false;
        }

        public void DisconnectAll()
        {
            if (_isWorking)
            {
                if (_isServer)
                {
                    DisconnectAllClient();
                    _server.Disconnect();
                }
                else
                {
                    _client.Disconnect();
                }
            }
        }

        public void SendMouseMove(Point location)
        {
            if (_id < 0)
                return;

            var action = new MouseMoveAction();
            action.Location = location;
            action.User = _clientList[_id];

            SendAction(action);
        }

        public void LoadControlBox(DrawingControl control)
        {
            var action = new LoadControlBoxAction();
            action.ShapeType = control.ReviewShape.GetShapeType();
            action.StartPoint = control.StartPoint;
            action.EndPoint = control.EndPoint;

            SendAction(action);
        }

        public void SendControlBox(DrawingControl control)
        {
            var action = new UpdateControlBoxAction(control);
            action.ShapeType = control.ReviewShape.GetShapeType();
            action.ControlVisible = control.Visible;
            action.StartPoint = control.StartPoint;
            action.EndPoint = control.EndPoint;

            SendAction(action);
        }

        public void SendTextBox(TextObject textObject)
        {
            var action = new UpdateTextControlAction();
            action.Text = textObject.Text;
            action.Font = textObject.Font;
            action.Location = textObject.Location;

            SendAction(action);
        }

        public void AddVertexControlBox(DrawingControl control, Point point)
        {
            var action = new AddVertexAction(control);
            action.Location = point;

            SendAction(action);
        }

        /// <summary>
        /// Process when draw is finish
        /// </summary>
        /// <param name="obj">The object.</param>
        public void CreateObject(IDrawingObject obj)
        {
            IAction action = null;
            if (obj.GetObjectType() == DrawingObjectType.Shape)
            {
                action = new CreateShapeAction((ShapeBase)obj);
            }
            else if (obj.GetObjectType() == DrawingObjectType.Text)
            {
                var text = obj as TextObject;
                if (text != null)
                    action = new UpdateTextControlAction()
                    {
                        Text = "",
                        Font = text.Font,
                        Location = text.Location
                    };
            }

            SendAction(action);

            if (obj.GetObjectType() == DrawingObjectType.Text)
            {
                action = new CreateTextAction((TextObject)obj);
                SendAction(action);
            }

            _page.AddDrawingObject(obj);
        }

        public void SavePage(Stream stream)
        {
            using (var bin = new BinaryWriter(stream))
            {
                // write info of page
                bin.Write(_page.Size.Width);
                bin.Write(_page.Size.Height);
                bin.Write((int)_page.Unit);
                bin.Write(_page.Resolution);

                // Write number of shape
                bin.Write(_page.DrawingObjects.Count);
                foreach (var obj in _page.DrawingObjects)
                {
                    Util.SaveDrawingObject(bin, obj);
                }
            }
        }

        public void OpenPage(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            using (var bin = new BinaryReader(stream))
            {
                //
                var w = bin.ReadSingle();
                var h = bin.ReadSingle();
                var u = (MessureUnit)bin.ReadInt32();
                var r = bin.ReadSingle();

                _page.Size = new SizeF(w, h);
                _page.Unit = u;
                _page.Resolution = r;

                // Write number of shape
                int count = bin.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    var obj = Util.ReadDrawingObject(bin);
                    _page.DrawingObjects.Add(obj);
                }
            }
        }

        public void Updo()
        {
            int idx = _page.DrawingObjects.Count - 1;
            if (idx < 0)
                return;

            var obj = _page.DrawingObjects[idx];
            _redoList.Push(obj);

            _page.DrawingObjects.RemoveAt(idx);
        }

        public void Redo()
        {
            if (_redoList.Count == 0)
                return;

            var obj = _redoList.Pop();
            _page.DrawingObjects.Add(obj);
        }

        public void DrawPointer(Graphics g)
        {
            foreach (var user in _clientList)
                if (user.Value.Id != _id)
                    DrawMouse(g, user.Value.MouseLocation);
        }

        public void DrawControlBox(Graphics g)
        {
            foreach (var user in _clientList)
                if (user.Value.Id != _id && user.Value.ControlBox.Visible)
                    user.Value.ControlBox.Draw(g);
        }

        private void Server_ClientConnecting(object sender, ClientConnectingEventArgs e)
        {
            // Ask for allowing client
            if (UserAdmiting != null)
                UserAdmiting(this, e);
        }

        private void Server_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            lock (this)
            {
                var user = new UserInfo(e.Client, e.Id, _shapeDrawer);
                _clientList.Add(e.Id, user);
                _sendList.Add(e.Id, new Queue<IAction>());

                SendIdUser(e.Id);
                SendUserList(e.Id);

                var action = new AddUserAction();
                action.User = user;
                action.SenderId = _id;
                SendActionBroadCast(action, e.Id);

                if (UserConnected != null)
                    UserConnected(this, new UserInfoEventArgs(user));
            }
        }

        private void Server_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            lock (this)
            {
                if (UserDisconnected != null)
                    UserDisconnected(this, new UserInfoEventArgs(_clientList[e.ClientId]));

                SendActionBroadCast(new RemoveUserAction { User = _clientList[e.ClientId] }, e.ClientId);

                _clientList.Remove(e.ClientId);
            }
        }

        private void ClientServer_DataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                _semaphoreRecv.WaitOne();
                IAction action = _actionFactory.GetAction((ActionType) e.Signal, e.Stream);
                action.Execute();

                if (action.RePaint)
                    OnRePaint(EventArgs.Empty);

                if (_isServer && action.IsToAll)
                    SendActionBroadCast(action, action.SenderId);

                if (action.GetActionType() == ActionType.AddUser)
                {
                    var addact = (AddUserAction)action;
                    if (addact.User.Id != _client.Id)
                        OnUserAdded(new UserInfoEventArgs(addact.User));
                }
                else if (action.GetActionType() == ActionType.RemoveUser)
                {
                    var addact = (RemoveUserAction)action;
                    OnUserRemoved(new UserInfoEventArgs(addact.User));
                }
                else if (action.GetActionType() == ActionType.IdentifyUser)
                {
                    // Sender is server
                    // so id will be ReceiverId
                    _id = action.ReceiverId;
                }
                else if (action.GetActionType() == ActionType.MouseMove && _clientList[action.SenderId].ControlBox.ReviewShape != null)
                {
                    if (_clientList[action.SenderId].ControlBox.ReviewShape.GetShapeType() == Core.ShapeType.Polygon)
                        _clientList[action.SenderId].ControlBox.EndPoint = ((MouseMoveAction) action).Location;
                }

            }
            finally
            {
                _semaphoreRecv.Release();
            }
        }

        private void ClientServer_DataSent(object sender, DataSentEventArgs e)
        {
            try
            {
                var action = _sendList[e.ClientId].Dequeue();
                _actionFactory.SendAction(action, e.Stream);
            }
            catch
            {}
        }

        void Client_DisconnectedFromServer(object sender, ClientDisconnectedEventArgs eventArgs)
        {
            _clientList.Clear();
            _sendList.Clear();

            OnDisconnected(EventArgs.Empty);
        }

        private void OnUserAdded(UserInfoEventArgs e)
        {
            if (UserAdded != null)
            {
                var target = UserAdded.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(UserAdded, new object[] { this, e });
                else
                    UserAdded(this, e);
            }
        }

        private void OnUserRemoved(UserInfoEventArgs e)
        {
            if (UserRemoved != null)
            {
                var target = UserRemoved.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(UserRemoved, new object[] { this, e });
                else
                    UserRemoved(this, e);
            } 
        }

        private void OnDisconnected(EventArgs e)
        {
            if (Disconnected != null)
            {
                var target = Disconnected.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(Disconnected, new object[] { this, e });
                else
                    Disconnected(this, e);
            }
        }

        private void OnRePaint(EventArgs e)
        {
            if (RePaint != null)
            {
                var target = RePaint.Target as Control;
                if (target != null && target.InvokeRequired)
                    target.Invoke(RePaint, new object[] { this, e });
                else
                    RePaint(this, e);
            }
        }

        private void DisconnectAllClient()
        {
            if (ClientList.Count == 0)
                return;

            foreach (var user in ClientList)
                user.Value.Disconnect();

            ClientList.Clear();
        }

        private void SendActionBroadCast(IAction action, int except = -1)
        {
            lock (this)
            {
                foreach (var user in _clientList)
                    if (user.Value.Id != except && user.Value.Id != 0)
                    {
                        action.ReceiverId = user.Value.Id;
                        _sendList[user.Value.Id].Enqueue(action);
                    }

                if (_isServer)
                    _server.SendDataBroadCast();
            }
        }

        private void SendActionToTarget(IAction action)
        {
            lock (this)
            {
                _sendList[action.ReceiverId].Enqueue(action);
                if (_isServer)
                    _server.SendData(action.ReceiverId);
                else
                    _client.SendData(action.ReceiverId);
            }
        }

        private void SendAction(IAction action)
        {
            if (_id < 0)
                return;

            action.SenderId = _id;

            if (_isServer && action.IsToAll)
            {
                SendActionBroadCast(action);
            }
            else
            {
                if (!_isServer) action.ReceiverId = 0;
                SendActionToTarget(action);
            }
            
        }

        /// <summary>
        /// Sends the user list to user having userId.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void SendUserList(int userId)
        {
            foreach (var user in _clientList)
            {
                var action = new AddUserAction();
                action.User = user.Value;
                action.ReceiverId = userId;
                action.SenderId = _id;

                SendAction(action);
            }
        }

        private void SendIdUser(int userId)
        {
            var action = new IdentifyAction(_client);
            action.ReceiverId = userId;
            action.SenderId = _id;

            SendAction(action);
        }

        private void DrawMouse(Graphics g, Point p)
        {
            var rec = new Rectangle(p.X - 2, p.Y - 2, 5, 5);
            using (var b = new SolidBrush(Color.Red))
                g.FillEllipse(b, rec);
        }

        private const int Port = 12345;

        private Server _server;
        private Client _client;
        private readonly Dictionary<int, UserInfo> _clientList;
        private Dictionary<int, Queue<IAction>> _sendList;
        private int _id;
        private bool _isWorking;
        private bool _isServer;
        private readonly System.Threading.Semaphore _semaphoreRecv = new System.Threading.Semaphore(1, 1);

        private ActionFactory _actionFactory;
        private Visualization.ShapeDrawer _shapeDrawer;
        private Page _page;
        private Stack<IDrawingObject> _redoList;
    }
}
