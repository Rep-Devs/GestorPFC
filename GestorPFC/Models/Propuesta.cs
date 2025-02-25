using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPFC.Models
{
    public enum EstadoPropuesta { StandBy, Aceptada, Denegada }

    public class Propuesta
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaGestion { get; set; }

        public EstadoPropuesta Estado { get; set; }
        public string Departamento { get; set; }
        public bool BooleanProyecto { get; set; }

        public int AlumnoId { get; set; }
        public virtual Alumno Alumno { get; set; }
    }

}
