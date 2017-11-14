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

        private List<Simplex> _fo;
        
        public List<Simplex> FO { get => _fo; set => _fo = value; }
        internal List<Restriction> Res { get => _res; set => _res = value; }

        private List<Restriction> _res;
        Simplex(List<Simplex>x)
        {
            FO = x;
        }

        void AddRestriction(Restriction x)
        {
            Res.Add(x);
        }
    }
}
