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

namespace IO.Frames.Simplex
{
    /// <summary>
    /// Lógica de interacción para Modelo.xaml
    /// </summary>
    public partial class Modelo : Page
    {
        public ObservableCollection<Core.MiembroFo> ListFO { get; set; }
        public ObservableCollection<Core.Restriction> ListRest { get; set; }

        private int totalVariables, totalRestricciones;
        public Modelo(int restricciones, int variables)
        {
            InitializeComponent();
            totalVariables = variables;
            totalRestricciones = restricciones;

            ListFO = new ObservableCollection<Core.MiembroFo>();

            DG_FO.ItemsSource = ListFO;

            for (int i = 0; i < variables; ++i)
                ListFO.Add( new Core.MiembroFo() );

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

        private void DG_Rest_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = "Restricción " + (e.Row.GetIndex() + 1).ToString() + ":";
        }


    }
}
