using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;



namespace Math.Structures
{
    #pragma warning disable CS0660 
    #pragma warning disable CS0661 

    /*
     Errores:
        El error más común que puede suceder en Fraction será que en algún momento haya denominador en 0.
            - Este error solo se valida en la creación del objeto (En el segundo constructor) y en Simplify() dado que esta función se llama en practicamente todas las sobrecargas.
         
    */

    public class Fraction

    {
        private int _numerator, _denominator;

        public int Numerator { get => _numerator; set => _numerator = value; }
        public int Denominator { get => _denominator; set => _denominator = value; }

        public Fraction()
        {
            _numerator = 0; 
            _denominator = 1;
        }

        public Fraction(int numerator, int denominator = 1)
        {
            if (_denominator == 0) throw new System.Exception(Utils.ErrorList.NotDivisbleByZero);

            _numerator = numerator;
            _denominator = denominator;
            this.Simplify();
        }   

        public Fraction(string val)
        {
            Fraction res = Fraction.FromString(val);
            _numerator = res.Numerator;
            _denominator = res.Denominator;

        }

        public void Simplify()
        {
            if (_denominator == 0) throw new System.Exception(Utils.ErrorList.NotDivisbleByZero);

            int gcd = NumberOperation.GCD(System.Math.Abs(_numerator), System.Math.Abs(_denominator));
            _numerator /= gcd;
            _denominator /= gcd;

            if (_numerator < 0 && _denominator < 0)
            {
                _numerator *= -1;
                _denominator *= -1;
            }
            else if (_numerator > 0 && _denominator < 0)
            {
                _numerator *= -1;
                _denominator *= -1;
            }
        }

        public void Invert()
        {
            int temp = _numerator;
            _numerator = _denominator;
            _denominator = temp;
        }

        public static Fraction operator + (Fraction first, Fraction second)
        {
            first.Simplify();
            second.Simplify();

            Fraction res = new Fraction();
            res.Numerator = first.Numerator * second.Denominator + second.Numerator * first.Denominator;
            res.Denominator = first.Denominator * second.Denominator;

            res.Simplify();   
            return res;
        }

        public static Fraction operator + (Fraction first, int second)
        {
            first.Simplify();

            Fraction res = new Fraction();
            res.Numerator = first.Numerator + second * first.Denominator;
            res.Denominator = first.Denominator;

            res.Simplify();
            return res;
        }

        public static Fraction operator + (int first, Fraction second) => second + first;

        public static Fraction operator - (Fraction first, Fraction second)
        {
            first.Simplify();
            second.Simplify();
            
            second.Numerator *= -1;

            return first + second;
        }

        public static Fraction operator - (Fraction first, int second)
        {
            first.Simplify();

            Fraction res = new Fraction();
            res.Numerator = first.Numerator - second * first.Denominator;
            res.Denominator = first.Denominator;

            res.Simplify();
            return res;
        }

        public static Fraction operator - (int first, Fraction second) => second - first;

        public static Fraction operator * (Fraction first, Fraction second)
        {
            Fraction res = new Fraction();
            res.Numerator = first.Numerator * second.Numerator;
            res.Denominator = first.Denominator * second.Denominator;

            res.Simplify();
            return res;
        }

        public static Fraction operator * (Fraction first, int second)
        {
            Fraction res = new Fraction();
            res.Numerator = first.Numerator * second;
            res.Denominator = first.Denominator;

            res.Simplify();
            return res;
        }

        public static Fraction operator * (int first, Fraction second) => second * first;

        public static Fraction operator / (Fraction first, Fraction second)
        {
            Fraction res = new Fraction();
            res.Numerator = first.Numerator * second.Denominator;
            res.Denominator = first.Denominator * second.Numerator;
            res.Simplify();
            return res;
        }

        public static Fraction operator / (Fraction first, int second)
        {
            Fraction res = new Fraction();
            res.Numerator = first.Numerator;
            res.Denominator = first.Denominator * second;

            res.Simplify();
            return res;
        }

        public static Fraction operator / (int first, Fraction second) => second / first;

        public static bool operator == (Fraction first, Fraction second) => first.Numerator == second.Numerator && first.Denominator == second.Denominator;

        public static bool operator !=(Fraction first, Fraction second) =>  !(first == second);

        public static bool operator > (Fraction first, Fraction second) => first.ToDouble() > second.ToDouble();

        public static bool operator >= (Fraction first, Fraction second) => first.ToDouble() >= second.ToDouble();

        public static bool operator < (Fraction first, Fraction second) => !(first > second);

        public static bool operator <= (Fraction first, Fraction second) => !(first >= second);

        public static bool operator > (Fraction first, int second) => first.ToDouble() > second;

        public static bool operator >= (Fraction first, int second) => first.ToDouble() >= second;

        public static bool operator < (Fraction first, int second) => !(first > second);

        public static bool operator <= (Fraction first, int second) => !(first >= second);

        public static bool operator > (int first, Fraction second) => first > second.ToDouble();

        public static bool operator >= (int first, Fraction second) => first >= second.ToDouble();

        public static bool operator < (int first, Fraction second) => !(first > second);

        public static bool operator <= (int first, Fraction second) => !(first >= second);

        public static implicit operator double(Fraction val) => val.ToDouble();
        
        public static implicit operator Fraction(int val) => new Fraction(val);

        public double ToDouble() => (double)_numerator / (double)_denominator;

        public static Fraction FromString(string val)
        {
            Fraction res = new Fraction();
            string[] sep = val.Split('.');
            res.Numerator = Convert.ToInt32(sep[0]) * (int)System.Math.Pow(10, sep[1].Length);
            res.Denominator = (int)System.Math.Pow(10, sep[1].Length);
            res.Simplify();

            return res;
        }

        public void Pow(int exponent)
        {
            _numerator = NumberOperation.Pow(_numerator, exponent);
            _denominator = NumberOperation.Pow(_denominator, exponent);
            this.Simplify();
        }

    }

    #pragma warning restore CS0661
    #pragma warning restore CS0660

}

