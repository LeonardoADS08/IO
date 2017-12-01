using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using Microsoft.SolverFoundation.Services;


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

        public double LeZ()
        {
            return (double)_sollutions.Solver.GetValue(_sollutions._z);
        }
        // Coeficientes de la solución
        public List<double> Report_Variables()
        {
            List<double> aux = new List<double>();

            foreach (MiembroFo t in _sollutions.FO)
            {
                aux.Add(_sollutions.Solver.GetValue(t._value).ToDouble());
            }
            return aux;
        }

        //  Holgura, excedente
        public List<double> Report_Restriction_Variables()
        {
            List<double> aux = new List<double>();
            foreach (Restriction t in _sollutions.Res)
            {
                aux.Add(_sollutions.Solver.GetValue(t._value).ToDouble() - t.Bside);
            }
            return aux;
        }

        // Limite de las variables (true = Minimo, false = Maximo) (1 = Coeficiente FO, ? = Restricciones)
        public List<Rational> Report_Variable_Limits(bool k, int a)
        {
            List<Rational> fin = new List<Rational>();
            if (a == 1)
            {
                if (k == true)
                {
                    foreach (MiembroFo t in _sollutions.FO)
                    {
                        fin.Add((Rational)_sensitivityReport.GetObjectiveCoefficientRange(t._value, 1).Lower);
                    }
                }
                else
                {
                    foreach (MiembroFo t in _sollutions.FO)
                    {
                        fin.Add((Rational)_sensitivityReport.GetObjectiveCoefficientRange(t._value, 1).Upper);
                    }
                }
            }
            else
            {
                if (k == true)
                {
                    foreach (Restriction t in _sollutions.Res)
                    {
                        fin.Add((Rational)_sensitivityReport.GetVariableRange(t._value).Lower);
                    }
                }
                else
                {
                    foreach (Restriction t in _sollutions.Res)
                    {
                        fin.Add((Rational)_sensitivityReport.GetVariableRange(t._value).Upper);
                    }
                }
            }
            return fin;
        }

        // Dual de las restricciones
        public List<double> Report_Constrain_RC()
        {
            List<double> fin = new List<double>();
            foreach (Restriction t in _sollutions.Res)
            {
                fin.Add(_sensitivityReport.GetDualValue(t._value).ToDouble());
            }
            return fin;
        }


        public Rational ObtenerZ() => _sollutions.Solver.GetValue(_sollutions._z);
        
        public List<Rational> Solucion()
        {
            List<Rational> resultado = new List<Rational>();
            foreach (MiembroFo val in _sollutions.FO)
            {
                resultado.Add(_sollutions.Solver.GetValue(val._value));
            }
            return resultado;
        }

        public List<double> HolguraExcedente()
        {
            List<double> resultado = new List<double>();
            foreach (Restriction val in _sollutions.Res)
            {
                resultado.Add(_sollutions.Solver.GetValue(val._value).ToDouble() - val.Bside);
            }
            return resultado;
        }

        // Minimo, Maximo
        public List<Tuple<Rational, Rational>> LimitesCoeficientesObjetivo()
        {
            List<Tuple<Rational, Rational>> resultado = new List<Tuple<Rational, Rational>>();
            foreach (MiembroFo val in _sollutions.FO)
            {
                Tuple<Rational, Rational> limites = new Tuple<Rational, Rational>(
                    _sensitivityReport.GetObjectiveCoefficientRange(val._value, 1).Lower,
                    _sensitivityReport.GetObjectiveCoefficientRange(val._value, 1).Upper);
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
                    _sensitivityReport.GetVariableRange(val._value).Lower,
                    _sensitivityReport.GetVariableRange(val._value).Upper);
                resultado.Add(limites);
            }
            return resultado;
        }

        public List<double> DualRestricciones()
        {
            List<double> resultado = new List<double>();
            foreach (Restriction t in _sollutions.Res)
            {
                resultado.Add(_sensitivityReport.GetDualValue(t._value).ToDouble() * -1);
            }
            return resultado;
        }


    }


}
