using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math.Constants;

namespace Math.Structures
{
    public class LinearEquation
    {
        List<Fraction> _firstTerms;
        Fraction _secondTerms;
        Constants.Signs _sign;

        public List<Fraction> FirstTerms { get { return _firstTerms; } set { _firstTerms = value; } }
        public Fraction SecondTerm { get { return _secondTerms; } set { _secondTerms = value; } }
        public Signs Sign { get { return _sign; } set { _sign = value; } }

        public LinearEquation()
        {
            _firstTerms = new List<Fraction>();
            _secondTerms = new Fraction();
            _sign = Signs.Equal;
        }


    }
}
