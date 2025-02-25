using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPFC.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
        public virtual ICollection<Profesor> Profesores { get; set; }

        public int DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }

        // Tutor asignado al curso
        public int TutorId { get; set; }
        public virtual Profesor Tutor { get; set; }
    }

}
