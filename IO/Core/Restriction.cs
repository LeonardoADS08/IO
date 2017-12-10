using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Restriction
    {
        private double _ladoB;
        private List<double> _coeficientes;
        private string _nombre;
        private Signo signo;
        public int HolguraExcedente; //sera la suma dle b con la de olgura u exedente

        public double LadoB { get => _ladoB; set => _ladoB = value; }
        public List<double> Coeficientes { get => _coeficientes; set => _coeficientes = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public Signo Signo { get => signo; set => signo = value; }

        public Restriction()
        {
            _ladoB = 0;
            _coeficientes = new List<double>();
            _nombre = "";
            HolguraExcedente = 0;
        }
    }
}
