using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Aplicaciones_Web.Clases
{
    public class clsUpload
    {
        public string Datos { get; set; }
        public string Proceso { get; set; }
        public HttpRequestMessage request { get; set; }

        private List<string> Archivos;

        public async Task<HttpResponseMessage> GrabarArchivo()
        {

            if (!request.Content.IsMimeMultipartContent())
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
            }
            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                bool Existe = false;
                //Lee el contenido de los archivos
                await request.Content.ReadAsMultipartAsync(provider);
                if (provider.FileData.Count > 0)
                {
                    Archivos = new List<string>();
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName;
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        if (File.Exists(Path.Combine(root, fileName)))
                        {
                            //Opciones si un archivo ya existe, no se va a cargar, se va a eliminar el temporal y se devolverá el error
                            File.Delete(Path.Combine(root, file.LocalFileName));
                            Existe = true;

                        }
                        else
                        {
                            Existe = false;
                            Archivos.Add(fileName); //Agrego en una lista el nombre de los archivos que se cargaron
                            File.Move(file.LocalFileName, Path.Combine(root, fileName)); // lo renombra
                        }

                    }
                    if (!Existe)
                    {
                        //Proceso de gestión en la base de datos
                        string RptaBD = ProcesarBD();
                        // Termina el ciclo y se muestra un mensaje de éxito
                        return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se cargaron los archivos en el servidor" + RptaBD);
                    }
                    else 
                    {
                        return request.CreateErrorResponse(System.Net.HttpStatusCode.Conflict, "El archivo ya existe");
                    }
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private string ProcesarBD()
        {
            switch (Proceso.ToUpper())
            {
                case "PRODUCTO":
                    clsProducto producto = new clsProducto();

                    return producto.GrabarImagenProducto(Convert.ToInt32(Datos), Archivos);

                    break;

                default:
                    return "NO se ha definido el proceso en la base de datos";
            }
        }

    }
}