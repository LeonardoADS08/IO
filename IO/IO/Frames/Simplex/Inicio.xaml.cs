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

namespace IO.Frames.Simplex
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        
        private bool estado;
        private int restricciones, variables;

        public bool Estado { get => estado; set => estado = value; }
        public int Restricciones { get => restricciones; set => restricciones = value; }
        public int Variables { get => variables; set => variables = value; }

        public Inicio()
        {
            InitializeComponent();
            estado = false;
        }

        private void B_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void B_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            
            if (Int32.TryParse(TB_Restricciones.Text, out restricciones) &&
                Int32.TryParse(TB_Variables.Text, out variables))
            {
                estado = true;
                this.Close();
            }
            else MessageBox.Show(Utils.ErrorList.CantConvertToInt32, "Error en conversión.", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
