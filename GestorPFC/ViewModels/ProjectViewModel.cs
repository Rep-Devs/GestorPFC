using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using GestorPFC.Models.DTOs;
using GestorPFC.Models;
using RestAPI.Models.Entity;
using Wpf.Ui;

namespace GestorPFC.ViewModels
{
    public partial class ProjectViewModel : ViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly INavigationService _navigationService;


        [ObservableProperty]
        private List<ProjectDTO> _proyectos = new List<ProjectDTO>();


        [ObservableProperty]
        private ProjectDTO _proyectoSeleccionado;

        public ProjectViewModel(HttpClient httpClient, INavigationService navigationService)
        {
            _httpClient = httpClient;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task CargarProyectos()
        {
            try
            {

                var token = System.Windows.Application.Current.Properties["authToken"]?.ToString();


                if (string.IsNullOrEmpty(token))
                {
                    System.Windows.MessageBox.Show("No se encontró el token de autenticación.");
                    return;
                }

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

 
                var response = await _httpClient.GetAsync("https://localhost:7060/api/Proyecto");


                if (response.IsSuccessStatusCode)
                {
                    
                    var proyectos = await response.Content.ReadFromJsonAsync<List<ProjectDTO>>();

                    if (proyectos != null)
                    {
                        Proyectos = proyectos;
                    }
                }
                else
                {
                    
                    System.Windows.MessageBox.Show($"Error al cargar los proyectos. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al cargar los proyectos: {ex.Message}");
            }
        }

        public void OnPageLoaded()
        {
            CargarProyectos();
        }
    }
}
