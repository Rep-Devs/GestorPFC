using GestorPFC.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestorPFC.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly ApiService _apiService;
        private string _username;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _apiService = new ApiService();
            LoginCommand = new RelayCommand<string>(async (password) => await Login(password));

        }

        private async Task Login(string password)
        {
            bool isAuthenticated = await _apiService.Login(Username, password);

            if (isAuthenticated)
            {
                ErrorMessage = "Bienvenido!";
                // Aquí podrías redirigir al DashboardPage
            }
            else
            {
                ErrorMessage = "Usuario o contraseña incorrectos";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
