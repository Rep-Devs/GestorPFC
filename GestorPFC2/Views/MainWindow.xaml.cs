
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Controls;

namespace GestorPFC2.Views
{
    public partial class MainWindow : INavigationWindow
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(MainWindowViewModel viewModel, INavigationService navigationService, IPageService pageService)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            // Configura el control de navegación
            navigationService.SetNavigationControl(RootNavigation);

            // Configura el servicio de páginas
            SetPageService(pageService);
        }

        public INavigationView GetNavigation() => RootNavigation;
        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);
        public void ShowWindow() => Show();
        public void CloseWindow() => Close();

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Asegúrate de que al cerrar esta ventana se inicie el proceso de cierre de la aplicación.
            Application.Current.Shutdown();
        }

        public void SetPageService(IPageService pageService)
        {
            // Configura el servicio de páginas en el control de navegación
            RootNavigation.SetPageService(pageService);
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
