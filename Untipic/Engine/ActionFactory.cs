using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Sockets;
using System.Text;
using Untipic.Core;
using Untipic.Engine.Action;

namespace Untipic.Engine
{
    public class ActionFactory
    {
        public ActionFactory(AppManament manager)
        {
            _manager = manager;
        }

        public void SendAction(IAction action, NetworkStream stream)
        {
            // ActionType | Reciever UserId | ...
            WriteInt((int)action.GetActionType(), stream);
            WriteInt(action.SenderId, stream);
            WriteInt(action.ReceiverId, stream);

            switch (action.GetActionType())
            {
                case ActionType.MouseMove:
                    SendMouseMoveAction((MouseMoveAction)action, stream);
                    break;
                case ActionType.AddUser:
                    SendAddUserAction((AddUserAction)action, stream);
                    break;
                case ActionType.IdentifyUser:
                    break;
                case ActionType.LoadControlBox:
                    SendLoadControlBoxAction((LoadControlBoxAction) action, stream);
                    break;
                case ActionType.UpdateControlBox:
                    SendUpdateControlBoxAction((UpdateControlBoxAction) action, stream);
                    break;
                case ActionType.AddVertex:
                    SendAddVertexAction((AddVertexAction) action, stream);
                    break;
                case ActionType.RemoveUser:
                    SendRemoveUserAction((RemoveUserAction) action, stream);
                    break;
                case ActionType.CreateShape:
                    SendCreateShapeAction((CreateShapeAction) action, stream);
                    break;
                case ActionType.UpdateTextControl:
                    SendUpdateTextControlAction((UpdateTextControlAction) action, stream);
                    break;
                case ActionType.CreateText:
                    SendCreateTextAction((CreateTextAction) action, stream);
                    break;
            }
        }

        public IAction GetAction(ActionType type, NetworkStream stream)
        {
            // Get user ID
            IAction action = null;
            int senderId = ReadInt(stream);
            int receiveId = ReadInt(stream);

            switch (type)
            {
                case ActionType.MouseMove:
                    action = GetMouseMoveAction(senderId, stream);
                    break;
                case ActionType.AddUser:
                    action = GetAddUserAction(/*senderId, */stream);
                    break;
                case ActionType.IdentifyUser:
                    action = GetIdentifyAction(/*senderId, stream*/);
                    break;
                case ActionType.LoadControlBox:
                    action = GetLoadControlBoxAction(senderId, stream);
                    break;
                case ActionType.UpdateControlBox:
                    action = GetUpdateControlBoxAction(senderId, stream);
                    break;
                case ActionType.AddVertex:
                    action = GetAddVertexAction(senderId, stream);
                    break;
                case ActionType.RemoveUser:
                    action = GetRemoveUserAction(/*senderId, */stream);
                    break;
                case ActionType.CreateShape:
                    action = GetCreateShapeAction(senderId, stream);
                    break;
                case ActionType.UpdateTextControl:
                    action = GetUpdateTextControlAction(senderId, stream);
                    break;
                case ActionType.CreateText:
                    action = GetCreateTextAction(senderId, stream);
                    break;
            }

            if (action == null) return null;

            action.SenderId = senderId;
            action.ReceiverId = receiveId;

            return action;
        }

        private MouseMoveAction GetMouseMoveAction(int senderId, NetworkStream stream)
        {
            var action = new MouseMoveAction();
            action.User = _manager.ClientList[senderId];

            // Get X
            int x = ReadInt(stream);

            // Get Y
            int y = ReadInt(stream);

            // Set location
            action.Location = new Point(x, y);

            return action;
        }

        private AddUserAction GetAddUserAction(/*int senderId, */NetworkStream stream)
        {
            // Get user ID
            int id = ReadInt(stream);

            // Get user name
            string name = ReadString(stream);

            var action = new AddUserAction(_manager.ClientList, _manager.SendList);
            action.User = new UserInfo(null, id, _manager.ShapeDrawer);
            action.User.Name = name;

            return action;
        }

