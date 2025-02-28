
using GestorPFC.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace GestorPFC.ViewModels
{
    public partial class MainWindowViewModel : ViewModel
    {
        private bool _isInitialized = false;

        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private string _applicationTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<object> _navigationItems = [];

        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
            }

        }


        private void InitializeViewModel()
        {
            
            ApplicationTitle = "GestorPFC";
            

            NavigationItems =
[
    new NavigationViewItem()
            {
                Content = "Home",

            },
            new NavigationViewItem()
            {
                Content = "Data",

            },
        ];

            _isInitialized = true;
        }


    }
}
