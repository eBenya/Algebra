using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace other_task
{
    class Straight_Line
    {
        enum param 
        {
            y,
            x,
            p
        };
        //Уравнение будет иметь вид: kx*y + ky*x + p = 0;
        //Класс "прямая" будет хранить параметры kx, ky, p;
        private double[] f;     // kx, ky, p;
        private int len;

        public double[] F { get => f; }
        public int Len { get => len; private set => len = value; }

        public Straight_Line(Point a1, Point a2)
        {
            if (a1.X != a2.X || a1.Y != a2.Y)
            {
                f = new double[(int)param.p+1];
                f[(int)param.x] = a1.Y - a2.Y;
                f[(int)param.y] = a2.X - a1.X;
                f[(int)param.p] = a1.X * a2.Y - a2.X * a1.Y;
                Len = f.Length;
            }
            else throw new Exception("Create a straight line - failed. This is the same point.");
        }
        public Straight_Line(double _x, double _y, double _p)
        {
            if (_x != _y)
            {
                f[(int)param.x] = _x;
                f[(int)param.y] = _y;
                f[(int)param.p] = _p;
                Len = f.Length;
            }
            else throw new Exception("Create a straight line - failed. Coefficient <x> equal coeffiecient <y>.");
        }

        /// <summary>
        /// Create new line, ortogonale current straight line, passing throught point <point>.
        /// </summary>
        /// <param name="point">The point throught which passing ortogonale line</param>
        /// <returns></returns>
        public Straight_Line OrtoFromPointLine(Point point)
        {
            Point p = new Point(f[(int)param.x], f[(int)param.y]);
            return new Straight_Line(p, point);
        }

        /// <summary>
        /// This method return equation straight line in string format.
        /// </summary>
        /// <returns></returns>
        public string GetEquationLine()
        {
            if (f[(int)param.x] != 0 && f[(int)param.y] != 0 && f[(int)param.p] != 0)
            {
                return $"{f[(int)param.y].ToString()}y" +
                       $"{(f[(int)param.x] > 0 ? "+" + f[(int)param.x].ToString() : f[(int)param.x].ToString())}x" +
                       $"{(f[(int)param.p] > 0 ? "+" + f[(int)param.p].ToString() : f[(int)param.p].ToString())}" +
                       "=0";
                //return ky.ToString() + "y" +
                //      (kx > 0 ? "+" + kx.ToString() : kx.ToString()) + "x" +
                //      (p > 0 ? "+" + p.ToString() : p.ToString()) + "=0";
            }
            else
            {
                if (f[(int)param.p] == 0)
                {
                    return $"{f[(int)param.y].ToString()}y" +
                           $"{(f[(int)param.x] > 0 ? "+" + f[(int)param.x].ToString() : f[(int)param.x].ToString())}x" +
                           "=0";
                }
                if (f[(int)param.x] == 0)
                {
                    return $"{f[(int)param.y].ToString()}y" +
                           $"{(f[(int)param.p] > 0 ? "+" + f[(int)param.p].ToString() : f[(int)param.p].ToString())}" +
                           "=0";
                }
                return $"{f[(int)param.x].ToString()}x" +
                       $"{(f[(int)param.p] > 0 ? "+" + f[(int)param.p].ToString() : f[(int)param.p].ToString())}" +
                       "=0";
            }            
        }

        /// <summary>
        /// Return point intersection of two lines.
        /// </summary>
        /// <param name="l2"></param>
        /// <returns></returns>
        public Point IntersectionPoint(Straight_Line l2)
        {
            if (this.len != l2.len)
            {
                throw new ArgumentException("Incorect paramentrs of lines");
            }
            var A = new Matrix(2, 2);
            var B = new Matrix(2, 1);

            for (int i = 0; i < this.len - 1; i++)
            {
                A[0, i] = this.f[i];
                A[1, i] = l2.f[i];
            }
            B[0, 0] = -this.f[2];
            B[1, 0] = -l2.f[2];
            if (A.CalcDeterminant() == 0)
            {
                return null;
            }
            Matrix invA = A.NewInvertMatrix();
            var X = invA*B;

            return new Point(X[1, 0], X[0, 0]);
        }
    }
}
