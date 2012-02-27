using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCSL.Core;
using LibCSL.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LibCSL.Actions
{
    public class FadeAction : Actions.Action
    {
        public Color color;
        public int time;
        private int elapsed;

        public override void parse(string curLine)
        {
            TextParser tp = new TextParser();
            actionType = ActionType.Fade;

            color = ColorUtils.colorFromHexString(curLine.Substring(6, 6));
            color.A = 0;
            time = int.Parse(tp.seperateWords(curLine)[2]);
            Console.WriteLine("Parsed a fade! Color " + color.ToString());
            elapsed = 0;
        }

        public void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            int ratio = (int)((float)elapsed / (float)time * 255f);
            if (ratio > 255)
            {
                ratio = 255;
            }
            color.A = (byte)ratio;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D fadeTex)
        {
            spriteBatch.Draw(fadeTex, new Rectangle(0, 0, 800, 600), color);
        }
    }
}
