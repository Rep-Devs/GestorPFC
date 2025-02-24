
using Wpf.Ui;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace GestorPFC.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private bool _isInitialized = false;
        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
            }
        }

        private void InitializeViewModel()
        {
            _isInitialized = true;

        }
    }
}
