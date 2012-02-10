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
            char[] charArray;

            while (!(reader.EndOfStream))
            {
                
                curLine = reader.ReadLine();
                Console.WriteLine("Read line " + curLine);
                charArray = curLine.ToCharArray();
                int endChar = 0;
                key = "";
                value = "";

                for (int i = 0; i < charArray.Length; i++)
                {
                    if (charArray[i] == ' ')
                    {
                        endChar = i;
                        break;
                    }

                    key += charArray[i];
                }

                for (int i = endChar + 1; i < charArray.Length; i++)
                {
                    if (charArray[i] == ' ')
                        break;

                    value += charArray[i];
                }


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
