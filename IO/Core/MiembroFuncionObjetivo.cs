namespace Core
{
    public class MiembroFuncionObjetivo
    {
        private string _nombre;
        private double _coeficiente;
        public int _valor;

        public string Nombre { get => _nombre; set => _nombre = value; }
        public double Coeficiente { get => _coeficiente; set => _coeficiente = value; }

        public MiembroFuncionObjetivo()
        {
            _valor = 0;
            _coeficiente = 1;
        }
    }
}