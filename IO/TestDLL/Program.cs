using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 3, aux, k =0;
            Math.Structures.Matrix data = new Math.Structures.Matrix(n, n);
            int[] m = { 3, 3, 5, 2, 0, 1, 4, 4, 3 };

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    data.Data[i, j] = m[k];
                    k++;
                }
            }

            data.Invert();
            

            Console.ReadKey();
        }
    }
}
