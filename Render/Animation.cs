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
using LibCSL.Render;
using LibCSL.Util;


namespace LibCSL.Render
{
    class Animation
    {
        int curFrame;
        Rectangle frameRect;
        Texture2D spriteTex;
        AnimationMode animationMode;
        int frameRate;
        int elapsed;
        bool backwards = false;
        bool done = false;

        public Animation(int width, int height, Texture2D tex, AnimationMode am, int framerate)
        {
            curFrame = 0;
            frameRect = new Rectangle(0, 0, width, height);
            spriteTex = tex;
            animationMode = am;
            frameRate = framerate;
        }

        public void update(GameTime gameTime)
        {
            if (!done)
            {
                elapsed += gameTime.ElapsedGameTime.Milliseconds;

                if (elapsed >= frameRate)
                {
                    curFrame++;

                    if (curFrame > spriteTex.Width / frameRect.Width)
                    {
                        switch (animationMode)
                        {
                            case AnimationMode.Loop:
                                curFrame = 0;
                                break;
                            case AnimationMode.PingPong:
                                backwards = !backwards;
                                curFrame = 0;
                                break;
                            case AnimationMode.PlayOnce:
                                curFrame = spriteTex.Width / frameRect.Width;
                                done = true;
                                break;
                        }
                        
                    }

                    if (!backwards)
                        frameRect.X = curFrame * frameRect.Width;
                    else
                        frameRect.X = spriteTex.Width - curFrame * frameRect.Width;

                    elapsed = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 loc)
        {
            spriteBatch.Draw(spriteTex, loc, frameRect, Color.White);
        }
    }

    public enum AnimationMode
    {
        PlayOnce, PingPong, Loop,
    }
}
