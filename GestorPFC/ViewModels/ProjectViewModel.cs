using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using RestAPI.Models.Entity;

namespace GestorPFC.ViewModels
{
    public class ProjectViewModel : ViewModel
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Proyecto> Proyectos { get; set; }
        public Proyecto ProyectoSeleccionado { get; set; }

        public ICommand CargarProyectosCommand { get; }

        public ProjectViewModel()
        {
            _httpClient = new HttpClient();
            Proyectos = new ObservableCollection<Proyecto>();
            CargarProyectosCommand = new RelayCommand(async () => await CargarProyectos());
            _ = CargarProyectos();
        }

        private async Task CargarProyectos()
        {
            string apiUrl = "https://localhost:7060/api/Proyecto"; 

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var proyectos = JsonSerializer.Deserialize<List<Proyecto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    Proyectos.Clear();
                    foreach (var proyecto in proyectos)
                    {
                        Proyectos.Add(proyecto);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al obtener proyectos: {ex.Message}");
            }
        }
    }
}
