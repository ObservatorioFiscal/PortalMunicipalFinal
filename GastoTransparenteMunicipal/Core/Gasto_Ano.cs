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
    
    public partial class Gasto_Ano
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gasto_Ano()
        {
            this.Gasto_Nivel1 = new HashSet<Gasto_Nivel1>();
        }
    
        public long IdAno { get; set; }
        public Nullable<int> IdMunicipalidad { get; set; }
        public Nullable<int> Ano { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool Activo { get; set; }
        public Nullable<decimal> Semestre { get; set; }
        public bool Cargado { get; set; }
        public string DataFilePath { get; set; }
    
        public virtual Municipalidad Municipalidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gasto_Nivel1> Gasto_Nivel1 { get; set; }
    }
}
