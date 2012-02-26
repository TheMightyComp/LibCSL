using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCSL.Core;
using Microsoft.Xna;
using Microsoft.Xna.Framework;

namespace LibCSL.Actions
{
    public class MoveAction : Actions.Action
    {
        public string actor;
        public Vector2 target;
        public string animation;
        public int time;

        public override void parse(string curLine)
        {
            TextParser tp = new TextParser();
            List<string> words = tp.seperateWords(curLine);

            actor = words[1];

            string actorXPos = "";
            Vector2 curActorPos = Vector2.Zero;
            string actorYPos = "";

            actorXPos = words[2];
            actorXPos = actorXPos.Substring(1, actorXPos.Length - 2);
            actorYPos = words[3];
            actorYPos = actorYPos.Substring(0, actorYPos.Length - 1);

            target = new Vector2(float.Parse(actorXPos), float.Parse(actorYPos));

            animation = words[4];
            time = int.Parse(words[5]);

            actionType = ActionType.Move;

        }        
    }
}
