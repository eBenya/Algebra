using other_task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * Рассчет расстояния от точки до прямой, заданной двумя разными точками
 **/
namespace Lenght_from_point
{
    class Program
    {
        static void Main(string[] args)
        {
            Point a = new Point(3, 5);
            Point b = new Point(4, 4);
            double[,] g = new double[5, 4] { { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, };


            Console.WriteLine(g.GetUpperBound(1));


            Point point = new Point(0, 1);

            Straight_Line line = new Straight_Line(a, b);
            Straight_Line ortoLine = line.OrtoFromPointLine(point);

            double[,] costomA = new double[2, 2];
            double[,] expandA = new double[3, 2];

            #region TEST region
            double[,] one = new double[1, 1] { { 1 } };
            Matrix oneMat = new Matrix(1, 1, one);
            Console.WriteLine(oneMat.CheckSquare);

            Matrix matrix = new Matrix(5, 4, g);
            Console.WriteLine(matrix.CheckSquare);
            var tmatrix = matrix.NewTransposeMatrix();
            tmatrix = tmatrix.MatrixWithoutColumn(4);
            tmatrix = tmatrix.MatrixWithoutRow(2);

            double[,] dA1 = new double[3, 3] { { 1, 2, 3 }, { 4, 5, 4 }, { 3, 2, 1 } };
            Matrix A1 = new Matrix(3, 3, dA1);
            double[,] dA2 = new double[3, 2] { { 1, 2 }, { 4, 5 }, { 3, 2 } };
            Matrix A2 = new Matrix(3, 2, dA2);
            double[,] dA3 = new double[3, 4] { { 1, 2, 3, 4 }, { 4, 5, 5, 4 }, { 3, 2, 1, 0 } };
            Matrix A3 = new Matrix(3, 4, dA3);
            double[,] dA4 = new double[4, 3] { { 1, 2, 3 }, { 4, 5, 4 }, { 3, 2, 1 }, { 6, 7, 8 } };
            Matrix A4 = new Matrix(4, 3, dA4);
            var sumA12 = A1 + A1;
            var difA12 = A1 - A1;
            var mulA12 = A1 * A2;
            var mulA13 = A1 * A3;
            var mulA34 = A3 * A4;
            #endregion

            Console.WriteLine(line.GetEquationLine());
            Console.WriteLine(ortoLine.GetEquationLine());

            Point intersectPoint = line.IntersectionPoint(ortoLine);
            Console.WriteLine($"X={intersectPoint.X}; Y={intersectPoint.Y}");

            double distance = Point.LenghtFromCentre(point.X - intersectPoint.X, point.Y - intersectPoint.Y);
            Console.WriteLine($"Distence from point to line = {distance};");
        }
    }
}
