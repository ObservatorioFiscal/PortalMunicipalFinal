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
    
    public partial class Gasto_Nivel2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gasto_Nivel2()
        {
            this.Gasto_Nivel3 = new HashSet<Gasto_Nivel3>();
        }
    
        public long IdNivel2 { get; set; }
        public Nullable<long> IdNivel1 { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public Nullable<long> MontoGastado { get; set; }
        public Nullable<long> MontoPresupuestado { get; set; }
        public Nullable<double> PorcentajeGastado { get; set; }
        public Nullable<double> PorcentajePresupuestado { get; set; }
        public string Descripcion { get; set; }
    
        public virtual Gasto_Nivel1 Gasto_Nivel1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gasto_Nivel3> Gasto_Nivel3 { get; set; }
    }
}
