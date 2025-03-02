namespace RestAPI.Models.Entity
{
    public class Profesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;

        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;

        // Relación muchos a muchos con Cursos
        public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    }
}
