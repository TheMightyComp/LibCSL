using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCSL.Core
{
    public class Scene
    {
        public string id;  //The scenes unique identifier
        public string englishName;  //A descriptive name for the scene - can be non-unique
        public Dictionary<string, Event> events;  //A dictionary of all the events - the keys are each event's ID.
        public Dictionary<string, Actor> actors;  //A dictionary of all the actors - keys are matched by name
        public Background background;


        public Scene(string ID, string EnglishName, List<Event> Events, List<Actor> Actors, Background BGround)
        {
            id = ID;
            englishName = EnglishName;
            Console.WriteLine("Recieved event list! There are " + Events.Count);

            int eventCount = 0;
            events = new Dictionary<string, Event>();

            foreach (Event curEvent in Events)  //Assign each event without an ID its numerical order in the scene
            {
                curEvent.id = eventCount.ToString();

                Console.WriteLine("Parsing event " + curEvent.id);
                eventCount++;

                events.Add(curEvent.id, curEvent);  //Add curEvent to the dictionary, using its ID as a key.
            }
            

            actors = new Dictionary<string, Actor>();

            foreach (Actor actor in Actors)
            {
                actors.Add(actor.name, actor);
            }
             

            background = BGround;
        }
    }



}


