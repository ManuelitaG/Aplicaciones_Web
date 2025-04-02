using Aplicaciones_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Aplicaciones_Web.Clases
{
    public class clsProducto
    {
        private SuperMercadoEntities dbSuper = new SuperMercadoEntities();

        public PRODucto producto { get; set; }

        //Insertar producto
        public string Insertar()
        {
            try
            {
                dbSuper.PRODuctoes.Add(producto); //Agrega un objeto empleado a la lista de "productos"
                dbSuper.SaveChanges(); // Guardar los cambios en la base de datos
                return "Producto guardado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el Producto " + ex.Message;
            }
        }

        //Actualizar Productos
        public string Actualizar()
        {
            try
            {
                //antes de actualizar un elemento (producto), se debe consultar para verificar q exista
                PRODucto pro = Consultar(producto.Codigo);
                if (pro == null)
                {
                    return "El producto con el código ingresado no existe, por ende no se puede actualizar :( ";
                }

                dbSuper.PRODuctoes.AddOrUpdate(producto);
                dbSuper.SaveChanges(); // Guardar los cambios en la base de datos
                return "Producto actualizado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el producto :( " + ex.Message;

            }
        }

        //Eliminar producto 
        public string Eliminar()
        {
            try
            {
                PRODucto pro = Consultar(producto.Codigo);
                if (pro == null)
                {
                    return "El producto ingresado no existe, por ende no se puede eliminar :( ";
                }

                dbSuper.PRODuctoes.Remove(pro);  
                dbSuper.SaveChanges(); // Guardar los cambios en la base de datos
                return "Producto eliminado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el producto :( " + ex.Message;

            }
        }

        //Eliminar producto 
        public string Eliminar(int Codigo)
        {
            try
            {
                PRODucto pro = Consultar(Codigo);
                if (pro == null)
                {
                    return "El producto ingresado no existe, por ende no se puede eliminar :( ";
                }

                dbSuper.PRODuctoes.Remove(pro);
                dbSuper.SaveChanges(); // Guardar los cambios en la base de datos
                return "Producto eliminado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el producto :( " + ex.Message;

            }
        }

        //Método para consultar que si exista en la base de datos
        public PRODucto Consultar(int codigo)
        {
            return dbSuper.PRODuctoes.FirstOrDefault(e => e.Codigo == codigo);

        }

        //Listar todos los productos 
        public List<PRODucto> ConsultarTodos()
        {
            return dbSuper.PRODuctoes
                .OrderBy(e => e.Nombre)
                .ToList(); // ToList es una función que convierte una lista de datos en una lista de objetos

        }

        //Listar todos los productos
        public List<PRODucto> ConsultarXTipoProducto(int CodTipoProducto) 
        {
            return dbSuper.PRODuctoes
                .Where(p => p.CodigoTipoProducto == CodTipoProducto)
                .OrderBy (e => e.Nombre)
                .ToList();
      
        }

        public string ModificarEstado(int Codigo, bool Activo)
        {
            try

            {
                PRODucto prod = Consultar(Codigo);
                if (prod == null)
                {
                    return "El codigo del producto no se encuentra en la base de datos";
                }
                prod.Activo = Activo;
                dbSuper.SaveChanges();
                if (Activo)
                {
                    return "Se activó correctamente el producto";
                }
                else 
                {
                    return "Se inactivó correctamente el producto";
                }

            }
            catch (Exception ex)
            {
                return " MALO :( ";           
            }

        }
        public string GrabarImagenProducto(int idProducto, List<string> Imagenes) 
        {
            try
            {
                foreach (string imagen in Imagenes)
                {
                    ImagenesProducto imagenProducto = new ImagenesProducto();
                    imagenProducto.idProducto = idProducto;
                    imagenProducto.NombreImagen = imagen;
                    dbSuper.ImagenesProductoes.Add(imagenProducto);
                    dbSuper.SaveChanges();

                }
                return "Se guardó la información en la base de datos :)";
            }
            catch (Exception ex) 
            { 
                return"Error :( "+ ex.Message;
            }
        }
        public string EliminarImagenProducto(int idImagen)
        {
            try
            {
                
                var imagenProducto = dbSuper.ImagenesProductoes.FirstOrDefault(e => e.idImagen == idImagen);

                if(imagenProducto != null) 
                {
                    dbSuper.ImagenesProductoes.Remove(imagenProducto);
                    dbSuper.SaveChanges();
                    return "Se guardó la información en la base de datos :)";
                }
                else 
                {
                    return "Error, no está en la base de datos";
                }


            }
            catch (Exception ex)
            {
                return "Error :( " + ex.Message;
            }
        }
        public IQueryable ListarImagenes(int idProducto) 
        {
            return from P in dbSuper.Set<PRODucto>()
                       join TP in dbSuper.Set<TIpoPRoducto>()
                       on P.CodigoTipoProducto equals TP.Codigo
                       join I in dbSuper.Set<ImagenesProducto>()
                       on P.Codigo equals I.idProducto
                   where P.Codigo == idProducto
                   orderby I.NombreImagen
                   select new
                   {
                       idTipoProducto = TP.Codigo,
                       TipoProducto = TP.Nombre,
                       idProducto = P.Codigo,
                       Producto = P.Nombre,
                       Imagen = I.NombreImagen
                   };

        }
    }

}
