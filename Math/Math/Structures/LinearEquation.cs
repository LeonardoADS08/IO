using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math.Constants;

namespace Math.Structures
{
    class LinearEquation
    {
        List<Fraction> _terms;
        Fraction _b;
        Constants.Signs _sign;

        public List<Fraction> Terms { get { return _terms; } set { _terms = value; } }
        public Fraction B { get { return _b; } set { _b = value; } }
        public Signs Sign { get { return _sign; } set { _sign = value; } }

        public LinearEquation()
        {
            _terms = new List<Fraction>();
            _b = new Fraction();
            _sign = Signs.Equal;
        }
    }
}
