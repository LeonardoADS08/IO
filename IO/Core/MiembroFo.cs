using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class MiembroFo
    {
        private string _name;
        private double _coef;
       public int _value;

        public string Name { get => _name; set => _name = value; }
        public double Coef { get => _coef; set => _coef = value; }

        public MiembroFo()
        {
            _value = 0;
            _coef = 1;
        }
    
    }
}
