using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace other_task
{
    class Matrix
    {
        private double[,] data;

        private int column; public int Column { get => column; }      //число столбцов    (j)   
        private int row; public int Row { get => row; }               //число строк       (i)
        private double determinant;

        public Matrix(int row, int column)
        {
            if (column > 0 || row > 0)
            {
                this.column = column;
                this.row = row;
                determinant = double.NaN;
                this.data = new double[row, column];
            }
            //throw new Exception("All elements must be greather or equal to 0 and at least once not equal to 0;");
        }
        public Matrix(int row, int column, double[,] data) : this(row, column)
        {
            this.data = data;
        }   //create matrix and filling her 

        //Action delegate for data this matrix
        private void ActionFunctForData(Action<int, int> action)
        {
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    action(r, c);   //строка-столбец
                }
            }
        }
        public bool CheckSquare { get => column == row; }

        public Matrix NewTransposeMatrix()
        {
            var tMatrix = new Matrix(this.column, this.row);            //new matrix with reversed rows and columns
            tMatrix.ActionFunctForData((i, j) => tMatrix[i, j] = this[j, i]);
            return tMatrix;
        }

        //Удаление столбца матрицы
        public Matrix MatrixWithoutColumn(int col)
        {
            if (col < 0 || col >= column)
            {
                throw new ArgumentException("Invalid column argument value.");
            }
            var newMatrix = new Matrix(row, column - 1);
            //перебор
            //Если в исходной матрице колона меньше параметра, то копируем как есть(i),
            //Иначе копируем через одну(i+1)
            newMatrix.ActionFunctForData((i, j) => newMatrix[i, j] = (j < col) ? this[i, j] : this[i, j + 1]);
            return newMatrix;
        }
        //Удаление строки матрицы
        public Matrix MatrixWithoutRow(int row)
        {
            if (row < 0 || row >= this.row)
            {
                throw new ArgumentException("Invalid column argument value.");
            }
            var newMatrix = new Matrix(this.row - 1, this.column);
            //перебор
            //Если в исходной матрице колона меньше параметра, то копируем как есть(i),
            //Иначе копируем через одну(i+1)
            newMatrix.ActionFunctForData((i, j) => newMatrix[i, j] = (i < row) ? this[i, j] : this[i + 1, j]);
            return newMatrix;
        }

        //Пробуем вычислить определитель
        public double CalcDeterminant(int dRow = 0)
        {
            if (!double.IsNaN(this.determinant))
            {
                return this.determinant;
            }
            if (!this.CheckSquare)
            {
                throw new InvalidOperationException("matrix must be is square for calculated determinant");
            }
            if (row == 1) return this[0, 0];
            //if (row == 2) return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0]; //выход №2

            double res = 0;
            for (int i = 0; i < this.row; i++)
            {
                res += (i % 2 == 0 ? 1 : -1) * this[dRow, i] * this.MatrixWithoutColumn(i).MatrixWithoutRow(dRow).CalcDeterminant();
            }
            this.determinant = res;
            return res;
        }

        private double CalcMinorMatrix(int i, int j)    //Рассчет минора i-ой строки, j-го столбца матрицы
        {
            return this.MatrixWithoutColumn(j).MatrixWithoutRow(i).CalcDeterminant();
        }

        //Create new invert matrix
        public Matrix NewInvertMatrix()
        {
            if (!this.CheckSquare)
            {
                return null;
            }
            if ((double.IsNaN(this.determinant) ? CalcDeterminant() : this.determinant) == 0)   //возможно появление ошибки тут, когда det будет очень малым но не 0
            {
                return null;
            }
            Matrix invertMatrix = new Matrix(this.row, this.row);

            ActionFunctForData((i, j) => invertMatrix[i, j] = ((i + j) % 2 == 0 ? 1 : -1) * this.CalcMinorMatrix(i, j) / this.determinant);

            return invertMatrix.NewTransposeMatrix();
        }

        //Перегрузка как метод доступа "[]", будем получать доступ к массиву данных "data"
        public double this[int x, int y]
        {
            get
            {
                if (x >= this.row && x < 0 || y >= this.column && y < 0)
                {
                    throw new ArgumentException("Write to the incorrect address");
                }
                return data[x, y];
            }
            set
            {
                if (x >= this.row && x < 0 || y >= this.column && y < 0)
                {
                    throw new ArgumentException("Write to the incorrect address");
                }
                data[x, y] = value;
                determinant = double.NaN;
            }
        }

        public void WriteToCell(int i, int j, double x)
        {
            this[i, j] = x;
        }

        //TODO: add operators <*> for calculate matrix
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.column != m2.column || m1.row != m2.row)
            {
                throw new ArgumentException("Row and column of two  matrices must be match.");
            }
            var newM = new Matrix(m1.column, m1.row);
            newM.ActionFunctForData((i, j) => newM[i, j] = m1[i, j] + m2[i, j]);
            return newM;
        }
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.column != m2.column || m1.row != m2.row)
            {
                throw new ArgumentException("Row and column of two  matrices must be match.");
            }
            var newM = new Matrix(m1.column, m1.row);
            newM.ActionFunctForData((i, j) => newM[i, j] = m1[i, j] - m2[i, j]);
            return newM;
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.column != m2.row)
            {
                throw new ArgumentException("Row and column of two  matrices must be match.");
            }
            var newM = new Matrix(m1.row, m2.column);
            newM.ActionFunctForData((i, j) =>
            {
                for (int k = 0; k < m1.column; k++)
                {
                    newM[i, j] += m1[i, k] * m2[k, j];
                }
            });
            return newM;
        }
        public static Matrix operator *(Matrix m1, double d)
        {
            var newM = new Matrix(m1.row, m1.column);
            newM.ActionFunctForData((i, j) => newM[i,j] = m1[i,j]*d);
            return newM;
        }
    }
}
