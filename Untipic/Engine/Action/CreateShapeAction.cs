using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Engine.Action
{
    public class CreateShapeAction : IAction
    {
        public CreateShapeAction(Core.ShapeBase shape)
        {
            Shape = shape;

            Visible = true;
            RePaint = true;
            IsToAll = true;
        }

        public CreateShapeAction(Core.Page page, Core.ShapeBase shape)
            : this(shape)
        {
            Page = page;
        }

        public Core.Page Page { get; set; }

        public Core.ShapeBase Shape { get; set; }

        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public bool Visible { get; set; }
        public bool RePaint { get; set; }
        public bool IsToAll { get; set; }
        public ActionType GetActionType()
        {
            return ActionType.CreateShape;
        }

        public void Execute()
        {
            Shape.UserId = SenderId;
            Page.AddDrawingObject(Shape);
        }
    }
}
