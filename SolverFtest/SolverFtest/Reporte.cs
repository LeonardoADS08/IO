using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
using Microsoft.SolverFoundation.Services;


namespace SolverFtest
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
            return (double) _sollutions.Solver.GetValue(_sollutions._z);
        }
        public List<double> Report_Variables()
        {
            List<double> aux=new List<double>();
            
            foreach (MiembroFo t in _sollutions.FO)
            {
                aux.Add(_sollutions.Solver.GetValue(t._value).ToDouble());
            }
            return aux;
        }

        public List<double> Report_Restriction_Variables()
        {
            List<double> aux = new List<double>();
            foreach (Restriction t in _sollutions.Res)
            {
                aux.Add(_sollutions.Solver.GetValue(t._value).ToDouble()-t.Bside);
            }
            return aux;
        }

        public List<Rational> Report_Variable_Limits(bool k,int a)
        {
            List < Rational > fin= new List<Rational>();
            if (a == 1)
            {
                if (k == true)
                {
                    foreach (MiembroFo t in _sollutions.FO)
                    {
                        fin.Add((Rational) _sensitivityReport.GetObjectiveCoefficientRange(t._value,1).Lower);
                    }
                }
                else
                {
                    foreach (MiembroFo t in _sollutions.FO)
                    {
                        fin.Add((Rational) _sensitivityReport.GetObjectiveCoefficientRange(t._value, 1).Upper);
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

        public List<double> Report_Constrain_RC()
        {
            List<double> fin = new List<double>();
            foreach (Restriction t in _sollutions.Res)
            {
                fin.Add(_sensitivityReport.GetDualValue(t._value).ToDouble());
            }
           
            return fin;
        }
        
    }


}
