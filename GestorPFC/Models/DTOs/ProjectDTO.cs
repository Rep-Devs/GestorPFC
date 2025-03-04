using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GestorPFC.Models.DTOs
{
    public class ProjectDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("estadoProyecto")]
        public string EstadoProyecto { get; set; }

        [JsonPropertyName("fechasTutoria")]
        public List<DateTime> FechasTutoria { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("fechaEntrega")]
        public DateTime FechaEntrega { get; set; }

        [JsonPropertyName("departamentoId")]
        public int DepartamentoId { get; set; }

        [JsonPropertyName("alumnoId")]
        public int AlumnoId { get; set; } // Cambiado de AlumnoName a AlumnoId

        [JsonPropertyName("tutorProyectoId")]
        public int TutorProyectoId { get; set; } // Cambiado de ProfesorName a TutorProyectoId
    }
}
