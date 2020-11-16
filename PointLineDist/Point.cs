using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace other_task
{
    class Point
    {
        private double x;
        private double y;

        public Point(double _x, double _y)
        {
            X = _x;
            y = _y;
        }

        public double X { get => x; private set => x = value; }
        public double Y { get => y; private set => y = value; }

        public static double LenghtFromCentre(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
        public static double LenghtFromCentre(Point p)
        {
            return Math.Sqrt(p.x * p.x + p.y * p.y);
        }

    }
}
