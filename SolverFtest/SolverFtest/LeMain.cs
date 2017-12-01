using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Services;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Solvers;
namespace SolverFtest
{
    class LeMain
    {
        public static List<MiembroFo> LLenarLista()
        {
            List<MiembroFo> x = new List<MiembroFo>();
            Console.WriteLine("ingrese el numero de variables");
            int n;
        
            n = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
           
            while (n-- != 0)
            {    MiembroFo var=new MiembroFo();
                Console.WriteLine("nombre :");
                var.Name = Console.ReadLine();
                Console.WriteLine("coef:");
                var.Coef = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                var._value = 0;
                x.Add(var);
            }
            return x;
        }
        public static List<double> LLenarListadoble()
        {
            List<double> x = new List<double>();
            Console.WriteLine("ingrese el numero de variables");
            int n;

            n = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            while (n-- != 0)
            {
                double u = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                x.Add(u);
            }
            return x;
        }
        public static void MostrarFo(List<MiembroFo>x)
        {
            for (int i = 0; i < x.Count; i++)
            {
                Console.WriteLine(x[i].Name + "  = " + x[i].Coef + "X" + i);
            }
        }

        public static Restriction LLenarRestriccion(int n)
        {
           
            Restriction aux = new Restriction();
            Console.WriteLine("ingrese el nombre de la restriccion");
            aux.Name = Console.ReadLine();
            int i = 0;
            while (n-- != 0)
            {
                Console.WriteLine("ingrese el coeficiente");
                double yus= double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                
                aux.Coef.Add(yus);
            }
            Console.WriteLine("ingrese el signo  1 =(mayor igual) ,2=(menor igual) ,3=(mayor que), 4=(menor que),5=(igual)");
            int op = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            if (op == 1)
            {
                aux._sign = Signo.MayorIgualQue;
            }
            else if (op == 2)
            {
                aux._sign = Signo.MenorIgualQue;
            }
            else if (op == 3)
            {
                aux._sign = Signo.MayorQue;
            }
            else if (op == 4)
            {
                aux._sign = Signo.MenorQue;
            }
            else if (op == 5)
            {
                aux._sign = Signo.Igual;
            }
            Console.WriteLine("ingrese el lado B");
            aux.Bside= double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            return aux;
        }

        public static void MostrarRestriccion(Restriction x)
        {
            Console.WriteLine(x.Name);
            Console.WriteLine("el signo es:  " + x._sign);
            for (int i = 0; i < x.Coef.Count; i++)
            {
                Console.WriteLine(x.Coef[i]+"X"+i);
            }
            
            Console.WriteLine("El lado B es "+x.Bside);
                
        }

        public static List<Restriction> LlenarListaderestricciones(int p)
        {
            List<Restriction> aux = new List<Restriction>();
            Console.WriteLine("ingrese el numero de restricciones");
            int n = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            while (n--!=0)
            {
                Restriction x = new Restriction();
                x = LLenarRestriccion(p);
                aux.Add(x);
                MostrarRestriccion(x);
            }
            return aux;
        }
        static void Main(string[] args)
        {

            List<MiembroFo> x = new List<MiembroFo>();

            x = LLenarLista();

            MostrarFo(x);
            Restriction y = new Restriction();
            Console.WriteLine("maximizar =0 ; minimzar = 1");
            int up = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Simplex aurus = new Simplex(x, up);

            aurus.AddRestriction(LlenarListaderestricciones(x.Count));
        
          

            Reporte _reporte = new Reporte(aurus);
            int oj = 0;
            Console.WriteLine(_reporte.LeZ());


            Console.WriteLine("variables");
            foreach (double ax in _reporte.Report_Variables())
            {
                Console.WriteLine("X " + oj + " =" + ax);
                oj++;
            }
            oj = 0;
            Console.WriteLine("variables limits lower");
            foreach (Rational t in _reporte.Report_Variable_Limits(true, 1))
            {
                Console.WriteLine("x " + oj + " lower =" + t);
                oj++;
            }
            oj = 0;
            Console.WriteLine("variables limits upper");
            foreach (Rational t in _reporte.Report_Variable_Limits(false, 1))
            {
                Console.WriteLine("x " + oj + "upper  =" + t);
                oj++;
            }
            oj = 0;



            Console.WriteLine("slack or surplus");
            foreach (double ax in _reporte.Report_Restriction_Variables())
            {
                Console.WriteLine("R " + oj + " =" + ax);
                oj++;
            }
            oj = 0;
            Console.WriteLine("constraints limits lower");
            foreach (Rational t in _reporte.Report_Variable_Limits(true, 2))
            {
                Console.WriteLine("x " + oj + " lower =" + t);
                oj++;
            }
            oj = 0;
            Console.WriteLine("constraints limits upper");
            foreach (Rational t in _reporte.Report_Variable_Limits(false, 2))
            {
                Console.WriteLine("x " + oj + "upper  =" + t);
                oj++;
            }
            Console.WriteLine("CR Constraints/dual price ");
            oj = 0;
            foreach (double ax in _reporte.Report_Constrain_RC())
            {
                Console.WriteLine("R " + oj + " =" + ax);
                oj++;
            }

            Console.WriteLine(aurus.Solver.PivotCount);
            Console.WriteLine(aurus.Solver.BranchCount);

            Console.WriteLine(aurus.Solver.FactorCount);

            Console.WriteLine(aurus.Solver.SolvedGoalCount);

            Console.ReadKey();
        }
    }
}
