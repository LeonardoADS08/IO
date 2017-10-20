﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using Math.Structures;

namespace IOCore
{
    class ObjectiveFunction
    {
        Math.Structures.LinearEquation _FOEquation;
        Constants.Objetive _objetive;
        ObjectiveFunction()
        {
            _FOEquation = new LinearEquation();
            _objetive = new Constants.Objetive();
        }
        public LinearEquation FOEquation { get => _FOEquation; set => _FOEquation = value; }
        internal Constants.Objetive Objetive { get => _objetive; set => _objetive = value; }
        public Fraction SolutionZ(List<Fraction> _variants)
        {
            int _tam = (_FOEquation.FirstTerms.Count),_counter=0;
            Fraction Sol = new Fraction(1);
            while (_counter<_tam)
            {
                Sol += ((_FOEquation.FirstTerms[_counter])*(_variants[_counter]));
            }
            return Sol;
        }//return Z value(entire problem solution)
    }
}
