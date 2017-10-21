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
        private int ES_Sing;
        private Math.Structures.LinearEquation _resEquation;

        public LinearEquation ResEquation { get => _resEquation; set => _resEquation = value; }

        public Fraction ExcendentOrSlackValue(List<Fraction> _variants)
        {
            int _tam = _variants.Count;
            int _counter = 0;
            Fraction _sol = new Fraction();
            Fraction _Resvariant = new Fraction();

            while (_counter < _tam)
            {
                _sol += _variants[_counter] * _resEquation.FirstTerms[_counter];
            }

            if (_resEquation.Sign == Math.Constants.Signs.BiggerEqual || _resEquation.Sign == Math.Constants.Signs.Bigger)
            { ES_Sing = 1; return _resEquation.SecondTerm - _sol; }
            else if (_resEquation.Sign == Math.Constants.Signs.LessEqual || _resEquation.Sign == Math.Constants.Signs.Less)
            { ES_Sing = -1; return _sol - _resEquation.SecondTerm; } 
            else return null;
        }

        public bool AproovedRequeriment(List<Fraction> _variants)
        {
            int _tam = _variants.Count;
            int _counter = 0;
            Fraction _sol = new Fraction();

            while (_counter < _tam)
            {
                _sol += _variants[_counter] * _resEquation.FirstTerms[_counter];
            }

            if (_resEquation.Sign == Math.Constants.Signs.BiggerEqual)
                return (_sol >= _resEquation.SecondTerm);
            else if(_resEquation.Sign == Math.Constants.Signs.Bigger)
                return (_sol >_resEquation.SecondTerm);
            else if(_resEquation.Sign == Math.Constants.Signs.LessEqual)
                return (_sol <= _resEquation.SecondTerm);
            else if(_resEquation.Sign == Math.Constants.Signs.Less)
                return (_sol < _resEquation.SecondTerm);
            else return (_sol == _resEquation.SecondTerm);
            //como es que esto cae en mas leible
        }

        public Fraction Term(int _pos)
        {
            return _resEquation.FirstTerms[_pos];
        }
        public string IdentificationVariant()
        {
            if (ES_Sing == 1) { return "Excedent"; }
            else if(ES_Sing==-1){ return "Slack"; }
            else{ return "Especial case"; }
        }
        
        public Restriction()
        {
            _resEquation = new LinearEquation();
            ES_Sing = 0;
        }
    }
}
