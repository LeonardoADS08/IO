using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IO.Frames.Simplex
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {
        public List<Core.MiembroFo> FuncionObjetivo { get; set; }
        public List<Core.Restriction> Restricciones { get; set; }
        public Core.Simplex Simplex { get; set; }
        public Core.Reporte ReporteModelo { get; set; }
        public Reporte(List<Core.MiembroFo> FO, List<Core.Restriction> Rest, int Objetivo )
        {
            InitializeComponent();

            FuncionObjetivo = FO;
            Restricciones = Rest;

            Simplex = new Core.Simplex(FuncionObjetivo, Objetivo);
            Simplex.AddRestriction(Restricciones);
            ReporteModelo = new Core.Reporte(Simplex);
        }
    }
}
