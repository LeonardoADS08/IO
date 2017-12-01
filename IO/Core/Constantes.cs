using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum Signo
    {
        MayorQue = 1,
        MayorIgualQue = 2,
        MenorQue = 3,
        MenorIgualQue = 4,
        Igual = 5
    }

    public static class Signos
    {
        public static Dictionary<string, Core.Signo> SignosDictionary = new Dictionary<string, Core.Signo>();

        static Signos()
        {
            SignosDictionary.Add(">", Signo.MayorQue);
            SignosDictionary.Add(">=", Signo.MayorIgualQue);
            SignosDictionary.Add("=", Signo.Igual);
            SignosDictionary.Add("<=", Signo.MenorIgualQue);
            SignosDictionary.Add("<", Signo.MenorQue);
        }
    }
}
