using Aplicaciones_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplicaciones_Web.Clases
{
    public class clsUsuario
    {
        private SuperMercadoEntities dbSuper = new SuperMercadoEntities();
        public Usuario usuario { get; set; }

        public string CrearUsuario(int idPerfil) 
        {
            try 
            {
                clsCypher cypher = new clsCypher();
                cypher.Password = usuario.Clave;
                if (cypher.CifrarClave()) 
                {
                    //Grabar el usuario.
                    //se deben leer los datos de la clase cypher con la información encryptada. 
                    usuario.Clave = cypher.PasswordCifrado;
                    usuario.Salt = cypher.Salt;
                    dbSuper.Usuarios.Add(usuario);
                    dbSuper.SaveChanges();
                    //Grabar el perfil del usuario.
                    Usuario_Perfil UsuarioPerfil = new Usuario_Perfil();
                    UsuarioPerfil.idPerfil = idPerfil;
                    UsuarioPerfil.Activo = true;
                    UsuarioPerfil.idUsuario = usuario.id;
                    dbSuper.Usuario_Perfil.Add(UsuarioPerfil);
                    dbSuper.SaveChanges();
                    return "Se creó el usuario correctamente :)";
                }
                else 
                {
                    return "Error al cifrar la clave :( ";
                }
            }
            catch(Exception ex) 
            {
                return ex.Message;
            }
        }
    }
}