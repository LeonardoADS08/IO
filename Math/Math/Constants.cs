using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.Constants
{
    public enum Signs
    {
        Equal= 1,
        Bigger = 2,
        BiggerEqual = 3,
        Less = 4,
        LessEqual = 5
    };

    public static class Errors
    {

        public static string ExceptionIn = "Excepción en: ";
        public static string DivideByZero = "Error #1: La división entre cero no esta permitida.";

    }
}
