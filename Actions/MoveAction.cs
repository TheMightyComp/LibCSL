using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCSL.Core;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LibCSL.Actions
{
    public class MoveAction : Actions.Action
    {
        public string actor;
        public Vector2 target;
        public string animation;
        public int time;
        private float ratio;
        private Vector2 origPos = Vector2.Zero;
        public int millis = 0;

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

        public void update(ref Actor actor, GameTime gameTime)
        {
            actor.curAnim = this.animation;
            millis += gameTime.ElapsedGameTime.Milliseconds;

            if (origPos == Vector2.Zero)
            {
                origPos = actor.coordinate;
            }
            if (millis >= time || (millis == 0 && origPos == Vector2.Zero))
            {
                actor.coordinate = target;
                actor.curAnim = "Idle";
            }
            else
            {
                ratio = (float)(millis) / (float)(time);;
                actor.coordinate = Vector2.Lerp(origPos, target, ratio);
                actor.coordinate.X = (float)((int)(actor.coordinate.X));
                actor.coordinate.Y = (float)((int)(actor.coordinate.Y));
            }
        }
    }
}
