using System;

namespace Math.Structures
{
    public class Matrix
    {
        private int _rows, _columns;
        private Fraction[,] _data;

        public int Columns { get => _columns; set => _columns = value; }
        public int Rows { get => _rows; set => _rows = value; }
        public Fraction[,] Data { get => _data; set => _data = value; }

        public Matrix()
        {
            _rows = 0;
            _columns = 0;
        }

        public Matrix(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _data = new Fraction[rows, columns];
        }

        public void Transpose()
        {
            Matrix result = new Matrix(_columns, _rows);
            for (int i = 0; i < _rows; ++i)
            {
                for (int j = 0; j < _columns; ++j)
                {
                    result.Data[j, i] = _data[i, j];
                }
            }
            _columns = result.Columns;
            _rows = result.Rows;
            _data = result.Data;
        }

        public bool SquareMatrix() => _columns == _rows;

        public Matrix Identity()
        {
            if (!SquareMatrix()) return null;

            Matrix result = new Matrix(_rows, _columns);
            for (int i = 0; i < _rows; ++i)
                for (int j = 0; j < _columns; ++j)
                {
                    if (i == j) result.Data[i, j] = 1;
                    else result.Data[i, j] = 0;
                }
            return result;
        }

        public Matrix Invert()
        {
            Matrix result = Identity();
            Fraction pivot, aux;

            // Se recorrera toda la diagonal, que es igual a la cantidad de filas o columnas
            for (int k = 0; k < _rows; ++k)
            {

                // Se guarda temporalmente el pivote
                pivot = _data[k, k];
                pivot.Simplify();

                if (pivot.Numerator == 0) return null; // Si el pivote es 0 no es invertible
                else if (pivot.Numerator != 1 || pivot.Denominator != 1) // Si el pivote no es igual a la unidad, se lo convierte a la unidad
                {
                    for (int i = 0; i < _columns; ++i)
                    {
                        _data[k, i] = _data[k, i] / pivot;
                        result.Data[k, i] = result.Data[k, i] / pivot;
                    }
                }

                // Se cera las filas de abajo del pivote
                for (int i = 0; i < _rows; ++i)
                {
                    // Si el elemento de la fila ya esta cerado ó es el pivote, se pasa al siguiente.
                    if (_data[i, k].Numerator == 0 || i == k) continue;

                    // Se toma el elemento que debe convertirse en cero.
                    aux = _data[i, k];
                    for (int j = 0; j < _columns; ++j)
                    {
                        _data[i, j] = _data[i, j] - aux * _data[k, j];
                        result.Data[i, j] = result.Data[i, j] - aux * result.Data[k, j];
                    }
                }
                
            }
            return result;
        }

        public void showMatrix()
        {
            for (int i = 0; i < _rows; ++i)
            {
                for (int j = 0; j < _columns; ++j)
                {
                    if (_data[i, j].Denominator != 1)
                        Console.Write(_data[i, j].Numerator + "/" + _data[i, j].Denominator + " ");
                    else
                        Console.Write(_data[i, j].Numerator + " ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

    }
}
