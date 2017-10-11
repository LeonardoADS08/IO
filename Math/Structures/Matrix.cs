using System;
using System.Collections.Generic;
using System.Text;

namespace Math.Structures
{
    class Matrix
    {
        private int _rows, _columns;
        private Fraction[,] _data;

        public int Columns { get => _columns; set => _columns = value; }
        public int Rows { get => _rows; set => _rows = value; }
        internal Fraction[,] Data { get => _data; set => _data = value; }

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

        public Matrix(int rows, int columns, Fraction[,] data)
        {
            _rows = rows;
            _columns = columns;
            _data = data;
            
        }


    }
}
