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
using LibCSL.Util;


namespace LibCSL.Core
{
    public class Actor
    {
        public string name;
        public Vector2 coordinate;
        public Texture2D texture;

        public Actor(string Name, string Path, Vector2 Coordinate)
        {
            name = Name;
            coordinate = Coordinate;
        }

        public void loadTexture(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(Globals.getValue("StandardSpritePath") + name);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, coordinate, Color.White);
        }
    }
}
