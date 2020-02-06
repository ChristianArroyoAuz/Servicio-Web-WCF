// ******************************************************************************************
// Arroyo Auz Christian Xavier.                                                             *
// 05/08/2016.                                                                              *
// ******************************************************************************************


using System.Collections.Generic;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System;

namespace ObjetoRemoto
{
    public class Servidor
    {
        //Notifica un subproceso en espera de que ha ocurrido un evento
        private static System.Threading.AutoResetEvent eventoTerminacion = new System.Threading.AutoResetEvent(false);
        public static void Main()
        {
            //Creamos un Host de la libreria ServiciosCapitales para poder obtener informacion sobre un pais y las diferentes eventos que deseamos ejecutar
            ServiceHost anfitrion = new ServiceHost(typeof(ObjetoRemoto));
            //Agregamos el extremo de servicio y el puerto de escucha
            anfitrion.AddServiceEndpoint(typeof(IObjetorRemoto), new NetTcpBinding(), "net.tcp://localhost:8080");
            //Abrimos la comunicacion con el extremo
            anfitrion.Open();
            //Detemos  el evento hasta obtener una respuesta del extremo e imprimos la informacion deseada
            eventoTerminacion.WaitOne();
            Console.WriteLine();
            //Cerramos el extremo de comunicacion
            anfitrion.Close();
            Console.WriteLine();
        }

        //Metodo para detener el servidor
        public static void StopServer()
        {
            eventoTerminacion.Set();
        }
    }
}