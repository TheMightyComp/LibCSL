using System;
using System.Collections.Generic;
using System.Text;

public class TextParser
{
    public List<string> seperateWords(string line)
    {
        List<string> words = new List<string>();
        char[] charArray = line.ToCharArray();
        string curWord = "";

        foreach (char curChar in charArray)
        {
            if (curChar.Equals(' '))
            {
                words.Add(curWord);
                curWord = "";
            }
            else
                curWord += curChar;
        }

        words.Add(curWord);

        return words;
    }
}