//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingreso_Nivel1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingreso_Nivel1()
        {
            this.Ingreso_Nivel2 = new HashSet<Ingreso_Nivel2>();
        }
    
        public long IdNivel1 { get; set; }
        public Nullable<long> IdAno { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public Nullable<long> MontoGastado { get; set; }
        public Nullable<long> MontoPresupuestado { get; set; }
        public Nullable<double> PorcentajeGastado { get; set; }
        public Nullable<double> PorcentajePresupuestado { get; set; }
        public string Descripcion { get; set; }
    
        public virtual Ingreso_Ano Ingreso_Ano { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingreso_Nivel2> Ingreso_Nivel2 { get; set; }
    }
}