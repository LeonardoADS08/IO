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
        public DataTable VariablesDT { get; set; }


        public Reporte(List<Core.MiembroFo> FO, List<Core.Restriction> Rest, int Objetivo )
        {
            InitializeComponent();

            FuncionObjetivo = FO;
            Restricciones = Rest;

            Simplex = new Core.Simplex(FuncionObjetivo, Objetivo);
            Simplex.AddRestriction(Restricciones);
            ReporteModelo = new Core.Reporte(Simplex);

            // Informacion básica
            L_ValorObjetivo.Content += " " + ReporteModelo.ObtenerZ().ToString();
            L_Restricciones.Content += " " + Rest.Count;
            L_Variables.Content += " " + FO.Count;

            // Variables
            DataTable VariablesDT = new DataTable();
            VariablesDT.Columns.Add( new DataColumn(String.Format("Nombre"), typeof(string)) ); // 0
            VariablesDT.Columns.Add( new DataColumn(String.Format("Valor"), typeof(double)) ); // 1
            VariablesDT.Columns.Add( new DataColumn(String.Format("Coeficiente"), typeof(double)) ); // 2
            VariablesDT.Columns.Add( new DataColumn(String.Format("Contribución"), typeof(double)) ); // 3
            VariablesDT.Columns.Add( new DataColumn(String.Format("Minimo"), typeof(string)) ); // 4
            VariablesDT.Columns.Add( new DataColumn(String.Format("Maximo"), typeof(string)) ); // 5

            var Solucion = ReporteModelo.Solucion();
            var LimitesVariables = ReporteModelo.LimitesCoeficientesObjetivo();
            for (int i = 0; i < FuncionObjetivo.Count; ++i)
            {
                DataRow newRow = VariablesDT.NewRow();
                newRow[0] = FuncionObjetivo[i].Name;
                newRow[1] = Solucion[i].ToDouble();
                newRow[2] = FuncionObjetivo[i].Coef;
                newRow[3] = FuncionObjetivo[i].Coef * Solucion[i].ToDouble();
                newRow[4] = LimitesVariables[i].Item1.ToString();
                newRow[5] = LimitesVariables[i].Item2.ToString();

                VariablesDT.Rows.Add(newRow);
            }

            // Restricciones
            DataTable RestriccionesDT = new DataTable();
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Nombre"), typeof(string))); // 0
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Signo"), typeof(string))); // 1
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Lado B"), typeof(double))); // 2
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Holgura o Excedente"), typeof(double)));  // 3
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Dual"), typeof(double))); // 4
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Minimo"), typeof(string))); // 5
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Maximo"), typeof(string))); // 6

            var HolguraExcedente = ReporteModelo.HolguraExcedente();
            var Duales = ReporteModelo.DualRestricciones();
            var LimitesRestricciones = ReporteModelo.LimitesRestriccion();
            for (int i = 0; i < Restricciones.Count; ++i)
            {
                DataRow newRow = RestriccionesDT.NewRow();
                newRow[0] = Restricciones[i].Name;
                newRow[1] = Core.Signos.IdSignoDictionary[Restricciones[i]._sign];
                newRow[2] = Restricciones[i].Bside;
                Double temp = new Double();
                temp = HolguraExcedente[i];
                newRow[3] = temp;
                newRow[4] = Duales[i];
                newRow[5] = LimitesRestricciones[i].Item1.ToString();
                newRow[6] = LimitesRestricciones[i].Item2.ToString();

                RestriccionesDT.Rows.Add(newRow);
            }

            // Bindings
            DG_Variables.ItemsSource = VariablesDT.AsDataView();
            DG_Restricciones.ItemsSource = RestriccionesDT.AsDataView();
        }

        private void B_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
