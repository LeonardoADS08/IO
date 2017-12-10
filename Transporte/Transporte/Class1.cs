using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transporte
{
    public class NWcorner2
    {

        public static int NorthWest(int[] s, int[] d, int[,] c)
        {
            int[,] table = new int[s.Count(), d.Count()];
            int[] supply = s;    // copy supply and demand array so original
            int[] demand = d;        // arrays remain unchanged
            int totalCost = 0;

            for (int i = 0; i < supply.Count(); i++)
            {
                for (int j = 0; j < demand.Count(); j++)
                {
                    // If Supply >= Demand, demand is filled completely, demand = 0
                    if (supply[i] >= demand[j])
                    {
                        table[i,j] = demand[j];
                        supply[i] = supply[i] - demand[j];
                        demand[j] = 0;
                    }
                    // If Supply < Demand, demand = demand - supply, supply = 0
                    else
                    {
                        table[i,j] = supply[i];
                        demand[j] = demand[j] - supply[i];
                        supply[i] = 0;
                    }
                    Console.Write(table[i,j] + "\t");   // Print as calculated
                }
                Console.WriteLine();                       // New line
            }
            // Calculate Shipping cost using NW Corner method
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    totalCost = totalCost + c[i,j] * table[i,j];
                }
            }
            Console.WriteLine("Total Cost = $" + totalCost);
            return (totalCost);
        }



        public static int MinCost(int[] s, int[] d, int[,] c)
        {
            int[,] table = new int[s.Count(),d.Count()];
            int[] supply = s;        // copy supply and demand and cost array 
            int[] demand = d;        // so original arrays remain unchanged
            int[,] cost = c;
            int totalCost = 0;
            int smallest = c[0,0];
            int ii = 0, jj = 0;
            bool rowSatisfied = false;
            bool colSatisfied = false;

            // find smallest cost of shipment from supplier
            for (int i = 0; i < supply.Count(); i++)
            {
                for (int j = 0; j < demand.Count(); j++)
                {
                    if (smallest > cost[i,j])
                    {
                        smallest = cost[i,j];
                        ii = i;
                        jj = j;
                    }
                }
            }
            int last = smallest;

            while (colSatisfied != true)
            {
                // If Supply >= Demand, demand is filled completely, demand = 0
                if (supply[ii] >= demand[jj])
                {
                    table[ii,jj] = demand[jj];
                    supply[ii] = supply[ii] - demand[jj];
                    demand[jj] = 0;
                }
                // If Supply < Demand, demand = demand - supply, supply = 0
                else
                {
                    table[ii,jj] = supply[ii];
                    demand[jj] = demand[jj] - supply[ii];
                    supply[ii] = 0;
                }
                Console.WriteLine("table " + table[ii,jj]);
                Console.WriteLine(ii + "  " + jj);

                Console.WriteLine("supply " + supply[ii]);
                Console.WriteLine("demand " + demand[jj]);
                Console.WriteLine("smallest " + smallest + " last " + last);
                int temp = 0;
                // Find next smallest cost of shipping
                for (int i = 0; i < s.Count(); i++)
                {
                    if (cost[i,jj] > last)
                    {
                        temp = cost[i,jj];
                        Console.WriteLine(temp);
                        if (temp <= cost[i,jj])
                        {
                            smallest = cost[i,jj];
                            ii = i;
                            Console.WriteLine(i + " " + temp);
                        }
                    }
                    Console.WriteLine(i + " " + ii + "  " + jj + " " + smallest);
                }
                Console.WriteLine("smallest " + smallest + " last " + last);
                Console.WriteLine(ii + "  " + jj);
                if (demand[jj] == 0)
                    colSatisfied = true;
            }

            // Calculate Shipping cost using Minimum Cost Method
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    totalCost = totalCost + c[i,j] * table[i,j];
                }
            }
            Console.WriteLine("Total Cost = $" + totalCost);
            return (totalCost);
        }
        // Calculate percentage saved using minPrice method over NWcorner method
        public static void percentSaved(int NWprice, int minPrice)
        {
            Console.WriteLine("Percentage "
                    + "saved: " + (100 - (minPrice / NWprice) * 100) + "%");

        }
    }
}
