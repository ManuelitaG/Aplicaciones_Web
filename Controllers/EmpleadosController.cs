using Aplicaciones_Web.Clases;
using Aplicaciones_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aplicaciones_Web.Controllers
{
    [RoutePrefix("api/Empleados")]
    public class EmpleadosController : ApiController
    {
        //GET: Consultar información, No debe manipular la base de datos 
        //POST: SE utiliza para insertar información en la base de datos 
        //PUT: Se utiliza para modificar (Actualizar) información en la base de datos
        //DELETE: Se utiliza para eliminar información de la base de datos

        [HttpGet] // Es el servicio q se va a exponer: GET, POST, PUT, DELETE
        [Route("ConsultarTodos")] // Nombre de la funcionalidad que se va a ejecutar

        public List<EMPLeado> ConsultarTodos() 
        { 
            //Se crea una instancia de la clase clsEmpleado
            clsEmpleado Empleado = new clsEmpleado();
            // Se invoca el método ConsultarTodos() de la clase clsEmpleado
            return Empleado.ConsultarTodos();
        }

        [HttpGet] // Es el servicio q se va a exponer: GET, POST, PUT, DELETE
        [Route("ConsultarXDocumento")] // Nombre de la funcionalidad que se va a ejecutar
        public EMPLeado ConsultarXDocumento(string Documento)
        {
            //Se crea una instancia de la clase clsEmpleado
            clsEmpleado Empleado = new clsEmpleado();
            // Se invoca el método ConsultarTodos() de la clase clsEmpleado
            return Empleado.Consultar(Documento);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] EMPLeado empleado) 
        {
            //Se crea una instancia de la clase clsEmpleado
            clsEmpleado Empleado = new clsEmpleado();

            //Se pasa la propiedad empleado al objeto de la clase clsEmpleado
            Empleado.empleado= empleado;
            //Se invoca el método insertar

            return Empleado.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] EMPLeado empleado ) 
        {
            clsEmpleado Empleado = new clsEmpleado();
            Empleado.empleado = empleado;   
            return Empleado.Actualizar();   
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] EMPLeado empleado) 
        {
            clsEmpleado Empleado = new clsEmpleado();
            Empleado.empleado = empleado;
            return Empleado.Eliminar();

        }

        [HttpDelete]
        [Route("EliminarXDocumento")]
        public string EliminarXDocumento(string Documento)
        {
            clsEmpleado Empleado = new clsEmpleado();
            return Empleado.Eliminar(Documento);

        }

    }
}