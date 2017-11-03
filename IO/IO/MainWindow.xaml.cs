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
        Frames.Grafico F_Grafico;

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
            if (!F_Simplex_Inicio.Estado) return;

            if (F_Simplex_Modelo == null)
            {
                F_Simplex_Modelo = new Frames.Simplex.Modelo(F_Simplex_Inicio.Restricciones, F_Simplex_Inicio.Variables);
                F_Vista.Content = F_Simplex_Modelo;
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
                    return;
                }
                else if (result == MessageBoxResult.No)
                {
                    F_Simplex_Modelo = new Frames.Simplex.Modelo(F_Simplex_Inicio.Restricciones, F_Simplex_Inicio.Variables);
                    F_Vista.Content = F_Simplex_Modelo;
                    return;
                }
                else return;
            }
            
            
        }

        #endregion

        private void B_Inicio_Click(object sender, RoutedEventArgs e)
        {
            F_Vista.Content = F_Inicio;
        }

        private void B_Grafico_Click(object sender, RoutedEventArgs e)
        {
            if (F_Simplex_Inicio == null) F_Grafico = new Frames.Grafico();
            F_Vista.Content = F_Grafico;
        }
    }
}
