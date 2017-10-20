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
        Frames.Inicio F_Inicio;
        Frames.Simplex F_Simplex;
        Frames.Grafico F_Grafico;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void B_Simplex_Click(object sender, RoutedEventArgs e)
        {
            if (F_Simplex == null) F_Simplex = new Frames.Simplex();
            F_Vista.Content = F_Simplex;
        }

        private void B_Inicio_Click(object sender, RoutedEventArgs e)
        {
            if (F_Simplex == null) F_Inicio = new Frames.Inicio();
            F_Vista.Content = F_Inicio;
        }

        private void B_Grafico_Click(object sender, RoutedEventArgs e)
        {
            if (F_Simplex == null) F_Grafico = new Frames.Grafico();
            F_Vista.Content = F_Grafico;
        }
    }
}
