using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCSL.Core;
using LibCSL.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LibCSL.Actions
{
    public class SpeakAction : Actions.Action
    {
        public string actor;
        public string text;
        public string animation;



        public override void parse(string curLine)
        {
            int endPos = 0;

            actor = extractActor(curLine, out endPos);
            text = extractText(curLine, ref endPos);
            animation = extractAnimation(curLine, ref endPos);
            actionType = ActionType.Speak;

            Console.WriteLine("Parsed a speak action! " + actor + " will say '" + text + "' with animation " + animation);
        }

        private string extractAnimation(string curLine, ref int endPos)
        {
            bool foundAnim = false;
            endPos += 7;
            char[] charArray = curLine.Substring(endPos).ToCharArray();
            string anim = "";
            endPos = 0;

            while (!foundAnim)
            {
                if (endPos == charArray.Length)
                    foundAnim = true;
                else
                    anim += charArray[endPos].ToString();

                endPos++;
            }

            Console.WriteLine("Parsed speaker animation " + anim);
            return anim;
        }

        private string extractText(string curLine, ref int endPos)
        {
            bool foundText = false;
            char[] charArray = curLine.Substring(endPos).ToCharArray();
            string text = "";
            endPos += 1;

            while (!foundText)
            {
                if (charArray[endPos] == ')')
                    foundText = true;
                else
                    text += charArray[endPos].ToString();

                endPos++;
            }

            Console.WriteLine("Added spoken text: '" + text + "'");
            return text;
        }

        private string extractActor(string curLine, out int endPos)
        {
            bool foundCharacter = false;
            endPos = 0;
            char[] charArray = curLine.Substring(6).ToCharArray();
            string ActorName = "";

            while (!foundCharacter)
            {
                if (charArray[endPos] == ' ')
                    foundCharacter = true;
                else
                    ActorName += charArray[endPos].ToString();

                endPos++;
            }

            Console.WriteLine("Extracted Speaker Name " + ActorName);
            return ActorName;
        }

        public void loadContent(ContentManager Content)
        {
            
        }
    }
}
