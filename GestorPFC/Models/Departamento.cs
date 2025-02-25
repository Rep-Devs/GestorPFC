using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPFC.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Profesor> Profesores { get; set; }
    }

}