        private IdentifyAction GetIdentifyAction(/*int senderId, NetworkStream stream*/)
        {
            var action = new IdentifyAction(_manager.Client);

            return action;
        }

        private LoadControlBoxAction GetLoadControlBoxAction(int senderId, NetworkStream stream)
        {
            var action = new LoadControlBoxAction(_manager.ClientList[senderId].ControlBox);
            action.ShapeType = (ShapeType)ReadInt(stream);
            var sx = ReadInt(stream);
            var sy = ReadInt(stream);
            action.StartPoint = new Point(sx, sy);

            var ex = ReadInt(stream);
            var ey = ReadInt(stream);
            action.EndPoint = new Point(ex, ey);

            return action;
        }

        private UpdateControlBoxAction GetUpdateControlBoxAction(int senderId, NetworkStream stream)
        {
            // ShapeType | Visible | Start Point | End Point
            var action = new UpdateControlBoxAction(_manager.ClientList[senderId].ControlBox);
            action.ShapeType = (ShapeType)ReadInt(stream);
            action.ControlVisible = ReadBool(stream);
            var sx = ReadInt(stream);
            var sy = ReadInt(stream);
            action.StartPoint = new Point(sx, sy);

            var ex = ReadInt(stream);
            var ey = ReadInt(stream);
            action.EndPoint = new Point(ex, ey);

            return action;
        }

        private AddVertexAction GetAddVertexAction(int senderId, NetworkStream stream)
        {
            // Location
            var action = new AddVertexAction(_manager.ClientList[senderId].ControlBox);
            var sx = ReadInt(stream);
            var sy = ReadInt(stream);
            action.Location = new Point(sx, sy);

            return action;
        }

        private RemoveUserAction GetRemoveUserAction(/*int senderId, */NetworkStream stream)
        {
            
            // Id
            int id = ReadInt(stream);

            var action = new RemoveUserAction(_manager.ClientList, _manager.SendList) {User = _manager.ClientList[id]};

            return action;
        }

        private CreateShapeAction GetCreateShapeAction(int senderId, NetworkStream stream)
        {
            // Shape type | Location | Size | Outline Color | Outline width | Outline dash | Fill Color | Vetices

            // Shape type
            var shaptype = (ShapeType)ReadInt(stream);
            var shape = ShapeFactory.CreateShape(shaptype);
            // Location
            shape.Location = ReadPointF(stream);
            // Size
            shape.Size = ReadSizeF(stream);
            // Outline Color
            shape.OutlineColor = ReadColor(stream);
            // Outline width
            shape.OutlineWidth = ReadFloat(stream);
            // Outline dash
            shape.OutlineDash = (DashStyle)ReadInt(stream);
            // Fill Color
            shape.FillColor = ReadColor(stream);

            shape.Vertices.Clear();
            if (shaptype != ShapeType.Ellipse)
            {
                // vertices count
                int vcount = ReadInt(stream);
                for (int i = 0; i < vcount; i++)
                {
                    var v = ReadVertex(stream);
                    shape.Vertices.Add(v);
                }
            }

            if (shape.GetShapeType() == ShapeType.Polygon)
                ((Polygon) shape).IsClosedFigure = true;

            shape.UserId = senderId;

            return new CreateShapeAction(_manager.Page, shape);
        }

        private UpdateTextControlAction GetUpdateTextControlAction(int senderId, NetworkStream stream)
        {
            var obj = new UpdateTextControlAction(_manager.ClientList[senderId].ControlBox);
            obj.Text = ReadString(stream);
            obj.Font = ReadFont(stream);
            obj.Location = ReadPointF(stream);

            return obj;
        }

