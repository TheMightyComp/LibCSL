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
using LibCSL;
using LibCSL.Core;


namespace LibCSL.Render
{
    public class CSLRenderer
    {
        private Scene scene;
        private int curEvent;

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            spriteBatch.Draw(scene.background.texture, new Rectangle(0, 0, 800, 600), Color.White);
            
            foreach (KeyValuePair<String, Actor> curActor in scene.actors)
            {
                curActor.Value.Draw(spriteBatch);
            
            }
            spriteBatch.End();
        }

        public CSLRenderer(Scene scene, ContentManager Content)
        {
            this.scene = scene;
            curEvent = 0;
            scene.background.loadTexture(Content);
            foreach (KeyValuePair<String, Actor> curActor in scene.actors)
            {
                curActor.Value.loadTexture(Content);

            }
        }
    }
}
