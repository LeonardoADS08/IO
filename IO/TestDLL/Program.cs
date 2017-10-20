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
            Console.WriteLine(Math.NumberOperation.Pow(2, 2));
            Console.WriteLine(Math.NumberOperation.Pow(3, 2));
            Console.WriteLine(Math.NumberOperation.Pow(4, 2));
            Console.WriteLine(Math.NumberOperation.Pow(5, 2));
            Console.WriteLine(Math.NumberOperation.Pow(6, 2));
            Console.ReadKey();
        }
    }
}
