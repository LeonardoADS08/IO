using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using Microsoft.SolverFoundation.Services;
namespace P
{
    class Program
    {
        static void Main(string[] args)
        {
            SimplexSolver solver = new SimplexSolver();
            int savid, vzvid;// x1y  x2
            solver.AddVariable("Saudi Arabia", out savid);
            solver.SetBounds(savid, 0, 9000);//pone sus limited inf y suoeriores
            solver.AddVariable("Venezuela", out vzvid);
            solver.SetBounds(vzvid, 0, 6000);

            int gasoline, jetfuel, machinelubricant, cost;//son sus restricciones (solo declara cuantas seran)
            solver.AddRow("gasoline", out gasoline);
            solver.AddRow("jetfuel", out jetfuel);
            solver.AddRow("machinelubricant", out machinelubricant);
            solver.AddRow("cost", out cost);

            solver.SetCoefficient(gasoline, savid, 0.3);// a la variavle  Xi le asiga un coeficiente en una restriccion
            //en este caso en la restriccion de gasolina en la variable X1 tien 0.3
            // 0.3*X1 + BX2=C
            solver.SetCoefficient(gasoline, vzvid, 0.4);
            // 0.3*X1 + 0.4X2=C
            solver.SetBounds(gasoline, 2000, Rational.PositiveInfinity);//bascamente el signo
            //     0.3*X1 + 0.4X2=>2000
            //buscar lo del gual
            solver.SetCoefficient(jetfuel, savid, 0.4);
            solver.SetCoefficient(jetfuel, vzvid, 0.2);
            solver.SetBounds(jetfuel, 1500, Rational.PositiveInfinity);
            solver.SetCoefficient(machinelubricant, savid, 0.2);
            solver.SetCoefficient(machinelubricant, vzvid, 0.3);
            solver.SetBounds(machinelubricant, 500, Rational.PositiveInfinity);

            solver.SetCoefficient(cost, savid, 20);
            solver.SetCoefficient(cost, vzvid, 15);
            solver.AddGoal(cost, 1, true);
            //maximizar o minimizar fun OBjetivo
            solver.Solve(new SimplexSolverParams());//mi no entender

            Console.WriteLine("SA {0}\n VZ {1}\n Gasoline {2}\n Jet Fuel {3}\n Machine Lubricant {4}\n Cost {5}",
                solver.GetValue(savid).ToDouble(),
                solver.GetValue(vzvid).ToDouble(),
                solver.GetValue(gasoline).ToDouble(),
                solver.GetValue(jetfuel).ToDouble(),
                solver.GetValue(machinelubricant).ToDouble(),
                solver.GetValue(cost).ToDouble());//string por parametro
            
Console.ReadKey();

        }
    }
}
