using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Gasto
{
    public class GastoInforme
    {        
        public string Codigo { get; set; }
        public string Cuenta { get; set; }
        public Nullable<double> MontoPresupuestado { get; set; }
        public Nullable<double> MontoGastado { get; set; }        
        //public Nullable<int> TipoCodigo { get; set; }
        public string TipoNombre { get; set; }
    }
}
