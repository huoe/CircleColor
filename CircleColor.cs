using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using AnimatedGif;

namespace CircleColor
{
    public class CircleColor
    {
        public CircleColor(int length, int width, int turns, int n, int size, int randomSize, float minDistance, float maxDistance, byte alpha, List<Color> Lcolors)
        {
            this.Length = length;
            this.Width = width;
            this.Turns = turns;
            this.N = n;
            this.Size = size;
            this.RandomSize = randomSize;
            this.MinDistance = minDistance;
            this.MaxDistance = maxDistance;
            this.Alpha = alpha;
            this.LColors = Lcolors;
            this.CenterRect = new Rect(length / 2, width / 2);
        }
        /// <summary>
        /// 计算
        /// </summary>
        public void Caculate()
        {
            float round = 360 / this.Turns;
            for (float i = round; i <= 360; i += round)
            {
                AddOneTurns(i);
            }
        }
        void AddOneTurns(float angle, float size, float minDistance, float maxDistance, int n, List<Color> colors, byte alpha)
        {
            Random rd = new Random();
            Color color;
            for (int i = 0; i < n; i++)
            {
                color = colors[rd.Next(colors.Count)];  //随机分配一个颜色
                int magnitude = rd.Next(Convert.ToInt32(minDistance), Convert.ToInt32(maxDistance));
                oneCircles.Add(new OneCircle(new Polar(angle, magnitude), size, color, alpha));
            }
        }
        /// <summary>
        /// 内部添加每条线上的圆，引用自身参数
        /// </summary>
        void AddOneTurns(float angle)
        {
            Random rd = new Random();
            Color color;
            for (int i = 0; i < this.N; i++)
            {
                color = this.LColors[rd.Next(LColors.Count)];
                int magnitude = rd.Next(Convert.ToInt32(MinDistance), Convert.ToInt32(MaxDistance));
                float size = Size + rd.Next(RandomSize);
                oneCircles.Add(new OneCircle(new Polar(angle, magnitude), size, color, Alpha));
            }
        }
        //导出Png
        public void PrintToImg()
        {
            Console.WriteLine("绘图中......");

            Bitmap image = new Bitmap(this.Length, this.Width);
            Graphics g = Graphics.FromImage(image);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Color.Black);

            foreach (var one in oneCircles)
            {
                int size = Convert.ToInt32(one.Size) * 2;

                Bitmap circle = new Bitmap(size, size);
                Graphics Gcircle = Graphics.FromImage(circle);
                Gcircle.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                Gcircle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Gcircle.Clear(Color.Black);
                Gcircle.FillEllipse(new SolidBrush(Color.FromArgb(one.Alpha, one.Color)), 0, 0, one.Size, one.Size);
                int x = Convert.ToInt32(one.R.x + CenterRect.x - one.Size / 2);
                int y = Convert.ToInt32(one.R.y + CenterRect.y - one.Size / 2);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        image.SetPixel(x + i, y + j, ColorConvert.Screen(circle.GetPixel(i, j), image.GetPixel(x + i, y + j)));
                    }
                }
            }

            string name = DateTime.Now.ToString("dd-hh-mm-ss") + "-img.png";
            image.Save(name, System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine("绘图成功！");
        }
        /// <summary>
        /// 导出Gif
        /// </summary>
        /// <param name="time">每张关键帧的间隔时间</param>
        public void PrintToGif(int time = -1)
        {
            Console.WriteLine("绘图中......");

            string name = DateTime.Now.ToString("dd-hh-mm-ss") + "-img.gif";
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + name;
            AnimatedGifCreator ac = new AnimatedGifCreator(path);

            Bitmap image = new Bitmap(this.Length, this.Width);
            Graphics g = Graphics.FromImage(image);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Color.Black);
            ac.AddFrame(image);

            foreach (var one in oneCircles)
            {
                int size = Convert.ToInt32(one.Size) * 2;

                Bitmap circle = new Bitmap(size, size);
                Graphics Gcircle = Graphics.FromImage(circle);
                Gcircle.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                Gcircle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Gcircle.Clear(Color.Black);
                Gcircle.FillEllipse(new SolidBrush(Color.FromArgb(one.Alpha, one.Color)), 0, 0, one.Size, one.Size);
                int x = Convert.ToInt32(one.R.x + CenterRect.x - one.Size / 2);
                int y = Convert.ToInt32(one.R.y + CenterRect.y - one.Size / 2);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        image.SetPixel(x + i, y + j, ColorConvert.Screen(circle.GetPixel(i, j), image.GetPixel(x + i, y + j)));
                    }
                }

                ac.AddFrame(image);
            }

            Console.WriteLine("绘图成功！");
        }
        private Rect CenterRect { get; }
        private byte Alpha { get; }
        private float MinDistance { get; }
        private float MaxDistance { get; }
        private float Size { get; }
        private int RandomSize { get; }
        private int N { get; }
        private int Turns { get; }
        private int Length { get; }
        private int Width { get; }
        /// <summary>
        /// 存放Color的容器
        /// </summary>
        List<Color> LColors = new List<Color>();
        static List<OneCircle> oneCircles = new List<OneCircle>();
    }
    class OneCircle : Coordinate
    {
        OneCircle(float size, Color color, byte alpha)
        {
            this.size = size;
            this.color = color;
            this.alpha = alpha;
        }
        public OneCircle(Rect r, float size, Color color, byte alpha = 255) : this(size, color, alpha) => this.R = r;
        public OneCircle(Polar p, float size, Color color, byte alpha = 255) : this(size, color, alpha) => this.P = p;
        public OneCircle(Coordinate c, float size, Color color, byte alpha = 255) : this(size, color, alpha) => this.P = c.P;

        private float size;//圆半径
        private byte alpha;//圆透明度-默认完全不透明
        private Color color;//圆颜色
        public float Size { get => size; }
        public byte Alpha { get => alpha; }
        public Color Color { get => color; }
    }
}