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
    [RoutePrefix("api/Productos")]
    public class ProductosController : ApiController
    {
        //GET: Consultar información, No debe manipular la base de datos 
        //POST: SE utiliza para insertar información en la base de datos 
        //PUT: Se utiliza para modificar (Actualizar) información en la base de datos
        //DELETE: Se utiliza para eliminar información de la base de datos

        [HttpGet]
        [Route("ConsultarImagenes")]
        public IQueryable ConsultarImagenes(int idProducto)
        {
            clsProducto Producto = new clsProducto();
            return Producto.ListarImagenes(idProducto);
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<PRODucto> ConsultarTodos() 
        {
            clsProducto Producto    = new clsProducto();
            return Producto.ConsultarTodos();  
        }

        [HttpGet]
        [Route("Consultar")]
        public PRODucto Consultar (int Codigo) 
        {
            clsProducto producto = new clsProducto();
            return producto.Consultar(Codigo);       
        }

        [HttpGet]
        [Route("ConsultarXTipoProducto")]
        public List<PRODucto> ConsultarXTipoProducto (int TipoProducto) 
        {
            clsProducto producto = new clsProducto();
            return producto.ConsultarXTipoProducto(TipoProducto);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] PRODucto producto) 
        {
            clsProducto Producto = new clsProducto();
            Producto.producto = producto;
            return Producto.Insertar();
        
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] PRODucto producto) 
        {
            clsProducto Producto = new clsProducto();
            Producto.producto = producto;
            return Producto.Actualizar();
        }

        [HttpPut]
        [Route("Inactivar")]
        public string Inactivar (int Codigo) 
        {
            clsProducto Producto = new clsProducto();
            return Producto.ModificarEstado(Codigo, false);
        }

        [HttpPut]
        [Route("Activar")]
        public string Activar(int Codigo)
        {
            clsProducto Producto = new clsProducto();
            return Producto.ModificarEstado(Codigo, true);
        }

        [HttpDelete]
        [Route("Eliminar")]

        public string Eliminar([FromBody] PRODucto producto) 
        {
            clsProducto Producto = new clsProducto();
            Producto.producto = producto;
            return Producto.Eliminar();

        }

        [HttpDelete]
        [Route("EliminarXCodigo")]

        public string EliminarXCodigo(int codigo)
        {
            clsProducto Producto = new clsProducto();
            return Producto.Eliminar(codigo);
        }
    }
}