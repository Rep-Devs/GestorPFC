using Microsoft.AspNetCore.Identity;

namespace RestAPI.Models.Entity
{
    public class AppUser : IdentityUser
    {
        // Propiedad adicional para el nombre completo del usuario.
        public string Name { get; set; }

        // Propiedad adicional para el email completo del usuario.
        public string Email { get; set; }

    }
}
