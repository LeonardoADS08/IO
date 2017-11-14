using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Restriction
    {
        private double _bside;
        List<double> _coef;
        private string _name;
        public int _value;//sera la suma dle b con la de olgura u exedente

        public double Bside { get => _bside; set => _bside = value; }
        public List<double> Coef { get => _coef; set => _coef = value; }
        public string Name { get => _name; set => _name = value; }
    }
}
