using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;

namespace SolverFtest
{
    public class Simplex
    {
        public int _z;

        private List<MiembroFo> _fo;
        
        public List<MiembroFo> FO { get => _fo; set => _fo = value; }
        internal List<Restriction> Res { get => _res; set => _res = value; }
        public SimplexSolver Solver { get => _solver; set => _solver = value; }
        public int Z { get => _z; set => _z = value; }

        private SimplexSolver _solver;
        private List<Restriction> _res;

        public Simplex(List<MiembroFo>x,int Objective)
        {
            FO = new List<MiembroFo>();
            int i;
            FO = x;
            Solver=new SimplexSolver();
            Solver.AddRow("Z", out _z);//declara la existencia de la funcion objetivo
            for (i = 0; i < FO.Count; i++)
            {
                Solver.AddVariable(FO[i].Name, out FO[i]._value);//asigna donde se guardaran los resultados;
                Solver.SetBounds(FO[i]._value, 0, Rational.PositiveInfinity);//asigna los limites de las variables
                Solver.SetCoefficient(_z, FO[i]._value, FO[i].Coef);//asigna los coef de las variables en la func ob

            }
            bool aux=false;
            if (Objective == 1) { aux = true; }
            else if (Objective == 0) { aux = false; }
            Solver.AddGoal(_z, 1, aux);//determina si es max o min(el uno no se para que sirve)
        }

        public Reporte TransformToFinalReport()
        {
            Reporte x = null ;
            Solver.Solve(new SimplexSolverParams());
            return x;
        }
        public void AddRestriction(List<Restriction> x)//tiene que recibir todas las restricciones ya llenadas
        {
            Res = x;
            foreach (Restriction t in Res)
            {
                Solver.AddRow(t.Name, out t._value);
                for (int j = 0; j < t.Coef.Count; j++)
                {
                    Solver.SetCoefficient(t._value, FO[j]._value, t.Coef[j]);// asigna a la variable correspondiente en la restriccion un coeficiente
                }

                if (t._sign==Signo.Igual)
                {
                    Solver.SetBounds(t._value, t.Bside, t.Bside);
                }
                else if (t._sign == Signo.MayorIgualQue)
                {
                    Solver.SetBounds(t._value, t.Bside,Rational.PositiveInfinity);
                }
                else if (t._sign == Signo.MayorQue)
                {
                    Solver.SetBounds(t._value, t.Bside + 0.1, Rational.PositiveInfinity);
                }
                else if (t._sign == Signo.MenorIgualQue)
                {
                    Solver.SetBounds(t._value,Rational.NegativeInfinity, t.Bside);
                }
                else if(t._sign == Signo.MenorQue)
                { Solver.SetBounds(t._value, Rational.NegativeInfinity, t.Bside - 0.1); }
            }
        }


    }
}
