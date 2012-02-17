using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCSL.Core
{
    public class Event
    {
        public FinishedID finishedOn;  //When the event finishes
        public List<Actions.Action> actions;  //A list of all the actions in the events
        public int time;

        public Event (FinishedID FinishedOn, List<Actions.Action> Actions, int Time)
        {
            finishedOn = FinishedOn;
            actions = Actions;
            time = Time;
        }

    }

    public enum FinishedID
    {
        onAction,
        afterTime
    }

}
