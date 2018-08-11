using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Subsidio
{
    public class Subsidio_N1
    {
        public long IdNivel1 { get; set; }
        public Nullable<long> IdAno { get; set; }
        public string Nombre { get; set; }
        public Nullable<long> Monto { get; set; }
        public List<Subsidio_N2> subsidio_Nivel2 { get; set; }

        public Subsidio_N1()
        {
            subsidio_Nivel2 = new List<Subsidio_N2>();
        }
    }
}
