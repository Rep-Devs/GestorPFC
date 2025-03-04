using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GestorPFC.Models.DTOs;
using System.Windows;
using Wpf.Ui;

namespace GestorPFC.ViewModels
{
    public partial class ProposalViewModel : ViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly INavigationService _navigationService;


        [ObservableProperty]
        private List<ProposalDTO> _propuestas = new List<ProposalDTO>();


        [ObservableProperty]
        private ProposalDTO _propuestaSeleccionada;

        public ProposalViewModel(HttpClient httpClient, INavigationService navigationService)
        {
            _httpClient = httpClient;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task CargarPropuestas()
        {
            try
            {

                var token = Application.Current.Properties["authToken"]?.ToString();

                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("No se encontró el token de autenticación.");
                    return;
                }


                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


                var response = await _httpClient.GetAsync("https://localhost:7060/api/Propuesta");

                if (response.IsSuccessStatusCode)
                {

                    var propuestas = await response.Content.ReadFromJsonAsync<List<ProposalDTO>>();

                    if (propuestas != null)
                    {
                        Propuestas = propuestas;
                    }
                }
                else
                {

                    MessageBox.Show($"Error al cargar las propuestas. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las propuestas: {ex.Message}");
            }
        }

        public void OnPageLoaded()
        {
            CargarPropuestas();
        }
    }
}
