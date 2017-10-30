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

namespace IO.Frames
{
    /// <summary>
    /// Lógica de interacción para Simplex.xaml
    /// </summary>
    public partial class Simplex : Page
    {
        public Simplex()
        {
            InitializeComponent();
        }

        private void B_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            int restricciones, variables;
            if (Int32.TryParse(TB_Restricciones.Text, out restricciones) &&
                Int32.TryParse(TB_Restricciones.Text, out variables))
            {

            }
            else MessageBox.Show(Utils.ErrorList.CantConvertToInt32, "Error en conversión.", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
