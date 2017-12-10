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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IO
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Se instancian las ventanas como propiedades de la ventana para cachearlas y no perder los dato al cambiar entre ventana
        // Estas ventanas solo van a instanciarse al haber abierto por lo menos una vez la vista.
        Frames.Inicio F_Inicio = new Frames.Inicio();

        Frames.Simplex.Inicio F_Simplex_Inicio;
        Frames.Simplex.Modelo F_Simplex_Modelo;

        Frames.Transporte.Inicio F_Transporte_Inicio;
        Frames.Transporte.MatrizTransporte F_Transporte_MatrizTransporte;


        public MainWindow()
        {
            InitializeComponent();
            F_Vista.Content = F_Inicio;
        }

        #region Simplex
        // Para abrir Simplex, primero se abre un popup para indicar el numero de variables y restricciones, cuando este se termine correctamente, 
        // se abrirá la ventana de modelado.
        private void B_Simplex_Click(object sender, RoutedEventArgs e)
        {
            if (F_Simplex_Inicio != null && F_Simplex_Inicio.ShowActivated)
            {
                F_Simplex_Inicio.Activate();
                return;
            }
            F_Simplex_Inicio = new Frames.Simplex.Inicio();
            F_Simplex_Inicio.Show();
            F_Simplex_Inicio.Closed += new EventHandler(CargarSimplex);
        }

        private void CargarSimplex(object sender, EventArgs e)
        {
            if (!F_Simplex_Inicio.Estado)
            {
                F_Simplex_Inicio = null;
                return;
            }

            if (F_Simplex_Modelo == null)
            {
                F_Simplex_Modelo = new Frames.Simplex.Modelo(F_Simplex_Inicio.Restricciones, F_Simplex_Inicio.Variables);
                F_Vista.Content = F_Simplex_Modelo;
                F_Simplex_Inicio = null;
                return;
            }
            if (F_Simplex_Inicio.Estado)
            {
                MessageBoxResult result = MessageBox.Show("Ya existe un modelo creado, ¿Desea recuperarlo?",
                                                        "Esperando confirmación",
                                                        MessageBoxButton.YesNoCancel,
                                                        MessageBoxImage.Information,
                                                        MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    F_Vista.Content = F_Simplex_Modelo;
                    F_Simplex_Inicio = null;
                    return;
                }
                else if (result == MessageBoxResult.No)
                {
                    F_Simplex_Modelo = new Frames.Simplex.Modelo(F_Simplex_Inicio.Restricciones, F_Simplex_Inicio.Variables);
                    F_Vista.Content = F_Simplex_Modelo;
                    F_Simplex_Inicio = null;
                    return;
                }
                else
                {
                    F_Simplex_Inicio = null;
                    return;
                }
            }
        }

        #endregion

        private void B_Inicio_Click(object sender, RoutedEventArgs e)
        {
            F_Vista.Content = F_Inicio;
        }

        #region Transporte
        private void B_Transporte_Click(object sender, RoutedEventArgs e)
        {
            if (F_Transporte_Inicio != null && F_Transporte_Inicio.ShowActivated)
            {
                F_Transporte_Inicio.Activate();
                return;
            }
            F_Transporte_Inicio = new Frames.Transporte.Inicio();
            F_Transporte_Inicio.Show();
            F_Transporte_Inicio.Closed += new EventHandler(CargarTransporte);
        }

        private void CargarTransporte(object sender, EventArgs e)
        {
            if (!F_Transporte_Inicio.Estado)
            {
                F_Transporte_Inicio = null;
                return;
            }

            if (F_Transporte_MatrizTransporte == null)
            {
                F_Transporte_MatrizTransporte = new Frames.Transporte.MatrizTransporte(F_Transporte_Inicio.Ofertantes, F_Transporte_Inicio.Demandantes);
                F_Vista.Content = F_Transporte_MatrizTransporte;
                F_Transporte_Inicio = null;
                return;
            }

            if (F_Transporte_Inicio.Estado)
            {
                MessageBoxResult result = MessageBox.Show("Ya existe una matriz de costos creada, ¿Desea recuperarla?",
                                                        "Esperando confirmación",
                                                        MessageBoxButton.YesNoCancel,
                                                        MessageBoxImage.Information,
                                                        MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    F_Vista.Content = F_Transporte_MatrizTransporte;
                    F_Transporte_Inicio = null;
                    return;
                }
                else if (result == MessageBoxResult.No)
                {
                    F_Transporte_MatrizTransporte = new Frames.Transporte.MatrizTransporte(F_Transporte_Inicio.Ofertantes, F_Transporte_Inicio.Demandantes);
                    F_Vista.Content = F_Transporte_MatrizTransporte;
                    F_Transporte_Inicio = null;
                    return;
                }
                else
                {
                    F_Transporte_Inicio = null;
                    return;
                }
            }
        }
        #endregion


    }
}
