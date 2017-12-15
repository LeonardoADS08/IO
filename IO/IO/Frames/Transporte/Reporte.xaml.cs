using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace IO.Frames.Transporte
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {
        public List<Core.MiembroFuncionObjetivo> FuncionObjetivo { get; set; }
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

        public Reporte(List<Core.MiembroFuncionObjetivo> FO, List<Core.Restriction> Rest, int Objetivo, int totalOfertantes, int totalDemandantes, List<double> demandas, List<double> ofertas)
        {
            InitializeComponent();

            Frames.Simplex.Reporte rep = new Frames.Simplex.Reporte(FO, Rest, Objetivo);
            rep.Show();

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
                newRow[0] = FuncionObjetivo[i].Nombre;
                newRow[1] = Solucion[i].ToDouble();
                newRow[2] = FuncionObjetivo[i].Coeficiente;
                newRow[3] = FuncionObjetivo[i].Coeficiente * Solucion[i].ToDouble();

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
            double recursosUsados = Solucion.Sum(x => x.ToDouble()), demandaTotal = Demanda.Sum(), ofertaTotal = Oferta.Sum();

            L_ValorObjetivo.Content += " " + ReporteModelo.ObtenerZ().ToString();
            L_Disponibilidad.Content += (ofertaTotal - recursosUsados).ToString();
            L_Requerimiento.Content += (demandaTotal - recursosUsados).ToString();

            // Ficticios, que son básicamente las variables de holgura/excedente.
            // Las cantidad de pende de la demanda y oferta total, el que tenga menos será al que le toque ser ficticio.
            if (ofertaTotal != demandaTotal)
            {
                var HolguraExcedente = ReporteModelo.HolguraExcedente();
                // HolgutaExcedente: 1.- Oferta || 2.- Demanda || 3.- Requisitos
                if (ofertaTotal > demandaTotal)
                {
                    for (int i = 0; i < totalOfertantes; ++i)
                    {
                        DataRow newRow = SolucionOptimaDT.NewRow();
                        newRow[0] = String.Format("Demandante Ficticio -> Ofertante {0}", (i + 1));
                        newRow[1] = Math.Abs(HolguraExcedente[i]);
                        newRow[2] = 0;
                        newRow[3] = 0;

                        SolucionOptimaDT.Rows.Add(newRow);
                    }
                }
                else if (demandaTotal > ofertaTotal)
                {
                    for (int i = 0; i < totalDemandantes; ++i)
                    {
                        DataRow newRow = SolucionOptimaDT.NewRow();
                        newRow[0] = String.Format("Ofertante Ficticio -> Demandante {0}", (i + 1));
                        newRow[1] = Math.Abs(HolguraExcedente[totalOfertantes + i]);
                        newRow[2] = 0;
                        newRow[3] = 0;

                        SolucionOptimaDT.Rows.Add(newRow);
                    }
                }
            }
            

            
        }

        private void B_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}