﻿using Aplicaciones_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private SuperMercadoEntities dbSuper = new SuperMercadoEntities();

        public async Task<HttpResponseMessage> GrabarArchivo(bool Actualizar)
        {
            try
            {
                if (!request.Content.IsMimeMultipartContent())
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
                }

                string root = HttpContext.Current.Server.MapPath("~/Archivos");
                var provider = new MultipartFormDataStreamProvider(root);


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
                            if (Actualizar)
                            {
                                File.Delete(Path.Combine(root, fileName));
                                //Actualizar el nombre del primer archivo
                                File.Move(file.LocalFileName, Path.Combine(root, fileName));
                                return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se actualizó la imagen");
                            }
                            else
                            {
                                //Opciones si un archivo ya existe, no se va a cargar, se va a eliminar el temporal y se devolverá el error
                                File.Delete(Path.Combine(root, file.LocalFileName));
                                Existe = true;
                            }

                        }
                        else
                        {
                            if (!Actualizar)
                            {
                                Existe = false;
                                Archivos.Add(fileName); //Agrego en una lista el nombre de los archivos que se cargaron
                                File.Move(file.LocalFileName, Path.Combine(root, fileName)); // lo renombra
                            }
                            else
                            {
                                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "El archivo no existe se debe agregar");
                            }
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

        public HttpResponseMessage DescargarArchivo(string Imagen)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, Imagen);
                if (File.Exists(Archivo))
                {
                    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    var stream = new FileStream(Archivo, FileMode.Open);
                    response.Content = new StreamContent(stream);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = Imagen;
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    return response;
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "No se encontró el archivo:( ");
                }

            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private string ProcesarBD2()
        {
            switch (Proceso.ToUpper())
            {
                case "IMAGEN":
                    
                    clsProducto producto = new clsProducto();

                    return producto.EliminarImagenProducto(Convert.ToInt32(Datos));

                    break;

                default:
                    return "NO se ha definido el proceso en la base de datos";
            }
        }
        public HttpResponseMessage EliminarArchivo(HttpRequestMessage request, string Imagen)
        {
            try
            {
                //string fileName = file.Headers.ContentDisposition.FileName;
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, Imagen);
                if (File.Exists(Archivo))
                {
                    File.Delete(Path.Combine(Ruta, Imagen));
                    string RptaBD = ProcesarBD2();
                    return request.CreateResponse(System.Net.HttpStatusCode.OK, "a" + RptaBD);
                    //return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se Deleta la imagen");

                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "No se existe el archivo:( ");
                }

            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}