using Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IO.Frames.Transporte
{
    /// <summary>
    /// Lógica de interacción para MatrizTransporte.xaml
    /// </summary>
    public partial class MatrizTransporte : Page
    {
        public int TotalOfertantes { get; set; }
        public int TotalDemandantes { get; set; }
        public DataTable MatrizCostosDT { get; set; }
        private dynamic UltimaCelda { get; set; }

        public MatrizTransporte(int ofertantes, int demandantes)
        {
            try
            {
                InitializeComponent();

                // Inicializando variables
                TotalOfertantes = ofertantes;
                TotalDemandantes = demandantes;
                MatrizCostosDT = new DataTable();

                // Bindings
                DG_Matriz.DataContext = MatrizCostosDT;

                // Construyendo el DataTable de la matriz de costos
                for (int i = 0; i < TotalDemandantes; ++i)
                    MatrizCostosDT.Columns.Add(new DataColumn(String.Format("Demandante {0}", i + 1), typeof(double)));

                MatrizCostosDT.Columns.Add(new DataColumn("Oferta", typeof(double)));

                // El valor de la ultima celda al inicio es vacio
                UltimaCelda = DBNull.Value;

                // Inicializa las filas
                ReiniciarMatriz();

                // ------ //
                // Mensajes de información estaticos
                L_Demandantes.Content = "Demandantes: " + TotalDemandantes.ToString();
                L_Ofertantes.Content = "Ofertantes: " + TotalOfertantes.ToString();

                // ------ //
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

        private void ReiniciarMatriz()
        {
            try
            {
                MatrizCostosDT.Clear();
                for (int i = 0; i < TotalOfertantes + 1; ++i)
                {
                    DataRow newRow = MatrizCostosDT.NewRow();
                    for (int j = 0; j < TotalDemandantes + 1; ++j)
                        newRow[j] = 0;

                    MatrizCostosDT.Rows.Add(newRow);
                }
                MatrizCostosDT.Rows[TotalOfertantes][TotalDemandantes] = UltimaCelda;
                ActualizarInformacionMatriz();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void DG_Matriz_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                int fila = e.Row.GetIndex();
                e.Row.Height = 25;
                if (fila == TotalOfertantes)
                    e.Row.Header = "Demanda:";
                else
                    e.Row.Header = "Ofertante " + (e.Row.GetIndex() + 1).ToString() + ":";
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void DG_Matriz_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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

                // La ultima columna de la ultima fila no se puede editar...
                if (rowIndex == TotalOfertantes && columnIndex == TotalDemandantes)
                    MatrizCostosDT.Rows[rowIndex][columnIndex] = UltimaCelda;

                // Se actuliza la informacion previa
                //CargarDatosMatriz();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void ActualizarInformacionMatriz()
        {
            try
            {
                double totalOferta = CalcularTotalOferta(), totalDemanda = CalcularTotalDemanda();

                L_TotalDemanda.Content = "Demanda total: " + totalDemanda.ToString();
                L_TotalOferta.Content = "Oferta total: " + totalOferta.ToString();
                L_Diferencia.Content = "Diferencia O-D: " + (totalOferta - totalDemanda).ToString();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void DG_Matriz_CurrentCellChanged(object sender, EventArgs e) => ActualizarInformacionMatriz();

        private void Button_Click(object sender, RoutedEventArgs e) => ReiniciarMatriz();

        private double CalcularTotalDemanda()
        {
            try
            {
                double totalDemanda = 0, temp;
                for (int i = 0; i < TotalDemandantes; ++i)
                {
                    if (!Double.TryParse(MatrizCostosDT.Rows[TotalOfertantes][i].ToString(), out temp)) return 0;
                    totalDemanda += temp;
                }
                return totalDemanda;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
                return 0;
            }
        }

        private double CalcularTotalOferta()
        {
            try
            {
                double totalOferta = 0, temp;
                for (int i = 0; i < TotalOfertantes; ++i)
                {
                    if (!Double.TryParse(MatrizCostosDT.Rows[i][TotalDemandantes].ToString(), out temp)) return 0;
                    totalOferta += temp;
                }
                return totalOferta;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
                return 0;
            }
        }

        private double CalcularDemanda(int demandante)
        {
            double totalDemanda = 0, temp;
            if (!Double.TryParse(MatrizCostosDT.Rows[TotalOfertantes][demandante++].ToString(), out temp)) return 0;
            totalDemanda = temp;
            return temp;
        }

        private double CalcularOferta(int ofertante)
        {
            double totalOferta = 0, temp;
            if (!Double.TryParse(MatrizCostosDT.Rows[ofertante][TotalDemandantes].ToString(), out temp)) return 0;
            totalOferta = temp;
            return totalOferta;
        }

        private void B_GenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Se verifica si se ha activado alguna restricción
                bool PriorizarOfertante = (bool)CB_PriorizarOfertante.IsChecked;
                bool PriorizarDemandante = (bool)CB_PriorizarDemandante.IsChecked;
                bool Multas = (bool)CB_Multas.IsChecked;
                bool Bonos = (bool)CB_Bonos.IsChecked;

                if (PriorizarOfertante) throw new Exception("No implementado");
                if (PriorizarDemandante) throw new Exception("No implementado");
                if (Multas) throw new Exception("No implementado");
                if (Bonos) throw new Exception("No implementado");

                // Modelado del problema sin restricciones
                // Funcion objetivo
                List<MiembroFuncionObjetivo> FuncionObjetivo = new List<MiembroFuncionObjetivo>();

                // La cantidad de miembros de la funcion objetivo es igual la cantidad de ofertantes por la cantidad de demandantes.
                FuncionObjetivo.Capacity = TotalDemandantes * TotalDemandantes;

                // La funcion objetivo esta compuesta básicamente por los terminos en la matriz de costos.
                for (int i = 0; i < TotalDemandantes; ++i)
                {
                    for (int j = 0; j < TotalOfertantes; ++j)
                    {
                        MiembroFuncionObjetivo miembro = new MiembroFuncionObjetivo();
                        miembro.Nombre = String.Format("Ofertante {0} -> Demandante {1}", j + 1, i + 1);
                        miembro.Coeficiente = (double)MatrizCostosDT.Rows[j][i];

                        FuncionObjetivo.Add(miembro);
                    }
                }

                // Restricciones
                List<Restriction> Restricciones = new List<Restriction>();

                // Restricciones de oferta
                for (int i = 0; i < TotalOfertantes; ++i)
                {
                    Restriction restriccionOferta = new Restriction();
                    restriccionOferta.LadoB = CalcularOferta(i);
                    restriccionOferta.Nombre = String.Format("Ofertante {0}", i + 1);
                    restriccionOferta.Signo = Signo.MenorIgualQue;

                    // Coeficientes de la restricción
                    // Deben tener 1 los coeficientes que le corresponde a la oferta 'i' y 0 los que no
                    restriccionOferta.Coeficientes.Capacity = TotalDemandantes * TotalOfertantes;
                    for (int j = 0; j < restriccionOferta.Coeficientes.Capacity; ++j) restriccionOferta.Coeficientes.Add(0D);
                    for (int j = 0; j < TotalDemandantes; ++j)
                        restriccionOferta.Coeficientes[j * TotalOfertantes + i] = 1;

                    Restricciones.Add(restriccionOferta);
                }

                // Restricciones de demanda
                for (int i = 0; i < TotalDemandantes; ++i)
                {
                    Restriction restriccionDemanda = new Restriction();
                    restriccionDemanda.LadoB = CalcularDemanda(i);
                    restriccionDemanda.Nombre = String.Format("Demandante {0}", i + 1);
                    restriccionDemanda.Signo = Signo.MenorIgualQue;

                    // Coeficientes de la restricción
                    // Deben tener 1 los coeficientes que le corresponde a la demanda 'i' y 0 los que no
                    restriccionDemanda.Coeficientes.Capacity = TotalDemandantes * TotalOfertantes;
                    for (int j = 0; j < restriccionDemanda.Coeficientes.Capacity; ++j) restriccionDemanda.Coeficientes.Add(0D);
                    for (int j = 0; j < TotalOfertantes; ++j)
                        restriccionDemanda.Coeficientes[j + i * TotalOfertantes] = 1;

                    Restricciones.Add(restriccionDemanda);
                }

                // Restriccion de disponibilidad oferta/demadna
                {
                    double demandaTotal = CalcularTotalDemanda(), ofertaTotal = CalcularTotalOferta();
                    Restriction restriccionDisponibilidad = new Restriction();

                    restriccionDisponibilidad.Nombre = "Disponibilidad Oferta/Demanda";
                    restriccionDisponibilidad.LadoB = (demandaTotal < ofertaTotal) ? demandaTotal : ofertaTotal;
                    restriccionDisponibilidad.Signo = Signo.Igual;
                    restriccionDisponibilidad.Coeficientes.Capacity = TotalDemandantes * TotalOfertantes;
                    for (int j = 0; j < restriccionDisponibilidad.Coeficientes.Capacity; ++j) restriccionDisponibilidad.Coeficientes.Add(1D);

                    Restricciones.Add(restriccionDisponibilidad);
                }

                // Se obtiene el tipo de modelo
                int TipoModelo = (int)CB_TipoModelo.SelectedValue;

                // Se obtiene un resumen de las demandas y ofertas
                List<double> Demandas = new List<double>();
                for (int i = 0; i < TotalDemandantes; ++i)
                    Demandas.Add(CalcularDemanda(i));

                List<double> Ofertas = new List<double>();
                for (int i = 0; i < TotalOfertantes; ++i)
                    Ofertas.Add(CalcularOferta(i));

                Transporte.Reporte Reporte = new Transporte.Reporte(FuncionObjetivo, Restricciones, TipoModelo, TotalOfertantes, TotalDemandantes, Demandas, Ofertas);
                Reporte.Show();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Error de ejecución. \n ¿Ver excepción?", "Error", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    MessageBox.Show(ex.Message, "Exception");
            }
        }
    }
}