        private CreateTextAction GetCreateTextAction(int senderId, NetworkStream stream)
        {
            // Location | Size | Text | Font | Color

            // Shape type
            var text = new TextObject();
            // Location
            text.Location = ReadPointF(stream);
            // Size
            text.Size = ReadSizeF(stream);
            // Outline width
            text.Text = ReadString(stream);
            // Outline dash
            text.Font = ReadFont(stream);
            // Fill Color
            text.Color = ReadColor(stream);

            text.UserId = senderId;

            return new CreateTextAction(_manager.Page, text);
        }

        private void SendMouseMoveAction(MouseMoveAction action, NetworkStream stream)
        {
            // x | y
            WriteInt(action.Location.X, stream);
            WriteInt(action.Location.Y, stream);
        }

        private void SendAddUserAction(AddUserAction action, NetworkStream stream)
        {
            WriteInt(action.User.Id, stream);
            WriteString(action.User.Name, stream);
        }

        private void SendLoadControlBoxAction(LoadControlBoxAction action, NetworkStream stream)
        {
            // ShapeType | start point | end point
            WriteInt((int)action.ShapeType, stream);
            WriteInt(action.StartPoint.X, stream);
            WriteInt(action.StartPoint.Y, stream);
            WriteInt(action.EndPoint.X, stream);
            WriteInt(action.EndPoint.Y, stream);
        }

        private void SendUpdateControlBoxAction(UpdateControlBoxAction action, NetworkStream stream)
        {
            // ShapeType | Visible | start point | end point
            WriteInt((int) action.ShapeType, stream);
            WriteBool(action.ControlVisible, stream);
            WriteInt(action.StartPoint.X, stream);
            WriteInt(action.StartPoint.Y, stream);
            WriteInt(action.EndPoint.X, stream);
            WriteInt(action.EndPoint.Y, stream);
        }

        private void SendAddVertexAction(AddVertexAction action, NetworkStream stream)
        {
            WriteInt(action.Location.X, stream);
            WriteInt(action.Location.Y, stream);
        }

        private void SendRemoveUserAction(RemoveUserAction action, NetworkStream stream)
        {
            // Id
            WriteInt(action.User.Id, stream);
        }

        private void SendCreateShapeAction(CreateShapeAction action, NetworkStream stream)
        {
            // Shape type | Location | Size | Outline Color | Outline width | Outline dash | Fill Color | Vetices
            
            // Shape type
            WriteInt((int) action.Shape.GetShapeType(), stream);
            // Location
            WritePointF(action.Shape.Location, stream);
            // Size
            WriteSizeF(action.Shape.Size, stream);
            // Outline Color
            WriteColor(action.Shape.OutlineColor, stream);
            // Outline width
            WriteFloat(action.Shape.OutlineWidth, stream);
            // Outline dash
            WriteInt((int) action.Shape.OutlineDash, stream);
            // Fill Color
            WriteColor(action.Shape.FillColor, stream);

            if (action.Shape.GetShapeType() != ShapeType.Ellipse)
            {
                // vertices count
                WriteInt(action.Shape.Vertices.Count, stream);
                foreach (var vertex in action.Shape.Vertices)
                    WriteVertex(vertex, stream);
            }
        }

        private void SendUpdateTextControlAction(UpdateTextControlAction action, NetworkStream stream)
        {
            // string | font | location
            WriteString(action.Text, stream);
            WriteFont(action.Font, stream);
            WritePointF(action.Location, stream);
        }

        private void SendCreateTextAction(CreateTextAction action, NetworkStream stream)
        {
            // Location | Size | Text | Font | Color

            // Location
            WritePointF(action.Text.Location, stream);
            // Size
            WriteSizeF(action.Text.Size, stream);
            // Outline Color
            WriteString(action.Text.Text, stream);
            // Outline width
            WriteFont(action.Text.Font, stream);
            // Fill Color
            WriteColor(action.Text.Color, stream);
        }

        private void WriteBool(bool b, NetworkStream stream)
        {
            WriteInt(b ? 1 : 0, stream);
        }

