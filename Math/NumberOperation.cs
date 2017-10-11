using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    static class NumberOperation
    {
        public static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
        
        public static int LCD(int a, int b)
        {
            return System.Math.Abs(a * b) / GCD(a, b);
        }

        // EXPERIMENTAL - Exponenciación binaria
        public static int Pow(int number, int exponent)
        {
            int result = 1, aux;
            while (exponent != 0)
            {
                aux = exponent & 1;
                if (aux != 0)
                    result *= number;
                exponent >>= 1;
                number *= number;
            }

            return result;
        }

    }
}
