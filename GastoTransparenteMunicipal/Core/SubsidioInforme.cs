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
    
    public partial class SubsidioInforme
    {
        public long IdSubsidio { get; set; }
        public Nullable<System.Guid> IdGroupInformeSubsidio { get; set; }
        public string Categoria { get; set; }
        public string Organizacion { get; set; }
        public string FechaDecreto { get; set; }
        public string ObjetivoDelAporte { get; set; }
        public Nullable<double> MontoAporte { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
