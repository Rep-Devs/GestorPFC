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
    }
}
