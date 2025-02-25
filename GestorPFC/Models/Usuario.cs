using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPFC.Models
{
    public enum RolUsuario { Alumno, Profesor, Administrador }

    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string ContraseñaHash { get; set; } // Guardar la contraseña de forma segura (hash)
        public RolUsuario Rol { get; set; }

        // Se puede relacionar opcionalmente con Alumno o Profesor
        public int? AlumnoId { get; set; }
        public virtual Alumno Alumno { get; set; }

        public int? ProfesorId { get; set; }
        public virtual Profesor Profesor { get; set; }
    }

}
