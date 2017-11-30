using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace IO.Frames.Simplex
{
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
