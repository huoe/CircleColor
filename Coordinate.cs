using System;

namespace CircleColor
{
    class CoordinateCovert
    {
        /// <summary>
        /// 将平面坐标转换为极坐标
        /// </summary>
        public static Polar RectToPolar(Rect rect)
        {
            Polar polar;

            polar.magnitude = Convert.ToSingle(Math.Sqrt(Math.Pow(rect.x, 2) + Math.Pow(rect.y, 2)));

            if (polar.magnitude == 0)
                polar.angle = 0;
            else
                polar.angle = Convert.ToSingle((180 / (4 * Math.Atan(1))) * Math.Atan2(rect.y, rect.x));

            return polar;
        }
        /// <summary>
        /// 将极坐标转换为平面坐标
        /// </summary>
        public static Rect PolarToRect(Polar polar)
        {
            Rect rect;
            rect.x = Convert.ToSingle(Math.Cos((Math.PI / 180) * polar.angle) * polar.magnitude);
            rect.y = Convert.ToSingle(Math.Sin((Math.PI / 180) * polar.angle) * polar.magnitude);

            return rect;
        }
    }
    /// <summary>
    /// 两种坐标集合体，可在一种坐标改变时更改另一种
    /// </summary>
    class Coordinate
    {
        private Rect r;
        private Polar p;
        public Rect R
        {
            get => r;
            set
            {
                if (value.x != r.x && value.y != r.y)
                {
                    r = value;
                    p = value.ToPolar();
                }
            }
        }
        public Polar P
        {
            get => p;
            set
            {
                if (value.angle != p.angle && value.magnitude != p.magnitude)
                {
                    p = value;
                    r = value.ToRect();
                }
            }
        }
    }
    /// <summary>
    /// 极坐标系
    /// </summary>
    struct Polar
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="angle">角度(角度制)</param>
        /// <param name="magnitude">与极点的距离</param>
        public Polar(float angle, float magnitude)
        {
            this.angle = angle;
            this.magnitude = magnitude;
        }
        /// <summary>
        /// 角度
        /// </summary>
        public float angle;
        /// <summary>
        /// 与极点的距离
        /// </summary>
        public float magnitude;
        public Rect ToRect() => CoordinateCovert.PolarToRect(this);

        public override string ToString() => $"({angle},{magnitude})";
    }
    /// <summary>
    /// 平面直角坐标系
    /// </summary>
    struct Rect
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        public Rect(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// 横坐标
        /// </summary>
        public float x;
        /// <summary>
        /// 纵坐标
        /// </summary>
        public float y;
        public Polar ToPolar() => CoordinateCovert.RectToPolar(this);

        public override string ToString() => $"({x},{y})";
    }
}