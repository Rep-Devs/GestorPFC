
using System.Net.Http;
using System.Net.Http.Json;
using Wpf.Ui;
using GestorPFC.Models.DTOs.UserDTO;
using GestorPFC.Views.Pages;

namespace GestorPFC.ViewModels
{
    public partial class RegisterViewModel : ViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly INavigationService _navigationService;

        public RegisterViewModel(HttpClient httpClient, INavigationService navigationService)
        {
            _httpClient = httpClient;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private string _nombre = string.Empty;

        [ObservableProperty]
        private string _nombreUsuario = string.Empty;

        [ObservableProperty]
        private string _correo = string.Empty;

        [ObservableProperty]
        private string _contraseña = string.Empty;

        [ObservableProperty]
        private bool _isRegisterEnabled = false;


        [RelayCommand]
        private async Task Register()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(NombreUsuario) ||
                string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contraseña))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            var registerData = new UserRegistrationDTO
            {
                Name = Nombre,
                UserName = NombreUsuario,
                Email = Correo,
                Password = Contraseña,
                Role = "string" // Valor null por defecto. No apañamos la implementacion
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7060/api/User/register", registerData);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Registro exitoso. Ahora puedes iniciar sesión.");
                    _navigationService.Navigate(typeof(LoginPage));
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error en el registro: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}");
            }
        }

        [RelayCommand]
        private void NavigateToLogin()
        {
            _navigationService.Navigate(typeof(LoginPage));
        }

        partial void OnNombreChanged(string value) => ValidateRegister();
        partial void OnNombreUsuarioChanged(string value) => ValidateRegister();
        partial void OnCorreoChanged(string value) => ValidateRegister();
        partial void OnContraseñaChanged(string value) => ValidateRegister();

        private void ValidateRegister()
        {
            IsRegisterEnabled = !string.IsNullOrWhiteSpace(Nombre) &&
                                !string.IsNullOrWhiteSpace(NombreUsuario) &&
                                !string.IsNullOrWhiteSpace(Correo) &&
                                !string.IsNullOrWhiteSpace(Contraseña);
        }
    }
}
