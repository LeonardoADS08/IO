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
using System.Data;

namespace IO.Frames.Transporte
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
        public DataTable SolucionOptimaDT { get; set; }
        public DataTable OfertantesDT { get; set; }
        public DataTable DemandantesDT { get; set; }
        public int TotalOfertantes { get; set; }
        public int TotalDemandantes { get; set; }
        public List<double> Demanda { get; set; }
        public List<double> Oferta { get; set; }

        public Reporte(List<Core.MiembroFo> FO, List<Core.Restriction> Rest, int Objetivo, int totalOfertantes, int totalDemandantes, List<double> demandas, List<double> ofertas)
        {
            InitializeComponent();

            FuncionObjetivo = FO;
            Restricciones = Rest;
            TotalOfertantes = totalOfertantes;
            TotalDemandantes = totalDemandantes;
            Demanda = demandas;
            Oferta = ofertas;

            Simplex = new Core.Simplex(FuncionObjetivo, Objetivo);
            Simplex.AddRestriction(Restricciones);
            ReporteModelo = new Core.Reporte(Simplex);

            // Solución óptima
            SolucionOptimaDT = new DataTable();

            SolucionOptimaDT.Columns.Add(new DataColumn("Ruta", typeof(string)));
            SolucionOptimaDT.Columns.Add(new DataColumn("Valor", typeof(double)));
            SolucionOptimaDT.Columns.Add(new DataColumn("Coeficiente", typeof(double)));
            SolucionOptimaDT.Columns.Add(new DataColumn("Aporte", typeof(double)));

            var Solucion = ReporteModelo.Solucion();
            for (int i = 0; i < FuncionObjetivo.Count; ++i)
            {
                DataRow newRow = SolucionOptimaDT.NewRow();
                newRow[0] = FuncionObjetivo[i].Name;
                newRow[1] = Solucion[i].ToDouble();
                newRow[2] = FuncionObjetivo[i].Coef;
                newRow[3] = FuncionObjetivo[i].Coef * Solucion[i].ToDouble();

                SolucionOptimaDT.Rows.Add(newRow);
            }

            // Ofertantes
            OfertantesDT = new DataTable();

            OfertantesDT.Columns.Add(new DataColumn("Ofertante", typeof(string)));
            OfertantesDT.Columns.Add(new DataColumn("Disponibilidad", typeof(double)));
            OfertantesDT.Columns.Add(new DataColumn("Enviado", typeof(double)));
            OfertantesDT.Columns.Add(new DataColumn("% Enviado", typeof(string)));

            for (int i = 0; i < TotalOfertantes; ++i)
            {
                DataRow newRow = OfertantesDT.NewRow();
                double temp = 0;
                newRow[0] = "Ofertante " + (i + 1).ToString();
                newRow[1] = Oferta[i];
                for (int j = 0; j < TotalDemandantes; ++j)
                    temp += Solucion[j * TotalOfertantes + i].ToDouble();
                newRow[2] = temp;
                newRow[3] = (Math.Round((temp * 100) / Oferta[i], 2)).ToString() + "%";

                OfertantesDT.Rows.Add(newRow);
            }

            // Demandantes
            DemandantesDT = new DataTable();

            DemandantesDT.Columns.Add(new DataColumn("Demandante", typeof(string)));
            DemandantesDT.Columns.Add(new DataColumn("Requerido", typeof(double)));
            DemandantesDT.Columns.Add(new DataColumn("Recibido", typeof(double)));
            DemandantesDT.Columns.Add(new DataColumn("% Recibido", typeof(string)));

            for (int i = 0; i < totalDemandantes; ++i)
            {
                DataRow newRow = DemandantesDT.NewRow();
                double temp = 0;

                newRow[0] = "Demandante " + (i + 1).ToString();
                newRow[1] = Demanda[i];
                for (int j = 0; j < TotalOfertantes; ++j)
                    temp += Solucion[j + i * TotalOfertantes].ToDouble();
                newRow[2] = temp;
                newRow[3] = (Math.Round((temp * 100) / Demanda[i], 2)).ToString() + "%";

                DemandantesDT.Rows.Add(newRow);
            }

            // Bindings
            DT_Demandantes.DataContext = DemandantesDT;
            DT_Ofertantes.DataContext = OfertantesDT;
            DT_Solucion.DataContext = SolucionOptimaDT;

            // Labels
            double recursosUsados = Solucion.Sum( x => x.ToDouble()), demandaTotal = Demanda.Sum(), ofertaTotal = Oferta.Sum();

            L_ValorObjetivo.Content += " " + ReporteModelo.ObtenerZ().ToString();
            L_Disponibilidad.Content += (ofertaTotal - recursosUsados).ToString();
            L_Requerimiento.Content += (demandaTotal - recursosUsados).ToString();

        }

        private void B_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
