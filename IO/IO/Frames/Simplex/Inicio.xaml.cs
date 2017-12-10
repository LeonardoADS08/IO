using System;
using System.Windows;

namespace IO.Frames.Simplex
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        private bool _estado;
        private int restricciones, variables;

        public bool Estado { get => _estado; set => _estado = value; }
        public int Restricciones { get => restricciones; set => restricciones = value; }
        public int Variables { get => variables; set => variables = value; }

        public Inicio()
        {
            InitializeComponent();
            _estado = false;
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
                _estado = true;
                this.Close();
            }
            else MessageBox.Show(Utils.ErrorList.CantConvertToInt32, "Error en conversión.", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}