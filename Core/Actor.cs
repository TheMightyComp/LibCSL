using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace LibCSL.Core
{
    public class Actor
    {
        public string name;
        //public string path;
        public Vector2 coordinate;
        //TODO: add texture2d voodoo

        public Actor(string Name, string Path, Vector2 Coordinate)
        {
            name = Name;
            coordinate = Coordinate;
            //path = Path;
        }
    }
}
