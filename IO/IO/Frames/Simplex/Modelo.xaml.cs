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
        public List<Core.Restriction> ListRest { get; set; }
        public DataTable RestriccionesDT { get; set; }

        private int totalVariables, totalRestricciones;

        public Modelo(int restricciones, int variables)
        {
            InitializeComponent();
            try
            {
                // Se guardan la cantidad de restricciones y variables
                totalVariables = variables;
                totalRestricciones = restricciones;

                // Se instancian los objetos que van a guardar los datos
                ListFO = new ObservableCollection<Core.MiembroFo>();
                ListRest = new List<Core.Restriction>();
                RestriccionesDT = new DataTable();

                // Se hace los binding
                DG_FO.ItemsSource = ListFO;
                DG_Rest.ItemsSource = RestriccionesDT.AsDataView();

                // Se crea la cantidad de variables que se pidio
                ReiniciarVariables();

                // Se crea la cantidad de restricciones que se pidio
                RestriccionesDT.Columns.Add(new DataColumn("Nombre", typeof(string)));
                for (int i = 0; i < totalVariables; ++i)
                {
                    DataColumn columna = new DataColumn(String.Format("Variable {0}", i + 1), typeof(double));
                    RestriccionesDT.Columns.Add(columna);
                }

                RestriccionesDT.Columns.Add(new DataColumn("Signo", typeof(string)));
                RestriccionesDT.Columns.Add(new DataColumn("Lado B", typeof(double)));

                // Se inicializa los valores de las restricciones
                ReiniciarRestricciones();

                // Se le da valor al ComboBox
                CB_TipoModelo.DisplayMemberPath = "Key";
                CB_TipoModelo.SelectedValuePath = "Value";
                CB_TipoModelo.Items.Add(new KeyValuePair<string, int>("Maximizar", 0));
                CB_TipoModelo.Items.Add(new KeyValuePair<string, int>("Minimizar", 1));
                CB_TipoModelo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            }
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
            try
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

                if (columnIndex == 0 && rowIndex != -1 && newText != "")
                    DG_Rest.Columns[rowIndex + 1].Header = newText;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void DG_Rest_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
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
                if (columnIndex == totalVariables + 1)
                {
                    if (newText == "") return;
                    if (!Core.Signos.SignosDictionary.TryGetValue(newText, out signoUsado))
                    {
                        MessageBox.Show("Signo invalido", "Error", MessageBoxButton.OK);
                        RestriccionesDT.Rows[rowIndex][columnIndex] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void B_ReestablecerRestricciones_Click(object sender, RoutedEventArgs e) => ReiniciarRestricciones();

        private void DG_Rest_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = "Restricción " + (e.Row.GetIndex() + 1).ToString() + ":";
            e.Row.Height = 25;
        }

        private void B_GenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int TipoModelo = (int)CB_TipoModelo.SelectedValue;
                ListRest.Clear();
                for (int i = 0; i < totalRestricciones; ++i)
                {
                    ListRest.Add(new Core.Restriction());
                    ListRest[i].Name = (string)RestriccionesDT.Rows[i][0];
                    ListRest[i].Bside = (double)RestriccionesDT.Rows[i][totalVariables + 2];
                    // Verificando que no falte un signo
                    if ((string)RestriccionesDT.Rows[i][totalVariables + 1] == "")
                    {
                        MessageBox.Show("Falta indicar un signo en una restricción.", "Error", MessageBoxButton.OK);
                        return;
                    }
                    ListRest[i]._sign = Core.Signos.SignosDictionary[(string)RestriccionesDT.Rows[i][totalVariables + 1]];
                    for (int j = 1; j <= totalVariables; ++j)
                        ListRest[i].Coef.Add((double)RestriccionesDT.Rows[i][j]);

                }

                // Verificamos que no falten datos fundamentales
                // Funcion objetivo
                // Nombres
                foreach (var val in ListFO)
                {
                    if (val.Name == "")
                    {
                        MessageBox.Show("Falta nombrar una variable.", "Error", MessageBoxButton.OK);
                        return;
                    }
                }

                // Restricciones
                // Verificando que no falte un nombre
                foreach (var val in ListRest)
                {
                    if (val.Name == "")
                    {
                        MessageBox.Show("Falta nombrar una restricción.", "Error", MessageBoxButton.OK);
                        return;
                    }     
                }

                Frames.Simplex.Reporte Reporte = new Reporte(ListFO.ToList(), ListRest, TipoModelo);
                Reporte.Show();
            }
            catch (Exception ex)
            {
                if ( MessageBox.Show("No se ha podido generar el reporte. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            } 
        }



        // Funciones extras
        private void ReiniciarRestricciones()
        {
            RestriccionesDT.Clear();
            for (int i = 0; i < totalRestricciones; ++i)
            {
                DataRow newRow = RestriccionesDT.NewRow();
                newRow[0] = "";
                newRow[totalVariables + 1] = "";
                for (int j = 1; j <= totalVariables; ++j)
                    newRow[j] = 0;

                newRow[totalVariables + 2] = 0;
                RestriccionesDT.Rows.Add(newRow);
            }
        }

        private void ReiniciarVariables()
        {
            ListFO.Clear();
            for (int i = 0; i < totalVariables; ++i)
            {
                ListFO.Add(new Core.MiembroFo());
                ListFO[i].Name = "";
            }
        }
    }
}
