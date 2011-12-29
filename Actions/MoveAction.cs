using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCSL.Core;
using Microsoft.Xna;
using Microsoft.Xna.Framework;

namespace LibCSL.Actions
{
    class MoveAction : Actions.Action
    {
        public string actor;
        public Vector2 target;
        public string animation;
        public int time;

        public override void parse(string curLine)
        {
            int curPos = 5;
            actor = extractActor(curLine, ref curPos);
            target = extractTarget(curLine, ref curPos);
            animation = extractAnimation(curLine, ref curPos);
            time = extractTime(curLine, ref curPos);

            actionType = ActionType.Move;

        }

        private string extractActor(string curLine, ref int curPos)
        {
            bool foundActor = false;
            char[] charArray = curLine.ToCharArray(curPos, curLine.Length - curPos);
            int curChar = 0;
            string actorName = "";

            while (!foundActor)
            {
                if (charArray[curChar] == ' ')
                    foundActor = true;
                else
                    actorName += charArray[curChar].ToString();

                curPos++;
                curChar++;
            }

            Console.WriteLine("Found mover name " + actorName);
            return actorName;
        }

        private Vector2 extractTarget(string curLine, ref int curPos)
        {
            string xCoord = "";
            string yCoord = "";

            bool foundXCoord = false;
            bool foundYCoord = false;
            curPos++;


            char[] charArray = curLine.ToCharArray(curPos, curLine.Length - curPos);
            int curChar = 0;

            while (!foundXCoord)
            {
                if (charArray[curChar] == ',')
                    foundXCoord = true;
                else
                    xCoord += charArray[curChar].ToString();

                curChar++;
                curPos++;
            }

            curPos++;

            while (!foundYCoord)
            {
                if (charArray[curChar] == ')')
                    foundYCoord = true;
                else
                    yCoord += charArray[curChar].ToString();

                curChar++;
                curPos++;
            }

            Vector2 returnVec = new Vector2(float.Parse(xCoord), float.Parse(yCoord));
            Console.WriteLine("Found move position " + returnVec.ToString());
            return returnVec;
        }

        private string extractAnimation(string curLine, ref int curPos)
        {
            bool foundAnimation = false;
            int curChar = 0;
            char[] charArray = curLine.ToCharArray(curPos, curLine.Length - curPos);
            curPos++;
            string anim = "";

            while (!foundAnimation)
            {
                if (charArray[curChar] == ' ')
                    foundAnimation = true;
                else
                    anim += charArray[curChar].ToString();

                curPos++;
                curChar++;
            }

            Console.WriteLine("Found move animation " + anim);
            return anim;
        }

        private int extractTime(string curLine, ref int curPos)
        {
            Console.WriteLine("extractTime called!");
            bool foundTime = false;

            int curChar = 0;
            char[] charArray = curLine.ToCharArray(curPos - 1, curLine.Length - curPos + 1);
            string timeString = "";

            while (!foundTime)
            {
                if (curChar == charArray.Length || charArray[curChar] == ' ')
                {
                    foundTime = true;
                }
                else
                {
                    timeString += charArray[curChar].ToString();
                }
                curChar++;
            }

            Console.WriteLine("Found move time " + timeString);
            return int.Parse(timeString);
        }
        
    }
}
