using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Subsidio
{
    public class Subsidio_N2
    {
        public long IdNivel2 { get; set; }
        public Nullable<long> IdNivel1 { get; set; }
        public string Nombre { get; set; }
        public Nullable<long> Monto { get; set; }
        public List<Subsidio_N3> subsidio_Nivel3 { get; set; }

        public Subsidio_N2()
        {
            subsidio_Nivel3 = new List<Subsidio_N3>();
        }
    }
}
