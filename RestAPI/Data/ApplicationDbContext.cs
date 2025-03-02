using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Entity;

namespace RestAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets para cada entidad
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Propuesta> Propuestas { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación muchos a muchos entre Curso y Profesor sin cascada en ninguno de los lados
            modelBuilder.Entity<Curso>()
                .HasMany(c => c.Profesores)
                .WithMany(p => p.Cursos)
                .UsingEntity<Dictionary<string, object>>(
                    "CursoProfesores",
                    j => j
                        .HasOne<Profesor>()
                        .WithMany()
                        .HasForeignKey("ProfesoresId")
                        .OnDelete(DeleteBehavior.NoAction),
                    j => j
                        .HasOne<Curso>()
                        .WithMany()
                        .HasForeignKey("CursosId")
                        .OnDelete(DeleteBehavior.NoAction)
                );

            // Relación uno a muchos para el Tutor del curso
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Tutor)
                .WithMany() // Sin navegación inversa para evitar ambigüedades
                .HasForeignKey(c => c.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar la relación entre Proyecto y Departamento para evitar cascadas múltiples.
            modelBuilder.Entity<Proyecto>()
                .HasOne(p => p.Departamento)
                .WithMany() // Asumiendo que Departamento no tiene navegación inversa a Proyectos.
                .HasForeignKey(p => p.DepartamentoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Si es necesario, podrías configurar otras relaciones (por ejemplo, Proyecto con Alumno o TutorProyecto)
        }
    }
}
