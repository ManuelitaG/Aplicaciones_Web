using Aplicaciones_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Aplicaciones_Web.Clases
{
    public class clsEmpleado
    {
        private SuperMercadoEntities dbSuper = new SuperMercadoEntities(); //Para gestionar la base de datos
        public EMPLeado empleado { get; set; } //Para manipular la info en la base de datos: add, edit o delete 


        //Insertar empleado
        public string Insertar() 
        {
            try { 
                dbSuper.EMPLeadoes.Add(empleado); //Agrega un objeto empleado a la lista de "empleadoes"
                dbSuper.SaveChanges(); // Guardar los cambios en la base de datos
                return "Empleado guardado correctamente";
            }
            catch (Exception ex) 
            { 
                return "Error al insertar el empleado "+ex.Message;          
            }
        }

        //Actualizar empleado
        public string Actualizar() 
        {
            try
            {
                //antes de actualizar un elemento (empleado), se debe consultar para verificar q exista
                EMPLeado emp = Consultar(empleado.Documento);
                if (emp == null)
                {
                    return "El empleado con el documento ingresado no existe, por ende no se puede actualizar :( ";
                }

                dbSuper.EMPLeadoes.AddOrUpdate(empleado);
                dbSuper.SaveChanges(); // Guardar los cambios en la base de datos
                return "Empleado actualizado correctamente :) ";
            }
            catch (Exception ex) 
            {
                return "No se pudo actializar el empleado :( " + ex.Message;
            
            }
        }

        //Eliminar empleado
        public string Eliminar(string Documento)
        {
            try
            {
                //antes de eliminar un elemento (empleado), se debe consultar para verificar q exista
                EMPLeado emp = Consultar(Documento);
                if (emp == null)
                {
                    return "El empleado con el documento ingresado no existe, por ende no se puede eliminar :( ";
                }

                dbSuper.EMPLeadoes.Remove(emp); //Se debe eliminar el objeto empleado que busca, no el que se envía 
                dbSuper.SaveChanges(); // Guardar los cambios en la base de datos
                return "Empleado eliminado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el empleado :( " + ex.Message;

            }
        }

        //Eliminar empleado
        public string Eliminar()
        {
            try
            {
                //antes de eliminar un elemento (empleado), se debe consultar para verificar q exista
                EMPLeado emp = Consultar(empleado.Documento);
                if (emp == null)
                {
                    return "El empleado con el documento ingresado no existe, por ende no se puede eliminar :( ";
                }

                dbSuper.EMPLeadoes.Remove(emp); //Se debe eliminar el objeto empleado que busca, no el que se envía 
                dbSuper.SaveChanges(); // Guardar los cambios en la base de datos
                return "Empleado eliminado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el empleado :( " + ex.Message;

            }
        }

        //Método para consultar que si exista en la base de datos
        public EMPLeado Consultar (string Documento) 
        {
            //Expresiones lambda. => permite definir funciones anonimas o instancias de objetos, sin la creación formal, dependiendo de la lista a la cual pertenezca
            //FirstOrDefault. es una función q permite consultar el primer elemento de una lista que cumple las consiciones solicitadas 
            return dbSuper.EMPLeadoes.FirstOrDefault(e => e.Documento == Documento);
        
        }

        //Listar todos los empleados
        public List<EMPLeado> ConsultarTodos() 
        {
            return dbSuper.EMPLeadoes
                .OrderBy(e => e.PrimerApellido) //Es una funcion que permite ordenar los elementos de una lista de acuerdo a un criterio específico
                .ToList(); // ToList es una función que convierte una lista de datos en una lista de objetos
        
        }
    }
}