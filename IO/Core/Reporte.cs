using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Solvers;
using System;
using System.Collections.Generic;

namespace Core
{
    public class Reporte
    {
        private Simplex _sollutions;
        private ILinearSolverReport _reportSensitivity;
        private ILinearSolverSensitivityReport _sensitivityReport;

        public Reporte(Simplex sollutions)
        {
            _sollutions = sollutions;

            SimplexSolverParams solverParams = new SimplexSolverParams();
            solverParams.GetSensitivityReport = true;

            _sollutions.Solver.Solve(solverParams);

            _reportSensitivity = _sollutions.Solver.GetReport(LinearSolverReportType.Sensitivity);
            _sensitivityReport = _reportSensitivity as ILinearSolverSensitivityReport;
        }

        public Rational ObtenerZ() => _sollutions.Solver.GetValue(_sollutions._z);

        public List<Rational> Solucion()
        {
            List<Rational> resultado = new List<Rational>();
            foreach (MiembroFuncionObjetivo val in _sollutions.FO)
            {
                resultado.Add(_sollutions.Solver.GetValue(val._valor));
            }
            return resultado;
        }

        public List<double> HolguraExcedente()
        {
            List<double> resultado = new List<double>();
            foreach (Restriction val in _sollutions.Res)
            {
                resultado.Add(_sollutions.Solver.GetValue(val.HolguraExcedente).ToDouble() - val.LadoB);
            }
            return resultado;
        }

        // Minimo, Maximo
        public List<Tuple<Rational, Rational>> LimitesCoeficientesObjetivo()
        {
            List<Tuple<Rational, Rational>> resultado = new List<Tuple<Rational, Rational>>();
            foreach (MiembroFuncionObjetivo val in _sollutions.FO)
            {
                Tuple<Rational, Rational> limites = new Tuple<Rational, Rational>(
                    _sensitivityReport.GetObjectiveCoefficientRange(val._valor, 1).Lower,
                    _sensitivityReport.GetObjectiveCoefficientRange(val._valor, 1).Upper);
                resultado.Add(limites);
            }

            return resultado;
        }

        // Minimo, Maximo
        public List<Tuple<Rational, Rational>> LimitesRestriccion()
        {
            List<Tuple<Rational, Rational>> resultado = new List<Tuple<Rational, Rational>>();

            foreach (Restriction val in _sollutions.Res)
            {
                Tuple<Rational, Rational> limites = new Tuple<Rational, Rational>(
                    _sensitivityReport.GetVariableRange(val.HolguraExcedente).Lower,
                    _sensitivityReport.GetVariableRange(val.HolguraExcedente).Upper);
                resultado.Add(limites);
            }
            return resultado;
        }

        public List<double> DualRestricciones()
        {
            List<double> resultado = new List<double>();
            foreach (Restriction t in _sollutions.Res)
            {
                resultado.Add(_sensitivityReport.GetDualValue(t.HolguraExcedente).ToDouble() * -1);
            }
            return resultado;
        }
    }
}