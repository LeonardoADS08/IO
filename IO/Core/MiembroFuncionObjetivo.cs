using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Core
{
    public class MiembroFuncionObjetivo
    {
        private string _nombre;
        private double _coeficiente;
        public int _valor;

        public string Nombre { get => _nombre; set => _nombre = value; }
        public double Coeficiente { get => _coeficiente; set => _coeficiente = value; }

        public MiembroFuncionObjetivo()
        {
            _valor = 0;
            _coeficiente = 1;
        }
    
    }
}
