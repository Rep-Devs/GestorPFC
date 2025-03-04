using System.Text.Json.Serialization;

namespace GestorPFC.Models.DTOs
{
    public class ProposalDTO
    {
        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("departamento")]
        public string Departamento { get; set; }

        [JsonPropertyName("booleanProyecto")]
        public bool BooleanProyecto { get; set; }

        [JsonPropertyName("alumnoId")]
        public int AlumnoId { get; set; }
    }
}
