using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPFC.Models
{
    public enum EstadoProyecto { Desarrollo, Aprobado, Suspendido, Revision }

    public class Proyecto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public DateTime FechaEntrega { get; set; }
        public EstadoProyecto EstadoProyecto { get; set; }

        public int DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }

        public virtual ICollection<DateTime> FechasTutoria { get; set; }

        public int AlumnoId { get; set; }
        public virtual Alumno Alumno { get; set; }

        public int? TutorProyectoId { get; set; }
        public virtual Profesor TutorProyecto { get; set; }

        // Constructor que crea el proyecto a partir de una propuesta aprobada
        public Proyecto(Propuesta propuesta)
        {
            if (propuesta == null || propuesta.Estado != EstadoPropuesta.Aceptada || !propuesta.BooleanProyecto)
                throw new ArgumentException("La propuesta no es válida para crear un proyecto.");

            Titulo = propuesta.Titulo;
            Descripcion = propuesta.Descripcion;
            AlumnoId = propuesta.AlumnoId;
            // Se asume que el Departamento se obtiene a través del Curso del Alumno
            Departamento = propuesta.Alumno.Curso.Departamento;
        }

        public Proyecto() { }
    }

}
