using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Untipic.Core;

namespace Untipic.Engine.Action
{
    public class CreateTextAction : IAction
    {
        public CreateTextAction(TextObject text)
        {
            Text = text;

            Visible = true;
            RePaint = true;
            IsToAll = true;
        }

        public CreateTextAction(Page page, TextObject text)
            : this(text)
        {
            Page = page;
        }


        public Page Page { get; set; }

        public TextObject Text { get; set; }

        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public bool Visible { get; set; }
        public bool RePaint { get; set; }
        public bool IsToAll { get; set; }
        public ActionType GetActionType()
        {
            return ActionType.CreateText;
        }

        public void Execute()
        {
            Text.UserId = SenderId;
            Page.AddDrawingObject(Text);
        }
    }
}
