using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Reporte
    {
        private double _z;
        private List<double> _slackSurpus;
        
        public double Z { get => _z; set => _z = value; }
        public List<double> SlackSurpus { get => _slackSurpus; set => _slackSurpus = value; }
    }
}
