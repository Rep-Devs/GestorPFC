

namespace GestorPFC.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para LoginPage.xaml
    /// </summary>
    public partial class LoginPage : INavigableView<LoginViewModel>
    {
        public LoginViewModel ViewModel { get; }
        public LoginPage(LoginViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}
