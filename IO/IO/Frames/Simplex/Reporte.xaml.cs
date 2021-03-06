﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace IO.Frames.Simplex
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {
        public List<Core.MiembroFuncionObjetivo> FuncionObjetivo { get; set; }
        public List<Core.Restriction> Restricciones { get; set; }
        public Core.Simplex Simplex { get; set; }
        public Core.Reporte ReporteModelo { get; set; }
        public DataTable VariablesDT { get; set; }

        public Reporte(List<Core.MiembroFuncionObjetivo> FO, List<Core.Restriction> Rest, int Objetivo)
        {
            InitializeComponent();

            FuncionObjetivo = FO;
            Restricciones = Rest;

            Simplex = new Core.Simplex(FuncionObjetivo, Objetivo);
            Simplex.AddRestriction(Restricciones);
            ReporteModelo = new Core.Reporte(Simplex);

            // Informacion básica
            L_ValorObjetivo.Content += " " + ReporteModelo.ObtenerZ().ToString();
            L_Restricciones.Content += " " + Rest.Count;
            L_Variables.Content += " " + FO.Count;

            // Variables
            DataTable VariablesDT = new DataTable();
            VariablesDT.Columns.Add(new DataColumn(String.Format("Nombre"), typeof(string))); // 0
            VariablesDT.Columns.Add(new DataColumn(String.Format("Valor"), typeof(double))); // 1
            VariablesDT.Columns.Add(new DataColumn(String.Format("Coeficiente"), typeof(double))); // 2
            VariablesDT.Columns.Add(new DataColumn(String.Format("Contribución"), typeof(double))); // 3
            VariablesDT.Columns.Add(new DataColumn(String.Format("Minimo"), typeof(string))); // 4
            VariablesDT.Columns.Add(new DataColumn(String.Format("Maximo"), typeof(string))); // 5

            var Solucion = ReporteModelo.Solucion();
            var LimitesVariables = ReporteModelo.LimitesCoeficientesObjetivo();
            for (int i = 0; i < FuncionObjetivo.Count; ++i)
            {
                DataRow newRow = VariablesDT.NewRow();
                newRow[0] = FuncionObjetivo[i].Nombre;
                newRow[1] = Solucion[i].ToDouble();
                newRow[2] = FuncionObjetivo[i].Coeficiente;
                newRow[3] = FuncionObjetivo[i].Coeficiente * Solucion[i].ToDouble();
                newRow[4] = LimitesVariables[i].Item1.ToString();
                newRow[5] = LimitesVariables[i].Item2.ToString();

                VariablesDT.Rows.Add(newRow);
            }

            // Restricciones
            DataTable RestriccionesDT = new DataTable();
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Nombre"), typeof(string))); // 0
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Signo"), typeof(string))); // 1
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Lado B"), typeof(double))); // 2
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Holgura o Excedente"), typeof(double)));  // 3
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Dual"), typeof(double))); // 4
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Minimo"), typeof(string))); // 5
            RestriccionesDT.Columns.Add(new DataColumn(String.Format("Maximo"), typeof(string))); // 6

            var HolguraExcedente = ReporteModelo.HolguraExcedente();
            var Duales = ReporteModelo.DualRestricciones();
            var LimitesRestricciones = ReporteModelo.LimitesRestriccion();
            for (int i = 0; i < Restricciones.Count; ++i)
            {
                DataRow newRow = RestriccionesDT.NewRow();
                newRow[0] = Restricciones[i].Nombre;
                newRow[1] = Core.Signos.IdSignoDictionary[Restricciones[i].Signo];
                newRow[2] = Restricciones[i].LadoB;
                Double temp = new Double();
                temp = HolguraExcedente[i];
                newRow[3] = temp;
                newRow[4] = Duales[i];
                newRow[5] = LimitesRestricciones[i].Item1.ToString();
                newRow[6] = LimitesRestricciones[i].Item2.ToString();

                RestriccionesDT.Rows.Add(newRow);
            }

            // Bindings
            DG_Variables.ItemsSource = VariablesDT.AsDataView();
            DG_Restricciones.ItemsSource = RestriccionesDT.AsDataView();
        }

        private void B_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}