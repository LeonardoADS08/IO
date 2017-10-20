using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math;
using Math.Structures;

namespace IOCore
{
    public class Restriction
    {
        private Math.Structures.LinearEquation _resEquation;

        public LinearEquation ResEquation { get => _resEquation; set => _resEquation = value; }

        public Fraction ExcendentOrSlack(List<Fraction> _variants)
        {
            int _tam = _variants.Count;
            int _counter = 0;
            Fraction _sol=new Fraction(0);
            Fraction _Resvariant=new Fraction(0);
            while(_counter<_tam)
            {
                _sol += _variants[_counter] * _resEquation.FirstTerms[_counter];
            }
            if (_resEquation.Sign == Math.Constants.Signs.BiggerEqual || _resEquation.Sign == Math.Constants.Signs.Bigger)
            {
                return _resEquation.SecondTerm - _sol;
            }
            else if (_resEquation.Sign == Math.Constants.Signs.LessEqual || _resEquation.Sign == Math.Constants.Signs.Less)
            {
                return _sol - _resEquation.SecondTerm;
            }
            else
            {
                return null;
            }
        }

        public bool AproovedRequeriment(List<Fraction> _variants)
        {
            int _tam = _variants.Count;
            int _counter = 0;
            Fraction _sol = new Fraction(0);
            while (_counter < _tam)
            {
                _sol += _variants[_counter] * _resEquation.FirstTerms[_counter];
            }
            if(_resEquation.Sign==Math.Constants.Signs.BiggerEqual)
            {
                return (_sol >= _resEquation.SecondTerm);
            }
            else if(_resEquation.Sign == Math.Constants.Signs.Bigger)
            {
                return (_sol >_resEquation.SecondTerm);
            }
            else if(_resEquation.Sign == Math.Constants.Signs.LessEqual)
            {
                return (_sol <= _resEquation.SecondTerm);
            }
            else if(_resEquation.Sign == Math.Constants.Signs.Less)
            {
                return (_sol < _resEquation.SecondTerm);
            }
            else { return (_sol == _resEquation.SecondTerm); }
            
        }

        public Fraction Term(int _pos)
        {

            return _resEquation.FirstTerms[_pos];
        }
        public Restriction()
        {
            _resEquation = new LinearEquation();
        }
    }
}
