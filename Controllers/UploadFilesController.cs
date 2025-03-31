using Aplicaciones_Web.Clases;
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
            return await upload.GrabarArchivo();
        }


    }
}