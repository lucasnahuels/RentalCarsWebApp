using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TrabajoCGB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class OficinaAux //deberia ir con mayusucla
    {
        public OficinaAux()
        {
            this.coche = new HashSet<coche>();
        }

        public int codigo_unico_oficina { get; set; }

        public string calle { get; set; }
    
        public int numero { get; set; }
         
        public string telefono { get; set; }
     
        public int id_ciudad { get; set; }
       
        public string nombre_ciudad { get; set; }

        public virtual ciudad ciudad { get; set; } 
        public virtual ICollection<coche> coche { get; set; } 
    }

}
