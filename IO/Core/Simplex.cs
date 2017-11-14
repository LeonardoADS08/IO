using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;

namespace Core
{
    public class Simplex
    {
        private int _z;

        private List<MiembroFo> _fo;
        
        public List<MiembroFo> FO { get => _fo; set => _fo = value; }
        internal List<Restriction> Res { get => _res; set => _res = value; }
        public SimplexSolver Solver { get => _solver; set => _solver = value; }
        public int Z { get => _z; set => _z = value; }

        private SimplexSolver _solver;
        private List<Restriction> _res;
        Simplex(List<MiembroFo>x,bool Objective)
        {
            int i;
            FO = x;
            Solver=new SimplexSolver();

            for (i = 0; i < FO.Count; i++)
            {
                Solver.AddVariable(FO[i].Name, out FO[i]._value);//asigna donde se guardaran los resultados;
                Solver.SetBounds(FO[i]._value, 0, Rational.PositiveInfinity);
                Solver.SetCoefficient(_z, FO[i]._value, FO[i].Coef);

            }
            
            Solver.AddGoal(_z, 1, Objective);
        }

        Reporte TransformToFinalReport()
        {
            Reporte x = null ;
            Solver.Solve(new SimplexSolverParams());
            return x;
        }
        void AddRestriction(Restriction x)
        {
            Res.Add(x);
            for (int i = 0; i < Res.Count; i++)
            {
                Solver.AddRow(Res[i].Name, out Res[i]._value);
                for (int j = 0; j < Res[i].Coef.Count; j++)
                {
                    Solver.SetCoefficient(Res[i]._value, FO[j]._value, Res[i].Coef[j]);// asigna a la variable correspondiente en la restriccion un coeficiente
                }
                if (Res[i]._sign==Signo.Igual)
                {
                    Solver.SetBounds(Res[i]._value, Res[i].Bside, Res[i].Bside);
                }
                else if (Res[i]._sign == Signo.MayorIgualQue)
                {
                    Solver.SetBounds(Res[i]._value, Res[i].Bside,Rational.PositiveInfinity);
                }
                else if (Res[i]._sign == Signo.MayorQue)
                {
                    Solver.SetBounds(Res[i]._value, Res[i].Bside + 0.1, Rational.PositiveInfinity);
                }
                else if (Res[i]._sign == Signo.MenorIgualQue)
                {
                    Solver.SetBounds(Res[i]._value, Res[i].Bside, Rational.NegativeInfinity);
                }
                else if(Res[i]._sign == Signo.MenorQue)
                { Solver.SetBounds(Res[i]._value, Res[i].Bside-0.1, Rational.NegativeInfinity); }
            }
        }


    }
}
