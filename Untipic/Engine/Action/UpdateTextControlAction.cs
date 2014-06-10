using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Untipic.Engine.Action
{
    public class UpdateTextControlAction : IAction
    {
        public UpdateTextControlAction()
        {
            Visible = false;
            RePaint = true;
            IsToAll = true;
        }

        public UpdateTextControlAction(DrawingControl control) : this()
        {
            _control = control;
        }

        public string Text { get; set; }

        public Font Font { get; set; }

        public PointF Location { get; set; }

        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public bool Visible { get; set; }
        public bool RePaint { get; set; }
        public bool IsToAll { get; set; }
        public ActionType GetActionType()
        {
            return ActionType.UpdateTextControl;
        }

        public void Execute()
        {
            _control.UpdateText(Text, Font, Location);
        }

        private readonly DrawingControl _control;
    }
}
