

using Wpf.Ui;

namespace GestorPFC.ViewModels
{
    public partial class ProjectViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;


        public ProjectViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
        }
    }
}
