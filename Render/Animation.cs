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

        public Animation(int width, int height, Texture2D tex, AnimationMode am)
        {
            curFrame = 0;
            frameRect = new Rectangle(0, 0, width, height);
            spriteTex = tex;
            animationMode = am;
        }
    }

    public enum AnimationMode
    {
        PlayOnce, PingPong, Loop,
    }
}
