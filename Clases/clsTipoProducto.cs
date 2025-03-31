﻿using Aplicaciones_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplicaciones_Web.Clases
{
    public class clsTipoProducto
    {
        private SuperMercadoEntities dbSuper = new SuperMercadoEntities();
        public TIpoPRoducto tipoProducto { get; set; }
        public List<TIpoPRoducto> ConsultarTodos() 
        {
            return dbSuper.TIpoPRoductoes.ToList();
        }
        public TIpoPRoducto Consultar(int Codigo) 
        {
           return dbSuper.TIpoPRoductoes.Where(x => x.Codigo == Codigo).FirstOrDefault(); 
        }
        public string Insertar() 
        {
            try
            {
                dbSuper.TIpoPRoductoes.Add(tipoProducto);
                dbSuper.SaveChanges();
                return "Tipo producto insertado correctamente";
            }
            catch (Exception ex) 
            {
                return "Error "+ ex.Message;
            }
        }

        public string Actualizar() 
        {
            try
            {
                TIpoPRoducto tipoProd = Consultar(tipoProducto.Codigo);
                if (tipoProd == null)
                {
                    return "El tipo producto no se encuentra en la base de datos";
                }
                tipoProd.Nombre = tipoProducto.Nombre;
                tipoProd.Activo = tipoProducto.Activo;
                dbSuper.SaveChanges();
                return "Tipo de producto actualizado correctamente";
            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
        }
    }
}