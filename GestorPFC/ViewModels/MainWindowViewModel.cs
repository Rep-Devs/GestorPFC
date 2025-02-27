
using System.Collections.ObjectModel;
using Wpf.Ui;

namespace GestorPFC.ViewModels
{
    public partial class MainWindowViewModel : ViewModel
    {
        private bool _isInitialized = false;

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

            _isInitialized = true;
        }


    }
}
