using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Wpf.Ui;
using Wpf.Ui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GestorPFC.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = "GestorPFC";

        [ObservableProperty]
        private ObservableCollection<object> _navigationItems = [];

        [ObservableProperty]
        private ObservableCollection<object> _navigationFooter = [];

        [ObservableProperty]
        private Visibility navigationVisibility = Visibility.Hidden; // Oculto al inicio

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            if (!_isInitialized)
            {
                InitializeViewModel();
                _ = ShowNavigationAfterDelay(); // Ejecutar delay al iniciar
            }
        }

        private void InitializeViewModel()
        {
            ApplicationTitle = "GestorPFC";
            NavigationItems =
            [
                new NavigationViewItem()
                {
                    Content = "Propuestas",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Add16 },
                    TargetPageType = typeof(Views.Pages.ProposalPage)
                },
                new NavigationViewItem()
                {
                    Content = "Proyectos",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Album20 },
                    TargetPageType = typeof(Views.Pages.ProjectPage)
                },
                new NavigationViewItem()
                {
                    Content = "Calendario",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.CalendarDay24 },
                    TargetPageType = typeof(Views.Pages.CalendarPage)
                },
            ];

            NavigationFooter =
            [
                new NavigationViewItem()
            {
                Content = "Perfil",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.ProfilePage)
            },
        ];

            _isInitialized = true;
        }

        public async Task ShowNavigationAfterDelay()
        {
            await Task.Delay(500); 
            NavigationVisibility = Visibility.Visible; 
        }
    }
}
