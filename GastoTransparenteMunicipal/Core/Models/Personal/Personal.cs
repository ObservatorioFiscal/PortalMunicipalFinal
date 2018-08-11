using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Personal
{
    public class Personal
    {
        public Personal_Nivel1 Personal_Nivel1 { get; set; }
        public List<Personal_Nivel2> Personal_Nivel2 { get; set; }

        public Personal()
        {
            this.Personal_Nivel1 = new Personal_Nivel1();
            this.Personal_Nivel2 = new List<Models.Personal.Personal_Nivel2>();
        }        
    }
}
