using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LibCSL.Util
{
    public static class Globals
    {
        private static Dictionary<String, String> globals;

        public static void initialize(string path)
        {
            StreamReader reader = new StreamReader(path);
            string key = "";
            string value = "";
            globals = new Dictionary<string, string>();
            string curLine;
            TextParser tp = new TextParser();

            while (!(reader.EndOfStream))
            {
                curLine = reader.ReadLine();
                if (tp.seperateWords(curLine).Count < 2)
                    break;
                key = tp.seperateWords(curLine)[0];
                value = tp.seperateWords(curLine)[1];

                globals.Add(key, value);
                Console.WriteLine("Added key " + key + " with value " + value);

            }
        }


        public static string getValue(string key)
        {
            string value = "";
            globals.TryGetValue(key, out value);
            return value;

        }

    }
}
