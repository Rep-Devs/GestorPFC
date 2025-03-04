namespace GestorPFC.Views.Pages
{
    public partial class RegisterPage : INavigableView<RegisterViewModel>
    {
        public RegisterViewModel ViewModel { get; }

        public RegisterPage(RegisterViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}

