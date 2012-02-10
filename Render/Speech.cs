using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCSL;
using LibCSL.Actions;
using LibCSL.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LibCSL.Render
{
    class Speech
    {
        SpeakAction action;

        public Speech(SpeakAction action)
        {
            this.action = action;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
