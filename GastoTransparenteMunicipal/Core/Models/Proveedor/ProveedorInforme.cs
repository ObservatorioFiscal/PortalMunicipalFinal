using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Proveedor
{
    public class ProveedorInforme
    {
        public string Categoria { get; set; }
        public Nullable<long> NumeroOrdenCompra { get; set; }
        public string Glosa { get; set; }
        public string Proveedor { get; set; }
        public string RUT { get; set; }
        public Nullable<long> Monto { get; set; }        
    }
}
