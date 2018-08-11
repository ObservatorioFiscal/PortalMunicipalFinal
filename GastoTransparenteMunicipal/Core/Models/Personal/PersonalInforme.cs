using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Personal
{
    public class PersonalInforme
    {
        public string Categoria { get; set; }
        public string GENERO { get; set; }
        public Nullable<int> EDAD { get; set; }
        public string CALIDADJURIDICA { get; set; }
        public string PROFESION { get; set; }
        public string NIVELACADEMICO { get; set; }
        public string ESTAMENTO { get; set; }
        public Nullable<int> GRADO { get; set; }
        public string ANTIGUEDAD { get; set; }
        public string AREA { get; set; }
        public Nullable<long> SUELDOHABERES { get; set; }
    }
}
