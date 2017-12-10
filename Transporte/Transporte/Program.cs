using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyDemandSolver;

namespace Transporte
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] s = { 20, 20, 10 };   // Supply
            int[] d = { 10, 5, 10, 25 };   // Demand
            int[,] c = { {5, 6, 7, 3},  // Cost per item from source (row) to 
                      { 10, 8, 11, 15},  //     destination (column)
                      {1, 3, 5, 9}};
            NWcorner2.NorthWest(s, d, c);            // Calculate Shipping using NW Corner
            NWcorner2.MinCost(s, d, c);              // Calculate Shipping using min cost method
                                           //percentSaved(NorthWest(s,d,c),MinCost(s,d,c));

            Console.ReadKey();

        }
    }
}
