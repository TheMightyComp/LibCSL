using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LibCSL.Util
{
    public static class ColorUtils
    {

        private static Dictionary<char, int> hexValuesAsInt = null;

        public static Color colorFromHexString(string hexCode)
        {
            if (hexValuesAsInt == null)
            {
                AssignHexValues();
            }


            if (hexCode.Length != 6)
                throw new FormatException("Error: Specified arguement is not a hex code!");

            Color colorToReturn = new Color();

            char[] charArray = hexCode.ToCharArray();

            for (int curColor = 0; curColor < 3; curColor++)
            {
                int hexAsInt = 0;

                for (int curDigit = 0; curDigit < 2; curDigit++)
                {
                    int curCharValue = hexValuesAsInt[charArray[curColor * 2 + curDigit]];
                    if (curDigit == 0)
                        hexAsInt += curCharValue * 16;
                    else
                        hexAsInt += curCharValue;
                }

                switch (curColor)
                {
                    case 0:
                        colorToReturn.R = (byte)hexAsInt;
                        break;
                    case 1:
                        colorToReturn.G = (byte)hexAsInt;
                        break;
                    case 2:
                        colorToReturn.B = (byte)hexAsInt;
                        break;
                    default:
                        Console.WriteLine("If you are reading this, Jared's hex converter is FUBAR.");
                        break;
                }

                hexAsInt = 0;
            }

            return colorToReturn;

        }

        private static void AssignHexValues()
        {
            hexValuesAsInt = new Dictionary<char, int>();
            hexValuesAsInt.Add('0', 0);
            hexValuesAsInt.Add('1', 1);
            hexValuesAsInt.Add('2', 2);
            hexValuesAsInt.Add('3', 3);
            hexValuesAsInt.Add('4', 4);
            hexValuesAsInt.Add('5', 5);
            hexValuesAsInt.Add('6', 6);
            hexValuesAsInt.Add('7', 7);
            hexValuesAsInt.Add('8', 8);
            hexValuesAsInt.Add('9', 9);
            hexValuesAsInt.Add('A', 10);
            hexValuesAsInt.Add('B', 11);
            hexValuesAsInt.Add('C', 12);
            hexValuesAsInt.Add('D', 13);
            hexValuesAsInt.Add('E', 14);
            hexValuesAsInt.Add('F', 15);
        }

        
    }
}
