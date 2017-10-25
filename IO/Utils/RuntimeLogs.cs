using System;
using System.IO;
using System.Threading;

namespace SDA_Core.Functional
{
    /// <summary>
    /// ES: Una clase que almacena registros en tiempo de ejecución, enfocado para guardar mensajes de excepciones u otros errores,
    ///     para que sea posteriormente analizado y sea más fácil encontrar errores.
    ///     Este proceso se ejecuta en otro hilo.
    /// </summary>
    internal static class RuntimeLogs
    {
        private static void WriteLine(string message, string direction, bool enumerate = true)
        {
            try
            {
                // ES: Dirección donde se guardará el archivo.
                //     Dirección del programa + "Runtime_Log " + Fecha + ".log"
                string logfile = AppDomain.CurrentDomain.BaseDirectory + "Runtime_Log " + DateTime.Today.ToString("dd-MM-yyyy") + ".log";

                // ES: El mensaje que será guardado en el Log
                string final = "";
                if (enumerate) final = DateTime.Now.ToString("hh:mm:ss") + ": " + direction + " - " + message + "\r\n";
                else final = direction + " - " + message + "\r\n";
                File.AppendAllText(logfile, final);
            }
            catch //(Exception ex)
            {
                // MessageBox.Show("An error message cannot be saved in the runtime log.\n" + ex.Message, "Error - RuntimeLog", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// ES: Envia un log a los registros del programa, esta función intentará ejecutarse en otro hilo de proceso.
        /// </summary>
        /// <param name="message">ES: Mensaje o razón por la cual se crea el log.</param>
        /// <param name="direction">ES: Dirección de donde se ha llamado el procedimiento.</param>
        /// <param name="enumerate">ES: Si se debe especificar el momento en el cual se ha llamado el procedimiento.</param>
        public static void SendLog(string message, string direction, bool enumerate = true)
        {
            try
            {
                Thread _thread = new Thread(() => WriteLine(message, direction, enumerate));
                _thread.Start();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message, nameof(RuntimeLogs) + "SendLog(string, string, bool = true)", false);
                WriteLine(message, direction, enumerate);
            }
        }
    }
}