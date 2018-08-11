using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Personal
{
    public class Personal_Nivel2
    {
        public long IdNivel2 { get; set; }
        public Nullable<long> IdNivel1 { get; set; }
        public string Nombre { get; set; }
        public Nullable<long> MontoMujer { get; set; }
        public Nullable<long> MontoHombre { get; set; }
        public Nullable<long> CantidadMujer { get; set; }
        public Nullable<long> CantidadHombre { get; set; }
    }
}
