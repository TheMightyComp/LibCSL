using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCSL.Core
{
    public class Event
    {
        public string id;  //The events unique identifier
        public FinishedID finishedOn;  //When the event finishes
        public List<Action> actions;  //A list of all the actions in the events

        public Event (string ID, FinishedID FinishedOn, List<Action> Actions)
        {
            id = ID;
            finishedOn = FinishedOn;
            actions = Actions;
        }

    }

    public enum FinishedID
    {
        onAction,
        afterTime
    }

}
