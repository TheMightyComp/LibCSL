using System;
using System.Collections.Generic;
using System.Linq;


namespace LibCSL.Actions
{
    public class Action
    {
        public virtual void execute() //Children of Action define their own execution methods
        {
        }

        public virtual void parse(string curLine)
        {
        }

        public ActionType actionType; //Allows in-code reference of which action a child of Action represents
    }

    public enum ActionType
    {
        Speak,
        Move,
        Fade
    }
}
