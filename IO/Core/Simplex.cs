using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using System.Collections.Generic;

namespace Core
{
    public class Simplex
    {
        public int _z;

        private List<MiembroFuncionObjetivo> _fo;

        public List<MiembroFuncionObjetivo> FO { get => _fo; set => _fo = value; }
        public List<Restriction> Res { get => _res; set => _res = value; }
        public SimplexSolver Solver { get => _solver; set => _solver = value; }
        public int Z { get => _z; set => _z = value; }

        private SimplexSolver _solver;
        private List<Restriction> _res;

        public Simplex(List<MiembroFuncionObjetivo> x, int Objective)
        {
            FO = new List<MiembroFuncionObjetivo>();
            int i;
            FO = x;
            Solver = new SimplexSolver();
            Solver.AddRow("Z", out _z);//declara la existencia de la funcion objetivo
            for (i = 0; i < FO.Count; i++)
            {
                Solver.AddVariable(FO[i].Nombre, out FO[i]._valor);//asigna donde se guardaran los resultados;
                Solver.SetBounds(FO[i]._valor, 0, Rational.PositiveInfinity);//asigna los limites de las variables
                Solver.SetCoefficient(_z, FO[i]._valor, FO[i].Coeficiente);//asigna los coef de las variables en la func ob
            }
            bool aux = false;
            if (Objective == 1) { aux = true; }
            else if (Objective == 0) { aux = false; }
            Solver.AddGoal(_z, 1, aux);//determina si es max o min(el uno no se para que sirve)
        }

        public Reporte TransformToFinalReport()
        {
            Reporte x = null;
            Solver.Solve(new SimplexSolverParams());

            return x;
        }

        public void AddRestriction(List<Restriction> x)//tiene que recibir todas las restricciones ya llenadas
        {
            Res = x;
            foreach (Restriction t in Res)
            {
                Solver.AddRow(t.Nombre, out t.HolguraExcedente);
                for (int j = 0; j < t.Coeficientes.Count; j++)
                {
                    Solver.SetCoefficient(t.HolguraExcedente, FO[j]._valor, t.Coeficientes[j]);// asigna a la variable correspondiente en la restriccion un coeficiente
                }

                if (t.Signo == Signo.Igual)
                {
                    Solver.SetBounds(t.HolguraExcedente, t.LadoB, t.LadoB);
                }
                else if (t.Signo == Signo.MayorIgualQue)
                {
                    Solver.SetBounds(t.HolguraExcedente, t.LadoB, Rational.PositiveInfinity);
                }
                else if (t.Signo == Signo.MayorQue)
                {
                    Solver.SetBounds(t.HolguraExcedente, t.LadoB + 0.1, Rational.PositiveInfinity);
                }
                else if (t.Signo == Signo.MenorIgualQue)
                {
                    Solver.SetBounds(t.HolguraExcedente, Rational.NegativeInfinity, t.LadoB);
                }
                else if (t.Signo == Signo.MenorQue)
                { Solver.SetBounds(t.HolguraExcedente, Rational.NegativeInfinity, t.LadoB - 0.1); }
            }
        }
    }
}