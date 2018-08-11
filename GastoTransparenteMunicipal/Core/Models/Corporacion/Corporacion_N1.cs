using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Corporacion
{
    public class Corporacion_N1
    {
        public long IdNivel1 { get; set; }
        public Nullable<long> IdAno { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public Nullable<long> Monto { get; set; }
    }
}
