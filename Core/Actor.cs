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
using LibCSL.Render;


namespace LibCSL.Core
{
    public class Actor
    {
        public string name;
        public Vector2 coordinate;
        public Texture2D texture;
        Dictionary<String, Animation> anims;
        public String curAnim = "Idle";
        

        public Actor(string Name, string Path, Vector2 Coordinate)
        {
            name = Name;
            coordinate = Coordinate;
            anims = new Dictionary<string, Animation>();
        }

        public void Update(GameTime gameTime)
        {
            anims[curAnim].update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            anims[curAnim].Draw(spriteBatch, coordinate);
        }

        public void addAnim(string path, AnimationMode am, ContentManager Content, int frames)
        {
           Texture2D tex = Content.Load<Texture2D>(Globals.getValue("StandardSpritePath") + name + "/" + path);
           anims.Add(path, new Animation(tex.Width / frames, tex.Height, tex, am, (int.Parse(Globals.getValue("DefaultFrameRate")))));
        }

        public bool hasAnim(string ani)
        {
            return anims.ContainsKey(ani);
        }
    }
}
