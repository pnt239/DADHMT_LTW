using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Engine.Action
{
    public enum ActionType
    {
        MouseMove = 0,
        AddUser,
        IdentifyUser,
        UpdateControlBox,
        AddVertex,
        RemoveUser,
        LoadControlBox,
        CreateShape,
        UpdateTextControl,
        CreateText
    }

    public interface IAction
    {
        int ReceiverId { get; set; }

        int SenderId { get; set; }

        bool Visible { get; set; }

        bool RePaint { get; set; }

        bool IsToAll { get; set; }

        ActionType GetActionType();

        //byte[] ToBytes();

        void Execute();
        //void UnExecute();
    }
}
