using System;
using System.Windows.Input;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using GestorPFC.Models;
using Wpf.Ui.Controls;
using GestorPFC.Views.Pages;
using System.Windows.Navigation;

namespace GestorPFC.ViewModels
{
    public partial class LoginViewModel : ViewModel
    {
            private readonly INavigationService _navigationService;

            public LoginViewModel(INavigationService navigationService)
            {
                _navigationService = navigationService;
            }

            [RelayCommand]
            private void NavigateToRegister()
            {
            System.Windows.MessageBox.Show("⏩ Navegando a RegisterPage...");
            _navigationService.Navigate(typeof(RegisterPage));
            }

        /*
        private string _correo;
        public string Correo
        {
            get => _correo;
            set => SetProperty(ref _correo, value);
        }

        private string _contraseña;
        public string Contraseña
        {
            get => _contraseña;
            set => SetProperty(ref _contraseña, value);
        }
        /*
        public ICommand LoginCommand { get; }
        */
        [RelayCommand]
        private void Login()
        {
            _navigationService.Navigate(typeof(DashboardPage));
        }
        /*
        private bool ValidarUsuario(string email, string password)
        {
            return email == "admin@correo.com" && password == "1234";
        }
        */
    }
}

