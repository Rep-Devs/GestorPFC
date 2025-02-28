


namespace GestorPFC.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : INavigableView<RegisterViewModel>
    {
        public RegisterViewModel ViewModel { get; }
        public RegisterPage() { }
        public RegisterPage(RegisterViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}
