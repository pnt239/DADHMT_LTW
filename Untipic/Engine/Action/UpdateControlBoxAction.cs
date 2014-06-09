using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Untipic.Core;

namespace Untipic.Engine.Action
{
    public class UpdateControlBoxAction : IAction
    {
        public UpdateControlBoxAction(DrawingControl control)
        {
            _control = control;

            Visible = false;
            RePaint = true;
            IsToAll = true;
        }

        public ShapeType ShapeType { get; set; }

        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public bool ControlVisible { get; set; }

        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public bool Visible { get; set; }
        public bool RePaint { get; set; }
        public bool IsToAll { get; set; }

        public ActionType GetActionType()
        {
            return ActionType.UpdateControlBox;
        }

        public void Execute()
        {
            _control.UpdateControl(ShapeType, StartPoint, EndPoint);
            _control.Visible = ControlVisible;
        }

        private readonly DrawingControl _control;
    }
}
