using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace IO.Frames.Simplex
{
    /// <summary>
    /// Lógica de interacción para Modelo.xaml
    /// </summary>
    public partial class Modelo : Page
    {
        public ObservableCollection<Core.MiembroFo> ListFO { get; set; }
        public ObservableCollection<Core.Restriction> ListRest { get; set; }
        public DataTable RestriccionesDT { get; set; }

        private int totalVariables, totalRestricciones;

        public Modelo(int restricciones, int variables)
        {
            InitializeComponent();
            totalVariables = variables;
            totalRestricciones = restricciones;

            ListFO = new ObservableCollection<Core.MiembroFo>();
            RestriccionesDT = new DataTable();

            DG_FO.ItemsSource = ListFO;

            for (int i = 0; i < variables; ++i)
                ListFO.Add( new Core.MiembroFo() );


            for (int i = 0; i < totalVariables; ++i)
            {
                DataColumn columna = new DataColumn(String.Format("Variable {0}", i + 1), typeof(double));
                RestriccionesDT.Columns.Add(columna);
            }

            RestriccionesDT.Columns.Add(new DataColumn("Signo", typeof(string)));
            RestriccionesDT.Columns.Add(new DataColumn("Lado B", typeof(double)));

            for (int i = 0; i < totalRestricciones; ++i)
            {
                DataRow newRow = RestriccionesDT.NewRow();
                for (int j = 0; j < totalVariables; ++j)
                    newRow[j] = 0;

                newRow[totalVariables + 1] = 0;
                RestriccionesDT.Rows.Add(newRow);

            }

            DG_Rest.ItemsSource = RestriccionesDT.AsDataView();

        }

        private void DG_FO_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = "x" + (e.Row.GetIndex() + 1).ToString();
        }

        private void B_ValoresDefecto_Click(object sender, RoutedEventArgs e)
        {
            ListFO = new ObservableCollection<Core.MiembroFo>();
            Core.MiembroFo defecto = new Core.MiembroFo();
            for (int i = 0; i < totalVariables; ++i)
            {
                defecto = new Core.MiembroFo();
                defecto.Name = "Variable " + (i + 1).ToString();
                ListFO.Add(defecto);
            }
            DG_FO.ItemsSource = ListFO;
        }

        private void B_Reestablecer_Click(object sender, RoutedEventArgs e)
        {
            ListFO = new ObservableCollection<Core.MiembroFo>();

            DG_FO.ItemsSource = ListFO;

            for (int i = 0; i < totalVariables; ++i)
                ListFO.Add(new Core.MiembroFo());
        }

        private void DG_FO_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.EditingElement;
            string newText = string.Empty;

            if (dep is TextBox)
            {
                TextBox txt = dep as TextBox;
                if (txt.Text != "") newText = txt.Text;
                else newText = string.Empty;
            }
     
            int columnIndex = -1, rowIndex = -1;
            while ((dep != null) && !(dep is DataGridCell))
                dep = VisualTreeHelper.GetParent(dep);

            if (dep == null)  return;

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;
                while ((dep != null) && !(dep is DataGridRow))
                    dep = VisualTreeHelper.GetParent(dep);

                if (dep == null) return;

                DataGridRow row = dep as DataGridRow;
                rowIndex = row.GetIndex();
                columnIndex = cell.Column.DisplayIndex;
            }

            if (columnIndex == 0 && rowIndex != -1 && newText != "")
                DG_Rest.Columns[rowIndex].Header = newText;
        }

        private void DG_Rest_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.EditingElement;
            string newText = string.Empty;

            if (dep is TextBox)
            {
                TextBox txt = dep as TextBox;
                if (txt.Text != "") newText = txt.Text;
                else newText = string.Empty;
            }

            int columnIndex = -1, rowIndex = -1;
            while ((dep != null) && !(dep is DataGridCell))
                dep = VisualTreeHelper.GetParent(dep);

            if (dep == null) return;

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;
                while ((dep != null) && !(dep is DataGridRow))
                    dep = VisualTreeHelper.GetParent(dep);

                if (dep == null) return;

                DataGridRow row = dep as DataGridRow;
                rowIndex = row.GetIndex();
                columnIndex = cell.Column.DisplayIndex;
            }

            // Indice de 'Signo'
            Core.Signo signoUsado;
            if (columnIndex == totalRestricciones)
            {
                if (!Core.Signos.SignosDictionary.TryGetValue(newText, out signoUsado))
                {
                    MessageBox.Show("Signo invalido", "Error", MessageBoxButton.OK);
                    RestriccionesDT.Rows[rowIndex][columnIndex] = "";
                }
            }
        }

        private void B_ReestablecerRestricciones_Click(object sender, RoutedEventArgs e)
        {
            RestriccionesDT.Clear();
            for (int i = 0; i < totalRestricciones; ++i)
            {
                DataRow newRow = RestriccionesDT.NewRow();
                for (int j = 0; j < totalVariables; ++j)
                    newRow[j] = 0;

                newRow[totalVariables + 1] = 0;
                RestriccionesDT.Rows.Add(newRow);

            }
        }

        private void DG_Rest_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = "Restricción " + (e.Row.GetIndex() + 1).ToString() + ":";
            e.Row.Height = 25;
        }
        private void B_GenerarReporte_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
