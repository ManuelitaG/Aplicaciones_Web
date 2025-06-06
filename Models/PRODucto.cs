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
    
    public partial class PRODucto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODucto()
        {
            this.DEtalleFActuras = new HashSet<DEtalleFActura>();
            this.DEtalleFacturaCompras = new HashSet<DEtalleFacturaCompra>();
            this.PRoductoPRoveedors = new HashSet<PRoductoPRoveedor>();
            this.ImagenesProductoes = new HashSet<ImagenesProducto>();
        }
    
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int ValorUnitario { get; set; }
        public int CodigoTipoProducto { get; set; }
        public bool Activo { get; set; }

        [JsonIgnore]
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEtalleFActura> DEtalleFActuras { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEtalleFacturaCompra> DEtalleFacturaCompras { get; set; }
        [JsonIgnore]
        public virtual TIpoPRoducto TIpoPRoducto { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRoductoPRoveedor> PRoductoPRoveedors { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImagenesProducto> ImagenesProductoes { get; set; }
    }
}
