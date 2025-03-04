namespace RestAPI.Models.Entity
{
    public enum EstadoProyecto
    {
        Desarrollo,
        Aprobado,
        Suspendido,
        Revision
    }

    public class Proyecto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public DateTime FechaEntrega { get; set; }
        public EstadoProyecto EstadoProyecto { get; set; }

        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        // Colección de fechas para tutorías
        public virtual ICollection<DateTime> FechasTutoria { get; set; } = new List<DateTime>();

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        // Tutor asignado al proyecto (un Profesor)
        public int? TutorProyectoId { get; set; }
        public Profesor TutorProyecto { get; set; } = null!;

        // Constructor basado en una propuesta aprobada, debera ir a la interfaz grafica en una validacion al cambiar el estado de la propuesta
        public Proyecto(Propuesta propuesta)
        {
            if (propuesta == null || propuesta.Estado != EstadoPropuesta.Aceptada || !propuesta.BooleanProyecto)
                throw new ArgumentException("La propuesta no es válida para crear un proyecto.");

            Titulo = propuesta.Titulo;
            Descripcion = propuesta.Descripcion;
            AlumnoId = propuesta.AlumnoId;
            // Se asume que el Departamento se obtiene a través del curso del alumno.
            Departamento = propuesta.Alumno.Curso.Departamento;
        }

        // Constructor por defecto para EF Core.
        public Proyecto() { }
    }
}
