

using Wpf.Ui;

namespace GestorPFC.ViewModels
{
    public partial class ProposalViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;


        public ProposalViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
        }
    }
}
