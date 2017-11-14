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

        private Dictionary<string, double> _var;
        private Dictionary<string,double>_restriction;
        public Simplex(Dictionary<string, double> vars, Dictionary<string, double> restriction)
        {
            _var = vars;
            _restriction = restriction;
        }


    }
}
