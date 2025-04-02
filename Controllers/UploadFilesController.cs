using Aplicaciones_Web.Clases;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aplicaciones_Web.Controllers
{
    [RoutePrefix("api/UploadFiles")]
    public class UploadFilesController : ApiController
    {

        [HttpPost]
        public async Task<HttpResponseMessage> CargarArchivo(HttpRequestMessage request, string Datos, string Proceso)
        {
            clsUpload upload = new clsUpload();
            upload.Datos = Datos;
            upload.Proceso = Proceso;
            upload.request = request;
            return await upload.GrabarArchivo(false);
        }

        [HttpGet]
        public HttpResponseMessage ConsultarArchivo(string NombreImagen) 
        {
            clsUpload upload = new clsUpload();
            return upload.DescargarArchivo(NombreImagen);   
        }

        [HttpPut]
        public async Task<HttpResponseMessage> ActualizarArchivo(HttpRequestMessage request)
        {
            clsUpload upload = new clsUpload();
            upload.request = request;
            return await upload.GrabarArchivo(true);
        }

        [HttpDelete]
        public HttpResponseMessage EliminarArchivo(string NombreImagen, string Datos, string Proceso) 
        {
            clsUpload upload = new clsUpload();
            upload.Datos = Datos;
            upload.Proceso = Proceso;
            return upload.EliminarArchivo(Request, NombreImagen);
        }



    }
}