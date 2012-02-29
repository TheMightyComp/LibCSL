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
using LibCSL.Actions;


namespace LibCSL.Render
{
    public class CSLRenderer
    {
        private Scene scene;
        private int curEventID;
        private Event curEvent;
        Texture2D speechTex;
        Texture2D fadeTex;
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
                #region Event change code
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
                #endregion

                #region Action updates
                foreach (Actions.Action curAct in curEvent.actions)
                {
                    if (curAct.actionType == ActionType.Move)
                    {
                        MoveAction move = curAct as MoveAction;
                        Actor act = scene.getActor(move.actor);
                        move.update(ref act, gameTime);
                    }
                    else if (curAct.actionType == ActionType.Fade)
                    {
                        FadeAction fade = curAct as FadeAction;
                        fade.Update(gameTime);
                    }
                    
                }
                #endregion

                foreach (Actor a in scene.actors.Values)
                {
                    a.Update(gameTime);
                }
            }

        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            
            spriteBatch.Draw(scene.background.texture, new Rectangle(0, 0, 800, 600), Color.White);
            
            foreach (KeyValuePair<String, Actor> curActor in scene.actors)
            {
                curActor.Value.Draw(spriteBatch);
            }

            foreach (Actions.Action curAction in curEvent.actions)
            {
                if (curAction.actionType == ActionType.Speak)
                {
                    SpeakAction speak = curAction as SpeakAction;
                    speak.Draw(font, speechTex, spriteBatch);
                }
                else if (curAction.actionType == ActionType.Fade)
                {
                    FadeAction fade = curAction as FadeAction;
                    fade.Draw(spriteBatch, fadeTex);
                }
                //Move modifies actor positions - no draw call needed
            }

            spriteBatch.End();
        }

        public CSLRenderer(Scene scene, ContentManager Content)
        {
            this.scene = scene;
            curEventID = 0;
            scene.background.loadTexture(Content);

            /* foreach (KeyValuePair<String, Actor> curActor in scene.actors)
            {
                curActor.Value.loadTexture(Content);
            } */

            fadeTex = Content.Load<Texture2D>(Globals.getValue("LibCSLContentPath") + "Fade");
            speechTex = Content.Load<Texture2D>(Globals.getValue("LibCSLContentPath") + "Speech");
            font = Content.Load<SpriteFont>(Globals.getValue("LibCSLContentPath") + "Dialogue");
            foreach (Event e in scene.events)
            {
                foreach (Actions.Action a in e.actions)
                {
                    if (a.actionType == ActionType.Move)
                    {
                        MoveAction m = a as MoveAction;
                        if (!scene.actors[m.actor].hasAnim(m.animation))
                        {
                            scene.actors[m.actor].addAnim(m.animation, AnimationMode.Loop, Content, 4);
                        }
                    }
                }

            }

            curEvent = scene.events[curEventID];
            done = false;

            curEvent = scene.events[curEventID];
            done = false;
            
        }

        public CSLRenderer(string name, ContentManager Content)
        {
            CSLParser cp = new CSLParser();

            this.scene = cp.parseCSL(Globals.getValue("CutscenePath") + name);
            curEventID = 0;
            scene.background.loadTexture(Content);

            /* foreach (KeyValuePair<String, Actor> curActor in scene.actors)
            {
                curActor.Value.loadTexture(Content);
            } */

            foreach (Event e in scene.events)
            {
                foreach (Actions.Action a in e.actions)
                {
                    if (a.actionType == ActionType.Move)
                    {
                        MoveAction m = a as MoveAction;
                        if (!scene.actors[m.actor].hasAnim(m.animation))
                        {
                            scene.actors[m.actor].addAnim(m.animation, AnimationMode.Loop, Content, 4);
                        }
                    }
                }

            }

            fadeTex = Content.Load<Texture2D>(Globals.getValue("LibCSLContentPath") + "Fade");
            speechTex = Content.Load<Texture2D>(Globals.getValue("LibCSLContentPath") + "Speech");
            font = Content.Load<SpriteFont>(Globals.getValue("LibCSLContentPath") + "Dialogue");

            curEvent = scene.events[curEventID];
            done = false;
        }
    }
}
