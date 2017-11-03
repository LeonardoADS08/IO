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

            int x1, x2, x3, x4;

            //int savid, vzvid;// x1y  x2
            //solver.AddVariable("Saudi Arabia", out savid);
            //solver.SetBounds(savid, 0, 9000);//pone sus limited inf y suoeriores
            //solver.AddVariable("Venezuela", out vzvid);
            //solver.SetBounds(vzvid, 0, 6000);

            //variables
            solver.AddVariable("X1",out x1);
            solver.SetBounds(x1,0,Rational.PositiveInfinity);
            solver.AddVariable("X2", out x2);
            solver.SetBounds(x2, 0, Rational.PositiveInfinity);
            solver.AddVariable("X3", out x3);
            solver.SetBounds(x3, 0, Rational.PositiveInfinity);
            solver.AddVariable("X4", out x4);
            solver.SetBounds(x4, 0, Rational.PositiveInfinity);
            //int gasoline, jetfuel, machinelubricant, cost;//son sus restricciones (solo declara cuantas seran)
            //solver.AddRow("gasoline", out gasoline);
            //solver.AddRow("jetfuel", out jetfuel);
            //solver.AddRow("machinelubricant", out machinelubricant);
            //solver.AddRow("cost", out cost);


            //restricicones
            int r1, r2, r3, r4,z;
            solver.AddRow("R1", out r1);
            solver.AddRow("R2", out r2);
            solver.AddRow("R3", out r3);
            solver.AddRow("R4", out r4);
            solver.AddRow("Z", out z);
            //crear ecuacion
            solver.SetCoefficient(r1, x1, 10);
            solver.SetCoefficient(r1, x2, 3);
            solver.SetCoefficient(r1, x3, 8);
            solver.SetCoefficient(r1, x4, 2);
            solver.SetBounds(r1, 5, Rational.PositiveInfinity);
            solver.SetCoefficient(r2, x1, 90);
            solver.SetCoefficient(r2, x2, 150);
            solver.SetCoefficient(r2, x3, 75);
            solver.SetCoefficient(r2, x4, 175);
            solver.SetBounds(r2, 100, Rational.PositiveInfinity);
            solver.SetCoefficient(r3, x1, 45);
            solver.SetCoefficient(r3, x2, 25);
            solver.SetCoefficient(r3, x3, 20);
            solver.SetCoefficient(r3, x4, 37);
            solver.SetBounds(r3, 30, Rational.PositiveInfinity);
            solver.SetCoefficient(r4, x1, 1);
            solver.SetCoefficient(r4, x2, 1);
            solver.SetCoefficient(r4, x3, 1);
            solver.SetCoefficient(r4, x4, 1);
            solver.SetBounds(r4, 1,1);


            solver.SetCoefficient(z, x1, 800);
            solver.SetCoefficient(z, x2, 400);
            solver.SetCoefficient(z, x3, 600);
            solver.SetCoefficient(z, x4, 500);
            solver.AddGoal(z, 1, true);
            solver.Solve(new SimplexSolverParams());

            //esto  muestra el lado izquierdo de una restriccion(variables y holguras o escedentes)
            Console.WriteLine("x1= {0}\nx2= {1}\nx3{2}\nx4= {3}\nr1= {4}\nr2= {5}\nr3= {6}\nr4= {7}\nz= {8}\n",
                solver.GetValue(x1).ToDouble(), solver.GetValue(x2).ToDouble(),
                solver.GetValue(x3).ToDouble(), solver.GetValue(x4).ToDouble(), solver.GetValue(r1).ToDouble(),
                solver.GetValue(r2).ToDouble(), solver.GetValue(r3).ToDouble(), solver.GetValue(r4).ToDouble(),
                solver.GetValue(z).ToDouble());

          
            //solver.SetCoefficient(gasoline, savid, 0.3);// a la variavle  Xi le asiga un coeficiente en una restriccion
            ////en este caso en la restriccion de gasolina en la variable X1 tien 0.3
            //// 0.3*X1 + BX2=C
            //solver.SetCoefficient(gasoline, vzvid, 0.4);
            //// 0.3*X1 + 0.4X2=C
            //solver.SetBounds(gasoline, 2000, Rational.PositiveInfinity);//bascamente el signo
            ////     0.3*X1 + 0.4X2=>2000
            ////buscar lo del gual
            //solver.SetCoefficient(jetfuel, savid, 0.4);
            //solver.SetCoefficient(jetfuel, vzvid, 0.2);
            //solver.SetBounds(jetfuel, 1500, Rational.PositiveInfinity);
            //solver.SetCoefficient(machinelubricant, savid, 0.2);
            //solver.SetCoefficient(machinelubricant, vzvid, 0.3);
            //solver.SetBounds(machinelubricant, 500, Rational.PositiveInfinity);

            //solver.SetCoefficient(cost, savid, 20);
            //solver.SetCoefficient(cost, vzvid, 15);
            //solver.AddGoal(cost, 1, true);
            ////maximizar o minimizar fun OBjetivo
            //solver.Solve(new SimplexSolverParams());//mi no entender

            //Console.WriteLine("SA {0}\n VZ {1}\n Gasoline {2}\n Jet Fuel {3}\n Machine Lubricant {4}\n Cost {5}",
            //    solver.GetValue(savid).ToDouble(),
            //    solver.GetValue(vzvid).ToDouble(),
            //    solver.GetValue(gasoline).ToDouble(),
            //    solver.GetValue(jetfuel).ToDouble(),
            //    solver.GetValue(machinelubricant).ToDouble(),
            //    solver.GetValue(cost).ToDouble());//string por parametro

            Console.ReadKey();

        }
    }
}
