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
    
    public partial class Personal_Salud_Nivel1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personal_Salud_Nivel1()
        {
            this.Personal_Salud_Nivel2 = new HashSet<Personal_Salud_Nivel2>();
        }
    
        public long IdNivel1 { get; set; }
        public Nullable<long> IdAno { get; set; }
        public string Nombre { get; set; }
        public string CodTipo { get; set; }
    
        public virtual Personal_Ano Personal_Ano { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personal_Salud_Nivel2> Personal_Salud_Nivel2 { get; set; }
    }
}
