using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Proveedor
{
    public class Proveedor_Nivel1
    {
        public long IdNivel1 { get; set; }
        public Nullable<long> IdAno { get; set; }
        public string Nombre { get; set; }
        public Nullable<long> Monto { get; set; }
    }
}
