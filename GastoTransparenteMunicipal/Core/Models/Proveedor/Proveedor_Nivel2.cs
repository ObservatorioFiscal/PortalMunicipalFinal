using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Proveedor
{
    public class Proveedor_Nivel2
    {
        public long IdNivel2 { get; set; }
        public Nullable<long> IdNIvel1 { get; set; }
        public string Nombre { get; set; }
        public Nullable<long> Monto { get; set; }
        public string Area { get; set; }
        public string Glosa { get; set; }        
    }
}
