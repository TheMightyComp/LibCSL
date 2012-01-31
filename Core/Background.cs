using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LibCSL.Core
{
    public class Background
    {
        public Texture2D texture;
        public string name;

        public Background(string Name)
        {
            name = Name;
        }

        public void loadTexture(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Sprites/Backgrounds/" + name);
        }

    }
}
