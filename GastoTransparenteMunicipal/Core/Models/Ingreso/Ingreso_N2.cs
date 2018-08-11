using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Ingreso
{
    public class Ingreso_N2
    {
        public long IdNivel2 { get; set; }
        public Nullable<long> IdNivel1 { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public Nullable<long> MontoGastado { get; set; }
        public Nullable<long> MontoPresupuestado { get; set; }
        public Nullable<double> PorcentajeGastado { get; set; }
        public Nullable<double> PorcentajePresupuestado { get; set; }
        public string Descripcion { get; set; }
    }
}
