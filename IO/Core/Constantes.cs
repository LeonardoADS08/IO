using System.Collections.Generic;

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
        public static Dictionary<Core.Signo, string> IdSignoDictionary = new Dictionary<Core.Signo, string>();

        static Signos()
        {
            SignosDictionary.Add(">", Signo.MayorQue);
            SignosDictionary.Add(">=", Signo.MayorIgualQue);
            SignosDictionary.Add("=", Signo.Igual);
            SignosDictionary.Add("<=", Signo.MenorIgualQue);
            SignosDictionary.Add("<", Signo.MenorQue);

            IdSignoDictionary.Add(Signo.MayorQue, ">");
            IdSignoDictionary.Add(Signo.MayorIgualQue, ">=");
            IdSignoDictionary.Add(Signo.Igual, "=");
            IdSignoDictionary.Add(Signo.MenorIgualQue, "<=");
            IdSignoDictionary.Add(Signo.MenorQue, "<");
        }
    }
}