        private void WriteInt(int i, NetworkStream stream)
        {
            byte[] buffer = BitConverter.GetBytes(i);
            stream.Write(buffer, 0, 4);
            stream.Flush();
        }

        private void WriteString(string str, NetworkStream stream)
        {
            byte[] strBuffer = Encoding.Unicode.GetBytes(str);
            byte[] buffer = BitConverter.GetBytes(strBuffer.Length);
            // write string lenght
            stream.Write(buffer, 0, 4);
            stream.Flush();
            // write string data
            stream.Write(strBuffer, 0, strBuffer.Length);
            stream.Flush();
        }

        private void WriteFloat(float f, NetworkStream stream)
        {
            byte[] buffer = BitConverter.GetBytes(f);
            stream.Write(buffer, 0, 4);
            stream.Flush();
        }

        private void WritePointF(PointF p, NetworkStream stream)
        {
            WriteFloat(p.X, stream);
            WriteFloat(p.Y, stream);
        }

        private void WriteSizeF(SizeF s, NetworkStream stream)
        {
            WriteFloat(s.Width, stream);
            WriteFloat(s.Height, stream);
        }

        private void WriteVertex(IVertex v, NetworkStream stream)
        {
            WriteFloat(v.X, stream);
            WriteFloat(v.Y, stream);
        }

        private void WriteColor(Color c, NetworkStream stream)
        {
            stream.WriteByte(c.A);
            stream.WriteByte(c.R);
            stream.WriteByte(c.G);
            stream.WriteByte(c.B);
        }

        private void WriteFont(Font font, NetworkStream stream)
        {
            WriteString(font.FontFamily.Name, stream);
            WriteFloat(font.Size, stream);
            WriteInt((int) font.Style, stream);
            WriteInt((int) font.Unit, stream);
        }

        private bool ReadBool(NetworkStream stream)
        {
            var i = ReadInt(stream);
            if (i == 1)
                return true;
            return false;
        }

        private int ReadInt(NetworkStream stream)
        {
            var buffer = new byte[4];
            int nbyte = stream.Read(buffer, 0, 4);
            if (nbyte == 0) return 0;

            return BitConverter.ToInt32(buffer, 0);
        }

        private string ReadString(NetworkStream stream)
        {
            //Read the command's MetaData size.
            int metaDataSize = ReadInt(stream);

            //Read the command's Meta data.
            var buffer = new byte[metaDataSize];
            int nbyte = stream.Read(buffer, 0, metaDataSize);
            if (nbyte == 0)
                return "";

            return Encoding.Unicode.GetString(buffer);
        }

        private float ReadFloat(NetworkStream stream)
        {
            var buffer = new byte[4];
            int nbyte = stream.Read(buffer, 0, 4);
            if (nbyte == 0) return 0;

            return BitConverter.ToSingle(buffer, 0);
        }

        private PointF ReadPointF(NetworkStream stream)
        {
            var x = ReadFloat(stream);
            var y = ReadFloat(stream);
            return new PointF(x, y);
        }

        private SizeF ReadSizeF(NetworkStream stream)
        {
            var w = ReadFloat(stream);
            var h = ReadFloat(stream);
            return new SizeF(w, h);
        }

        private IVertex ReadVertex(NetworkStream stream)
        {
            var x = ReadFloat(stream);
            var y = ReadFloat(stream);
            return new Vertex(x, y);
        }

        private Color ReadColor(NetworkStream stream)
        {
            var a = stream.ReadByte();
            var r = stream.ReadByte();
            var g = stream.ReadByte();
            var b = stream.ReadByte();

            return Color.FromArgb(a, r, g, b);
        }

        private Font ReadFont(NetworkStream stream)
        {
            string name = ReadString(stream);
            var size = ReadFloat(stream);
            var style = (FontStyle)ReadInt(stream);
            var unit = (GraphicsUnit)ReadInt(stream);

            return new Font(name, size, style, unit);
        }

        private AppManament _manager;
    }
}
