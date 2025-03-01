using GestorPFC.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace GestorPFC.ViewModels
{
    public partial class RegisterViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;


        public RegisterViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
        }

        [RelayCommand]
        private void NavigateToLogin()
        {
            System.Windows.MessageBox.Show("⏩ Navegando a LoginPage...");
            _navigationService.Navigate(typeof(LoginPage));
        }
    }
}
