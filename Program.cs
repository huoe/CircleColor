using System;
using System.Collections.Generic;
using System.Drawing;

namespace CircleColor
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Color> c = new List<Color>();
            c.Add(Color.FromArgb(118, 128, 211));
            c.Add(Color.FromArgb(190, 93, 181));

            CircleColor circle = new CircleColor(500, 500, 50, 20, 2, 12, 70, 220, 255, c);
            circle.Caculate();
            circle.PrintToImg();
        }
    }
}
