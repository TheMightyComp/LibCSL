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
            TextParser tp = new TextParser();
            List<string> words = tp.seperateWords(curLine);

            actor = words[1];


            if (words.Count == 4)
            {
                text = words[2].Substring(1, words[2].Length - 2);
            }

            else
            {
            

            for (int i = 2; i < words.Count; i++)
            {
                if (words[i].EndsWith(")"))
                {
                    text += words[i].Substring(0, words[i].Length - 1);
                    break;
                }

                if (words[i].StartsWith("("))
                    text += words[i].Substring(1) + " ";

                else
                    text += words[i] + " ";
            }
        }

            animation = words[words.Count - 1];
            actionType = ActionType.Speak;
        }

        public void Draw(SpriteFont font, Texture2D bgtex, SpriteBatch spriteBatch)
        {
            for (int i = 32; i < 768; i += 16)
            {
                spriteBatch.Draw(bgtex, new Rectangle(i, 440, 16, 128), Color.White);
            }

            float xlen = font.MeasureString(text).X;
            float height = font.MeasureString(text).Y;

            if (font.MeasureString(text).X > 736)
            {
            }
            else
            {
                spriteBatch.DrawString(font, text, new Vector2(400 - (int)xlen / 2, 504 - (int)height / 2), Color.Black);
            }
        }
    }
}
