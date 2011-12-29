using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCSL.Core;
using LibCSL.Util;
using Microsoft.Xna.Framework;

namespace LibCSL.Actions
{
    public class FadeAction : Actions.Action
    {
        public Color color;

        public override void parse(string curLine)
        {
            actionType = ActionType.Fade;
            color = ColorUtils.colorFromHexString(curLine.Substring(6, 6));
            Console.WriteLine("Parsed a fade! Color " + color.ToString());
        }
    }
}
