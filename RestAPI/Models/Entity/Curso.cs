namespace RestAPI.Models.Entity
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
        // Relación muchos a muchos con Profesores
        public virtual ICollection<Profesor> Profesores { get; set; } = new List<Profesor>();

        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        // Tutor asignado al curso (un Profesor que pertenece al mismo departamento)
        public int TutorId { get; set; }
        public Profesor Tutor { get; set; } = null!;
    }
}
