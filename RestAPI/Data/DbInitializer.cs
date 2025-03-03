using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            // Asegurarse de que la base de datos esté creada y aplicar migraciones pendientes.
            await context.Database.MigrateAsync();

            // 1. Sembrar el usuario administrador
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Configura las credenciales del administrador; podrías leer esto desde la configuración si lo prefieres.
            string adminRole = "admin";
            string adminUserName = "admin";
            string adminEmail = "admin@example.com";
            string adminPassword = "UnaContraseñaSegura@123";

            // Crear el rol admin si no existe.
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Verificar si el usuario admin ya existe.
            var adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    Name = "Administrador"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Error al crear el usuario admin: {errors}");
                }

                await userManager.AddToRoleAsync(adminUser, adminRole);
            }

            // 2. Sembrar Departamento si no existe ninguno.
            if (!context.Departamentos.Any())
            {
                var departamento = new Departamento
                {
                    Nombre = "Departamento Inicial"
                };
                context.Departamentos.Add(departamento);
                await context.SaveChangesAsync();
            }

            // Obtener el departamento (el primero existente).
            var dept = context.Departamentos.First();

            // 3. Sembrar Profesor si no existe ninguno.
            if (!context.Profesores.Any())
            {
                var profesor = new Profesor
                {
                    Nombre = "Profesor Inicial",
                    Apellido = "ApellidoInicial",
                    Email = "profesor.inicial@example.com",
                    DepartamentoId = dept.Id,
                    Departamento = dept
                };
                context.Profesores.Add(profesor);
                await context.SaveChangesAsync();
            }

            // 4. Sembrar Curso si no existe ninguno.
            if (!context.Cursos.Any())
            {
                // Obtener el profesor creado para asignarlo como tutor.
                var tutor = context.Profesores.First();
                var curso = new Curso
                {
                    Nombre = "Curso Inicial",
                    DepartamentoId = dept.Id,
                    Departamento = dept,
                    TutorId = tutor.Id,
                    Tutor = tutor
                };
                context.Cursos.Add(curso);
                await context.SaveChangesAsync();
            }
        }
    }
}
