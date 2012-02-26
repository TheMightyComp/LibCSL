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
using LibCSL.Util;


namespace LibCSL.Render
{
    public class CSLRenderer
    {
        private Scene scene;
        private int curEventID;
        private Event curEvent;
        Texture2D speechTex;
        SpriteFont font;

        KeyboardState lastKState;
        KeyboardState kState;
        GamePadState lastGState;
        GamePadState gState;
        int millis;
        bool done;
        public bool IsDone
        {
            get
            {
                return done;
            }
        }

        public void Update(GameTime gameTime)
        {
            lastKState = kState;
            kState = Keyboard.GetState();
            lastGState = gState;
            gState = GamePad.GetState(PlayerIndex.One);
     

            if (!done)
            {
                if (curEvent.finishedOn == FinishedID.onAction)
                {
                    if ((kState.IsKeyDown(Keys.Space) && lastKState.IsKeyUp(Keys.Space)) || (gState.IsButtonDown(Buttons.A) && lastGState.IsButtonUp(Buttons.A)))
                    {
                        curEventID++;
                        if (curEventID == scene.events.Count)
                        {
                            done = true;
                        }
                        else
                        {
                            curEvent = scene.events[curEventID];
                        }
                        Console.WriteLine("Finished event " + curEventID);
                    }
                }
                else
                {
                    millis += gameTime.ElapsedGameTime.Milliseconds;
                    Console.WriteLine("Millis = " + millis);

                    if (millis >= curEvent.time)
                    {
                        curEventID++;
                        if (curEventID == scene.events.Count)
                        {
                            done = true;
                        }
                        else
                        {
                            curEvent = scene.events[curEventID];
                        }
                        Console.WriteLine("Finished event " + curEventID);
                        millis = 0;
                    }
                }
            }

        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            spriteBatch.Draw(scene.background.texture, new Rectangle(0, 0, 800, 600), Color.White);
            
            foreach (KeyValuePair<String, Actor> curActor in scene.actors)
            {
                curActor.Value.Draw(spriteBatch);
            }

            foreach (Actions.Action curAction in curEvent.actions)
            {
                if (curAction.actionType == Actions.ActionType.Speak)
                {
                    Actions.SpeakAction speak = curAction as Actions.SpeakAction;
                    speak.Draw(font, speechTex, spriteBatch);
                }
            }

            spriteBatch.End();
        }

        public CSLRenderer(Scene scene, ContentManager Content)
        {
            this.scene = scene;
            curEventID = 0;
            scene.background.loadTexture(Content);

            foreach (KeyValuePair<String, Actor> curActor in scene.actors)
            {
                curActor.Value.loadTexture(Content);

            }

            speechTex = Content.Load<Texture2D>(Globals.getValue("LibCSLContentPath") + "Speech");
            font = Content.Load<SpriteFont>(Globals.getValue("LibCSLContentPath") + "Dialogue");

            curEvent = scene.events[curEventID];
            done = false;
            
        }
    }
}
