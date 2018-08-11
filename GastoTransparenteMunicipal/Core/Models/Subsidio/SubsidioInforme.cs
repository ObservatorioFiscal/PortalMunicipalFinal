using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Subsidio
{
    public class SubsidioInforme
    {        
        public string CATEGORIA { get; set; }
        public string ORGANIZACION { get; set; }
        public string FECHADECRETO { get; set; }
        public string OBJETIVODELAPORTE { get; set; }
        public Nullable<double> MONTOAPORTE { get; set; }        
    }
}
