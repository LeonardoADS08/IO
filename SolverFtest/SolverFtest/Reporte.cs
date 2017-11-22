using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;

namespace SolverFtest
{

    public class Reporte
    {
        private Simplex _sollutions;

        public Reporte(Simplex sollutions)
        {
            _sollutions = sollutions;
        }

        public List<double> Report_Variables()
        {
            List<double> aux=new List<double>();
            
            foreach (MiembroFo t in _sollutions.FO)
            {
                aux.Add(_sollutions.Solver.GetValue(t._value).ToDouble());
            }
            return aux;
        }

        public List<double> Report_Restriction_Variables()
        {
            List<double> aux = new List<double>();
            foreach (Restriction t in _sollutions.Res)
            {
                aux.Add(_sollutions.Solver.GetValue(t._value).ToDouble());
            }
            return aux;
        }
    }


}
