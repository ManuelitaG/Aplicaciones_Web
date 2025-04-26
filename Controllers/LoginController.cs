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
    [RoutePrefix("api/Login")]
    //[AllowAnonymous] No requiere autenticación
    //[Authorize] Si necesita autenticación y un token para que se pueda procesar

    [AllowAnonymous]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Ingresar")]
        public IQueryable<LoginRespuesta> Ingresar([FromBody] Login login) 
        {
            clsLogin _login = new clsLogin();
            _login.login = login;
            return _login.Ingresar();

        }
        
    }
}