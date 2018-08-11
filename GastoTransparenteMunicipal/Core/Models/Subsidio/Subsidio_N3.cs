using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Subsidio
{
    public class Subsidio_N3
    {
        public long IdNivel3 { get; set; }
        public Nullable<long> IdNivel2 { get; set; }
        public string Nombre { get; set; }
        public Nullable<long> Monto { get; set; }
    }
}
