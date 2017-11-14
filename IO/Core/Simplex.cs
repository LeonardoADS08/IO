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
    class Simplex
    {

        private List<MiembroFo> _fo;
        
        public List<MiembroFo> FO { get => _fo; set => _fo = value; }
        internal List<Restriction> Res { get => _res; set => _res = value; }
        public SimplexSolver Solver { get => _solver; set => _solver = value; }

        private SimplexSolver _solver;
        private List<Restriction> _res;
        Simplex(List<MiembroFo>x)
        {
            int i;
            FO = x;
            Solver=new SimplexSolver();

            for (i = 0; i < FO.Count; i++)
            {
                Solver.AddVariable(FO[i].Name, out FO[i]._value);//asigna donde se guardaran los resultados;
                Solver.SetBounds(FO[i]._value, 0, Rational.PositiveInfinity);
            }


        }

        void AddRestriction(Restriction x)
        {
            Res.Add(x);
            for (int i = 0; i < Res.Count; i++)
            {
             Solver.AddRow(Res[i].Name,out)   
            }
        }


    }
}
