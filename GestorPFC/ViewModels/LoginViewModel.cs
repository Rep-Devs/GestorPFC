using System;
using System.Windows.Input;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using GestorPFC.Models;
using Wpf.Ui.Controls;
using GestorPFC.Views.Pages;
using System.Windows.Navigation;
using System.Net.Http;
using System.Net.Http.Json;
using GestorPFC.Models.DTOs.UserDTO;

namespace GestorPFC.ViewModels
{
    public partial class LoginViewModel : ViewModel
    {

        private readonly HttpClient _httpClient;
        private readonly INavigationService _navigationService;


        public LoginViewModel(HttpClient httpClient, INavigationService navigationService)
        {
            _httpClient = httpClient;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            _navigationService.Navigate(typeof(RegisterPage));
        }

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private bool _isLoginEnabled = false; // Botón deshabilitado por defecto

        [RelayCommand]
        private async Task CheckLogin()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                System.Windows.MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            var loginData = new UserLoginDTO
            {
                UserName = Username,
                Password = Password
            };

            try
            {
                // Enviar la solicitud POST al backend para hacer el login
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7060/api/User/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    // Deserializa la respuesta
                    var apiResponse = await response.Content.ReadFromJsonAsync<UserLoginResponseDTO>();

                    if (apiResponse?.IsSuccess == true && apiResponse.Result != null)
                    {
                        var userResponse = apiResponse.Result;

                        // Login exitoso, almacena el token
                        string token = userResponse.Token;

                        // Guardar el token para usarlo en futuras peticiones
                        System.Windows.Application.Current.Properties["authToken"] = token;

                        // Mensaje de bienvenida con el nombre del usuario
                        System.Windows.MessageBox.Show($"Bienvenido {userResponse.User.Name}");

                        // Redirige al Dashboard u otra página
                        _navigationService.Navigate(typeof(ProjectPage));
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Error en la autenticación.");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error de conexión: {ex.Message}");
            }
        }




        partial void OnUsernameChanged(string value) => ValidateLogin();
        partial void OnPasswordChanged(string value) => ValidateLogin();

        private void ValidateLogin()
        {
            IsLoginEnabled = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        [RelayCommand]
        private void Login()
        {
            _navigationService.Navigate(typeof(DashboardPage));
        }
    }


}

