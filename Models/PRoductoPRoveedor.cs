//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aplicaciones_Web.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class PRoductoPRoveedor
    {
        public int Codigo { get; set; }
        public string Documento { get; set; }
        public int CodigoProducto { get; set; }
        public int ValorUnitario { get; set; }
        public System.DateTime FechaCotizacion { get; set; }
        public System.DateTime FechaValidez { get; set; }

        [JsonIgnore]
        public virtual PRODucto PRODucto { get; set; }
        [JsonIgnore]
        public virtual PROVeedor PROVeedor { get; set; }
    }
}
