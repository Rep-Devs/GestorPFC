using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPFC.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }

        public int DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }
    }

}
