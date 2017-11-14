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
    /// Lógica de interacción para Modelo.xaml
    /// </summary>
    public partial class Modelo : Page
    {
        public Modelo(int restricciones, int variables)
        {
            InitializeComponent();

            DataGridTextColumn Nombre = new DataGridTextColumn();
            Nombre.Header = "Nombre de la variable";
            Nombre.Binding = new Binding("Name");
            Nombre.Width = 350;
            DG_FO.Columns.Add(Nombre);

            DataGridTextColumn Coeficiente = new DataGridTextColumn();
            Coeficiente.Header = "Coeficiente";
            Coeficiente.Binding = new Binding("Coef");
            Coeficiente.Width = 225;
            DG_FO.Columns.Add(Coeficiente);

            for (int i = 0; i < variables; ++i)
            {
           //     dataGrid1.Items.Add();
            }
        }
    }
}
