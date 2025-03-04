using GestorPFC.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace GestorPFC.ViewModels
{
    public partial class ProfileViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;


        public ProfileViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
        }

        [RelayCommand]
        public void Logout()
        {
            // Eliminar el token de autenticación
            System.Windows.Application.Current.Properties["authToken"] = null;

            // Redirigir a la página de login
            _navigationService.Navigate(typeof(LoginPage));
        }
    }
}
