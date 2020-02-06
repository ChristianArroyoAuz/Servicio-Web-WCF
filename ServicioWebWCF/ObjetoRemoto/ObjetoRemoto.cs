﻿// ******************************************************************************************
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
    //Obtiene o establece el valor que indica cuando se crean nuevos objetos de servicio.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    //Heredando de la interfaz IInformacionPais
    public class ObjetoRemoto : MarshalByRefObject, IObjetorRemoto
    {
        //Haciendo referencia a la clase miDB donde se halla la cadena de conexion a la base de datos y
        //creando una variable para poder cargar el ID y hacerlo autonumerado
        miDB miBase = new miDB();
        //Variables publicas para el envio y recibo de las consultas y datos
        public int identificadorModidifcar;
        public int idenfificadorUsuario, identificadorBusqueda, costo;
        public string nombre, descripcion;
        //Lista tipo piezas que me permite agregar los diferentes elementos del usuario
        public List<Piezas> mostarUsuarios = new List<Piezas>();

        public int obtenerID(int id)
        {
            //consulta a la base de datos usando LINQ para obtener el ID mas alto, sumarle 1 y presentar
            // en el formulario para el ingreso de de datos
            var consulta = (from piezas in miBase.Piezas
                            where piezas.Id > id
                            orderby piezas.Id descending
                            select (int)piezas.Id).Take(1);
            foreach (var identificador in consulta)
            {
                id = identificador;
                idenfificadorUsuario = id;
            }
            return (idenfificadorUsuario + 1);
        }

        public List<Piezas> agregarCliente(List<Piezas> listausuarios)
        {
            //Agregando los diferentes parametros a las tabla de piezas
            Piezas nuevo = new Piezas(idenfificadorUsuario, nombre, descripcion, costo);
            nuevo.Id = idenfificadorUsuario;
            nuevo.Nombre_Pieza = nombre;
            nuevo.Descripcion = descripcion;
            nuevo.Costo = costo;
            miBase.Piezas.InsertOnSubmit(nuevo);
            miBase.SubmitChanges();
            //Consulta a la base de datos usando LINQ para presentar todos los datos cuyo ID sea mayor o igual 0
            //Los datos se presentaran un datagrridview de forma ascendente
            var consulta = from piezas in miBase.Piezas
                           where piezas.Id == idenfificadorUsuario
                           select new
                           {
                               ID = piezas.Id,
                               NOMBRE = piezas.Nombre_Pieza,
                               DESCRIPCION = piezas.Descripcion,
                               COSTO = piezas.Costo
                           };
            foreach (var item in consulta)
            {
                //Recorriendo la lista de consultas para agregar a la lista local y enviar al cliente
                Piezas cargar = new Piezas(item.ID, item.NOMBRE, item.DESCRIPCION, item.COSTO);
                listausuarios.Add(cargar);
                mostarUsuarios = listausuarios;
            }
            return mostarUsuarios;
        }

        public List<Piezas> modificarCliente(List<Piezas> listausuarios)
        {
            //Codigo que me permite la modificacion de los parametros del elemento seleccionado
            miBase.ExecuteCommand("Update Piezas set Id =" + "'" + Convert.ToString(identificadorModidifcar) + "'" + "where Id =" + Convert.ToString(identificadorModidifcar) + ";");
            miBase.ExecuteCommand("Update Piezas set Nombre_Pieza =" + "'" + Convert.ToString(nombre) + "'" + "where Id =" + Convert.ToString(identificadorModidifcar) + ";");
            miBase.ExecuteCommand("Update Piezas set Descripcion =" + "'" + Convert.ToString(descripcion) + "'" + "where Id =" + Convert.ToString(identificadorModidifcar) + ";");
            miBase.ExecuteCommand("Update Piezas set Costo =" + "'" + Convert.ToString(costo) + "'" + "where Id =" + Convert.ToString(identificadorModidifcar) + ";");
            //Actualizando la tabla de la base de datos
            miBase.SubmitChanges();
            mostarTodos(listausuarios);
            return mostarUsuarios;
        }

        public List<Piezas> eliminarCliente(List<Piezas> listausuarios)
        {
            //Codigo que me permite la eliminacion del elemto seleccionado de la tabla en la base de datos
            miBase.ExecuteCommand("Delete from Piezas where Id = " + Convert.ToString(idenfificadorUsuario) + ";");
            miBase.SubmitChanges();
            mostarTodos(listausuarios);
            return mostarUsuarios;
        }

        public List<Piezas> mostarTodos(List<Piezas> listausuarios)
        {
            //Consulta a la base de datos usando LINQ para presentar todos los datos cuyo ID sea mayor o igual 0
            //Los datos se presentaran un datagrridview de forma ascendente
            var consulta = from piezas in miBase.Piezas
                           where piezas.Id >= 0
                           orderby piezas.Id ascending
                           select new
                           {
                               ID = piezas.Id,
                               NOMBRE = piezas.Nombre_Pieza,
                               DESCRIPCION = piezas.Descripcion,
                               COSTO = piezas.Costo
                           };
            foreach (var item in consulta)
            {
                //Recorriendo la lista de consultas para agregar a la lista local y enviar al cliente
                Piezas cargar = new Piezas(item.ID, item.NOMBRE, item.DESCRIPCION, item.COSTO);
                listausuarios.Add(cargar);
                mostarUsuarios = listausuarios;
            }
            return mostarUsuarios;
        }

        public List<Piezas> buscarID(List<Piezas> listausuarios)
        {
            //Consulta a la base de datos usando LINQ para presentar todos los datos cuyo ID sea mayor o igual 0
            //Los datos se presentaran un datagrridview de forma ascendente
            var consulta = from piezas in miBase.Piezas
                           where piezas.Id == identificadorBusqueda
                           orderby piezas.Id ascending
                           select new
                           {
                               ID = piezas.Id,
                               NOMBRE = piezas.Nombre_Pieza,
                               DESCRIPCION = piezas.Descripcion,
                               COSTO = piezas.Costo
                           };
            foreach (var item in consulta)
            {
                //Recorriendo la lista de consultas para agregar a la lista local y enviar al cliente
                Piezas cargar = new Piezas(item.ID, item.NOMBRE, item.DESCRIPCION, item.COSTO);
                listausuarios.Add(cargar);
                mostarUsuarios = listausuarios;
            }
            return mostarUsuarios;
        }
    }
}