namespace RestAPI.Models.DTOs.ProyectoDTO
{
    public class ProyectoDto : CreateProyectoDTO
    {
        public int Id { get; set; }
        public string EstadoProyecto { get; set; } = null!;
        // Lista de fechas para tutorías
        public ICollection<DateTime> FechasTutoria { get; set; } = new List<DateTime>();
    }
}
