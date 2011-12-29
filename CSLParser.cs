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
        public Scene parseCSL(string location)
        {
            return parseCSL(new FileStream(location, FileMode.Open));
        }

        public Scene parseCSL(FileStream location)
        {
            
            StreamReader file = new StreamReader(location);

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

                if (curLine.StartsWith("Declaration", false, null))
                {
                    Console.WriteLine("Found declaration statement!");
                }

                else if (curLine.StartsWith("Scene", false, null))
                {
                    Console.WriteLine("Found Keyword Scene!");
                    char[] charArray = curLine.ToCharArray(6, curLine.Length - 6);  //Get everything after the word Scene as a char array
                    int curChar = 0;
                    bool foundSpace = false;


                    #region Get the ID code
                    while (!foundSpace) //Keep cycling through until it finds whitespace
                    {
                        if (charArray[curChar].Equals(' '))
                        {
                            foundSpace = true;
                        }
                        else
                        {
                            id += charArray[curChar];
                            Console.WriteLine("Found a character in the ID! It is " + charArray[curChar]);
                        }
                        curChar++;
                    }

                    //If no ID is found...
                    if (id == "")
                    {
                        throw new IOException("Error: no ID found!");

                    }

                    #endregion

                    #region Get the English Name

                    foundSpace = false;
                    while (!foundSpace)
                    {
                        if ((curChar == charArray.Length) || (charArray[curChar].Equals(' ')) || (charArray[curChar].Equals('/') && (charArray[curChar + 1].Equals('/'))))
                        {
                            foundSpace = true;
                        }
                        else
                        {
                            englishName += charArray[curChar];
                            Console.WriteLine("Found a character in the name! It is " + charArray[curChar]);
                        }
                        curChar++;
                    }


                    #endregion

                    foundDeclaration = true;
                }

                else if ((curLine.StartsWith("//", false, null)) || (curLine == String.Empty))
                {
                    Console.WriteLine("Found Comment/Whitespace!");
                    Console.WriteLine(curLine);
                }

                else
                {
                    Console.WriteLine("Error: Unknown (Non-comment) keyword detected!");
                    Console.WriteLine("Line: " + curLine);

                }
            }

        }

        private void extractActorsAndBackground(StreamReader file, out Background background, out List<Actor> actors)
        {
            background = null;
            actors = new List<Actor>();
            string curline;
            char[] charArray;

            #region Find Background Declaration
            bool foundBackgroundDeclaration = false;

            while (!foundBackgroundDeclaration)
            {
                curline = file.ReadLine();
                if (curline.StartsWith("//") || curline == string.Empty)
                {
                    Console.WriteLine("Found Comment/Whitespace!");
                    Console.WriteLine(curline);
                }
                if (curline.StartsWith("Background"))
                {
                    Console.WriteLine("Found Background Declaration!");
                    Console.WriteLine(curline);
                    charArray = curline.ToCharArray(11, curline.Length - 11);

                    string backgroundName = "";
                    bool foundWhiteSpace = false;
                    int curChar = 0;

                    while (!foundWhiteSpace)
                    {
                        if ((curChar == charArray.Length) || (charArray[curChar].Equals(' ')) || (charArray[curChar].Equals('/') && (charArray[curChar + 1].Equals('/'))))
                        {
                            foundWhiteSpace = true;

                        }
                        else
                        {
                            backgroundName += charArray[curChar];
                            Console.WriteLine("Added character " + charArray[curChar] + " to BG name!");
                        }
                        curChar++;
                    }

                    foundBackgroundDeclaration = true;
                    Background newBack = new Background(backgroundName, null);
                    background = new Background(backgroundName, null);

                }
            }
            #endregion

            #region Find Actors

            bool foundAllActors = false;
            int foundActorCount = 0;
            List<string> actorNames = new List<string>();
            List<Vector2> coordinateNames = new List<Vector2>();


            while (!foundAllActors)
            {
                curline = file.ReadLine();

                if (curline.StartsWith("//") || curline == string.Empty)
                {
                    Console.WriteLine("Found Comment/Whitespace!");
                    Console.WriteLine(curline);
                }

                else if (curline.StartsWith("Actor"))
                {
                    #region Parse an Actor Declaration
                    Console.WriteLine("Actor declaration found!");
                    Console.WriteLine(curline);
                    charArray = curline.ToCharArray(6, curline.Length - 6);


                    string actorName = "";
                    string actorXPos = "";
                    Vector2 curActorPos = Vector2.Zero;
                    string actorYPos = "";
                    bool foundWhiteSpace = false;
                    int curChar = 0;

                    #region Get the Actor's Name
                    while (!foundWhiteSpace)
                    {
                        if ((curChar == charArray.Length) || (charArray[curChar].Equals(' ')) || (charArray[curChar].Equals('/') && (charArray[curChar + 1].Equals('/'))))
                        {
                            actorNames.Add(actorName);
                            Console.WriteLine("Added Actor " + actorName);
                            foundActorCount++;
                            foundWhiteSpace = true;
                        }
                        else
                        {
                            actorName += charArray[curChar];
                            Console.WriteLine("Added character " + charArray[curChar] + " to current actor name!");
                        }
                        curChar++;
                    }
                    #endregion

                    #region Get the Actors X coordinate

                    curChar++; //Skip parenthesis
                    foundWhiteSpace = false;



                    while (!foundWhiteSpace)
                    {
                        if (charArray[curChar] == ',')
                        {
                            curActorPos.X = (float)(int.Parse(actorXPos));
                            Console.WriteLine("Added X coordinate " + actorXPos + " to " + actorName);
                            foundActorCount++;
                            foundWhiteSpace = true;
                        }
                        else
                        {
                            actorXPos += charArray[curChar];
                            Console.WriteLine("Added character " + charArray[curChar] + " to current actor X Coordinate!");
                        }
                        curChar++;
                    }



                    #endregion

                    #region Get the Actors Y coordinate

                    foundWhiteSpace = false;
                    curChar++;  //Skip the blank space

                    while (!foundWhiteSpace)
                    {
                        if (charArray[curChar] == ')')
                        {
                            curActorPos.Y = (float)(int.Parse(actorYPos));
                            Console.WriteLine("Added Y coordinate " + actorYPos + " to " + actorName);
                            foundActorCount++;
                            foundWhiteSpace = true;
                        }
                        else
                        {
                            actorYPos += charArray[curChar];
                            Console.WriteLine("Added character " + charArray[curChar] + " to current actor Y Coordinate!");
                        }
                        curChar++;
                    }

                    #endregion

                    #region Create the actor
                    Actor newActor = new Actor(actorName, null, curActorPos);
                    actors.Add(newActor);
                    Console.WriteLine("Created " + actorName + " at coordinate " + curActorPos.ToString());
                    #endregion

                    #endregion
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

                if (curLine.StartsWith(" ") || curLine.StartsWith("//"))
                {
                    Console.WriteLine("Found comment!");
                    Console.WriteLine(curLine);
                }

                else if (curLine.StartsWith("Event"))
                {
                    Console.WriteLine("Found event declaration!");
                    Console.WriteLine(curLine);
                    string id;
                    FinishedID finishedOn;
                    int time;
                    List<LibCSL.Actions.Action> actions;
                    Event foundEvent;

                    extractEventHeaderFromLine(curLine, out id, out finishedOn, out time);

                    Console.WriteLine("\n\nFinished extracting header!");
                    Console.WriteLine("The id is " + id);
                    Console.WriteLine("The finishedID is " + finishedOn.ToString());
                    Console.WriteLine("The time (if it exists) is " + time.ToString());

                    extractActions(file, out actions);

                    Console.WriteLine("\n\n\n\n\n");
                    Console.WriteLine("Found all actions! There are " + actions.Count);

                    Event newEvent = new Event(id, finishedOn, actions);
                    Console.WriteLine("Added new event! " + id + finishedOn.ToString() + actions.Count.ToString());
                    events.Add(newEvent);

                }

                else if (curLine.StartsWith("ENDCSL"))
                {
                    foundAllEvents = true;
                    Console.WriteLine("Found all events! There are " + events.Count);
                }
            }

           
        }

        private void extractEventHeaderFromLine(string curLine, out string id, out FinishedID finishedOn, out int time)
        {
            char[] charArray = curLine.ToCharArray(6, curLine.Length - 6);
            int curChar = 0;
            id = "";
            finishedOn = FinishedID.onAction;
            time = 0;

            #region Extract the event ID

            bool foundID = false;
            id = "";
            while (!foundID)
            {
                if (charArray[curChar] == ' ')
                {
                    Console.WriteLine("Found whitespace and the entire event name:");
                    Console.WriteLine(id);
                    foundID = true;
                }
                else
                {
                    id += charArray[curChar];
                    Console.WriteLine("Added character '" + charArray[curChar] + "' to event name!");
                }

                curChar++;
            }

            #endregion

            #region Extract the finish identifier

            curChar += 9; //Skip the word "finished"

            bool foundFinishedOn = false;
            string finishedAsString = "";



            while (!foundFinishedOn)
            {
                if (curChar == charArray.Length || charArray[curChar] == ' ')
                {
                    switch (finishedAsString)
                    {
                        case "afterTime":
                            finishedOn = FinishedID.afterTime;
                            Console.WriteLine("Now looking for a finishing time too...");
                            break;
                        case "onAction":
                            finishedOn = FinishedID.onAction;
                            break;
                        default:
                            throw new Exception("Unexpected FinishID!  Did you spell something wrong?");
                            break;
                    }

                    Console.WriteLine("Found whitespace and the entire finished ID:");
                    Console.WriteLine(finishedOn);
                    foundFinishedOn = true;
                }

                else
                {
                    finishedAsString += charArray[curChar];
                    Console.WriteLine("Added char '" + charArray[curChar] + "'to event finished ID");
                }

                curChar++;

            }

            #endregion

            #region Extract the time if the Finish ID is "afterTime"

            if (finishedOn == FinishedID.afterTime)
            {
                bool foundTime = false;
                string timeString = "";
                while (!foundTime)
                {
                    if (curChar == charArray.Length)
                    {
                        Console.WriteLine("Ran out of chars!");
                        time = int.Parse(timeString);
                        Console.WriteLine("Found time! It is " + time.ToString());
                        foundTime = true;
                    }

                    else
                    {
                        timeString += charArray[curChar];
                        Console.WriteLine("Found and added character " + charArray[curChar] + " to the time!");
                    }

                    curChar++;
                }
            }

            #endregion
        }

        private void extractActions(StreamReader file, out List<LibCSL.Actions.Action> actions)
        {
            string curLine;
            bool foundAllEvents = false;
            actions = new List<Actions.Action>();
            int actionCount = 0;

            while (!foundAllEvents)
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
                    foundAllEvents = true;
                

                
            }
        }

 

        private bool isNextLineNonActor(char nextChar)
        {

            if (nextChar != 'A' && nextChar != '/' && nextChar == 'E')
            {
                Console.WriteLine("Found an E - I assume it is EndDeclaration.  Moving on.");
                return true;
            }
            else
            {
                return false;
            }
        }

       
       
    }
}
