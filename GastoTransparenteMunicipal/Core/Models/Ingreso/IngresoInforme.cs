using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Ingreso
{
    public class IngresoInforme
    {        
        public string Codigo { get; set; }
        public Nullable<double> MontoPresupuestado { get; set; }
        public Nullable<double> MontoGastado { get; set; }
        //public Nullable<int> TipoCodigo { get; set; }
        public string TipoNombre { get; set; }
    }
}
