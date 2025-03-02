namespace RestAPI.Models.Entity
{
    public class User
    {
        // Identificador del usuario (por ejemplo, generado por Identity)
        public string Id { get; set; } = null!;

        // Nombre completo del usuario
        public string Name { get; set; } = null!;

        // Nombre de usuario (usado para login)
        public string UserName { get; set; } = null!;

        // Correo electrónico del usuario
        public string Email { get; set; } = null!;

        // Rol asignado al usuario (Solo puede haber tres roles: admin, profesor, alumno")
        public string Role { get; set; } = null!;
    }
}
