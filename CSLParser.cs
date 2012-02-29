using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LibCSL.Core;
using LibCSL.Actions;
using Microsoft.Xna.Framework;

namespace LibCSL
{
    public class CSLParser
    {
        TextParser seperator;

        public Scene parseCSL(string location)
        {
            return parseCSL(new FileStream(location, FileMode.Open));
        }

        public Scene parseCSL(FileStream location)
        {
            
            StreamReader file = new StreamReader(location);
            seperator = new TextParser();

            string id;
            string englishName;
            Background background;
            List<Actor> actors;
            List<Event> events = new List<Event>();

            extractHeader(file, out id, out englishName);
            extractActorsAndBackground(file, out background, out actors);
            extractEvents(file, ref events);
            Console.WriteLine("Found all events at parseCSL method! There are " + events.Count);


            return new Scene(id, englishName, events, actors, background);
        }

        private void extractHeader(StreamReader file, out string id, out string englishName)
        {
            bool foundDeclaration = false;
            id = "";
            englishName = "";

            while (!foundDeclaration)
            {
                string curLine = file.ReadLine();

                if (curLine.StartsWith("Scene", false, null))
                {
                    List<string> words = seperator.seperateWords(curLine);
                    id = words[1];
                    englishName = words[2];

                    foundDeclaration = true;
                }
            }

        }

        private void extractActorsAndBackground(StreamReader file, out Background background, out List<Actor> actors)
        {
            background = null;
            actors = new List<Actor>();
            string curline;

            #region Find Background Declaration

            bool foundBackgroundDeclaration = false;

            while (!foundBackgroundDeclaration)
            {
                curline = file.ReadLine();
             
                if (curline.StartsWith("Background"))
                {
                    foundBackgroundDeclaration = true;
                    background = new Background(seperator.seperateWords(curline)[1]);
                }
            }

            #endregion

            #region Find Actor Declarations

            bool foundAllActors = false;
            List<string> actorNames = new List<string>();

            while (!foundAllActors)
            {
                curline = file.ReadLine();

                if (curline.StartsWith("Actor"))
                {
                    List<String> words = seperator.seperateWords(curline);

                    string actorName = words[1];
                    string actorXPos = "";
                    Vector2 curActorPos = Vector2.Zero;
                    string actorYPos = "";

                    actorXPos = words[2];
                    actorXPos = actorXPos.Substring(1, actorXPos.Length - 2);
                    actorYPos = words[3];
                    actorYPos = actorYPos.Substring(0, actorYPos.Length - 1);
                    curActorPos = new Vector2(float.Parse(actorXPos), float.Parse(actorYPos));

                    Actor newActor = new Actor(actorName, null, curActorPos);
                    actors.Add(newActor);
                }

                if (isNextLineNonActor((char)file.Peek())) //Check to see if all the actors are done with.  If so, exit loop
                    foundAllActors = true;

            }

            #endregion
        }

        private void extractEvents(StreamReader file, ref List<Event> events)
        {
            bool foundAllEvents = false;
            string curLine;
            
            while (!foundAllEvents)
            {
                curLine = file.ReadLine();

                if (curLine.StartsWith("Event"))
                {
                    string id;
                    FinishedID finishedOn;
                    int time;
                    List<LibCSL.Actions.Action> actions;

                    extractEventHeaderFromLine(curLine, out id, out finishedOn, out time);

                    extractActions(file, out actions);

                    Event newEvent = new Event(finishedOn, actions, time);
                    events.Add(newEvent);

                }

                else if (curLine.StartsWith("ENDCSL"))
                {
                    foundAllEvents = true;
                }
            }

           
        }

        private void extractEventHeaderFromLine(string curLine, out string id, out FinishedID finishedOn, out int time)
        {
            id = "";
            finishedOn = FinishedID.onAction;
            time = 0;
            List<string> words = seperator.seperateWords(curLine);

            #region Extract the finish identifier

            string finishedAsString = words[2];

            switch (finishedAsString)
            {
                case "onAction":
                    finishedOn = FinishedID.onAction;
                    break;
                case "afterTime":
                    finishedOn = FinishedID.afterTime;
                    break;
                default:
                    throw new InvalidDataException("Unrecognized finished ID: " + finishedAsString);
                    break;
            }

            #endregion

            if (finishedOn == FinishedID.afterTime)

                time = int.Parse(words[3]);
        }

        private void extractActions(StreamReader file, out List<LibCSL.Actions.Action> actions)
        {
            string curLine;
            bool foundAllActions = false;
            actions = new List<Actions.Action>();
            int actionCount = 0;

            while (!foundAllActions)
            {
                curLine = file.ReadLine();

                if (string.IsNullOrWhiteSpace(curLine))
                    continue;

                if (curLine.StartsWith("Fade"))
                {
                    actions.Add(new FadeAction());
                    actions[actionCount].parse(curLine);
                    actionCount++;
                }

                if (curLine.StartsWith("Speak"))
                {
                    actions.Add(new SpeakAction());
                    actions[actionCount].parse(curLine);
                    actionCount++;
                }

                if (curLine.StartsWith("Move"))
                {
                    actions.Add(new MoveAction());
                    actions[actionCount].parse(curLine);
                    actionCount++;
                }

                if (curLine.StartsWith("EndEvent"))
                    foundAllActions = true;

            }
        }

        private bool isNextLineNonActor(char nextChar)
        {

            if (nextChar != 'A' && nextChar != '/' && nextChar == 'E')
                return true;
            else
                return false;
        }
    }
}
