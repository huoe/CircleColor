using System;
using System.Drawing;

namespace CircleColor
{
    class ColorConvert
    {
        /// <summary>
        /// PS中正片叠底
        /// </summary>
        public static Color Multiply(Color c1, Color c2)
        {
            int R = (int)c1.R * (int)c2.R / 255;
            int G = (int)c1.G * (int)c2.G / 255;
            int B = (int)c1.B * (int)c2.B / 255;
            return Color.FromArgb(R, G, B);
        }
        /// <summary>
        /// PS中滤色处理
        /// </summary>
        public static Color Screen(Color c1, Color c2)
        {
            return Invert(Multiply(Invert(c1), Invert(c2)));
        }
        /// <summary>
        /// 反色处理
        /// </summary>
        public static Color Invert(Color c)
        {
            int R = 255 - c.R;
            int G = 255 - c.G;
            int B = 255 - c.B;
            return Color.FromArgb(R, G, B);
        }
    }
}