// ******************************************************************************************
// Arroyo Auz Christian Xavier.                                                             *
// 05/08/2016.                                                                              *
// ******************************************************************************************


using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System;

namespace ObjetoRemoto
{
    //ServiceContract:Indica que una interfaz o una clase define un contrato de servicio en una aplicación de WCF
    //Interface: Una interfaz contiene definiciones para un grupo de funcionalidades relacionadas que una clase o una estructura pueden poner en práctica
    [ServiceContract]
    public interface IObjetorRemoto
    {
        //OperationContract: Indica que un método define una operación que forma parte de un contrato de servicio en una aplicación de WCF
        [OperationContract]
        //Contrato para cargar el id autonumerado
        int obtenerID(int id);
        [OperationContract]
        //Contrato para agregar los clientes
        List<Piezas> agregarCliente(List<Piezas> listausuarios);
        [OperationContract]
        //Contrato para modificar un cliente seleccionado
        List<Piezas> modificarCliente(List<Piezas> listausuarios);
        [OperationContract]
        //Contrato para eliminar el cliente seleccionado
        List<Piezas> eliminarCliente(List<Piezas> listausuarios);
        [OperationContract]
        //Contrato para mostrar todos los clientes en la tabla
        List<Piezas> mostarTodos(List<Piezas> listausuarios);
        [OperationContract]
        //Contrato para buscar un cliente por su id
        List<Piezas> buscarID(List<Piezas> listausuarios);
    }
}