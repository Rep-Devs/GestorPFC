namespace RestAPI.Models.DTOs.PropuestaDTO
{
    public class PropuestaDto : CreatePropuestaDTO
    {
        public int Id { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaGestion { get; set; }
        // Estado puede ser "StandBy", "Aceptada" o "Denegada"
        public string Estado { get; set; } = null!;
    }
}
