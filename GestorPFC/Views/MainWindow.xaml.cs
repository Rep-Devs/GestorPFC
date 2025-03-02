
using GestorPFC.ViewModels;
using GestorPFC.Views.Pages;
using System.Windows.Media;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace GestorPFC.Views
{


    public partial class MainWindow : INavigationWindow
    {

        public MainViewModel ViewModel { get; }
        public MainWindow(MainViewModel viewModel, INavigationService navigationService)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

            navigationService.SetNavigationControl(RootNavigation);

            SetPaneControl();
            
        }

        public void SetPaneControl()
        {

            RootNavigation.Navigated += async (s, e) =>
            {
                
                
                var currentPage = e.Page;

                if (currentPage is LoginPage || currentPage is RegisterPage)
                {
                    RootNavigation.IsPaneVisible = false;
                    RootNavigation.OpenPaneLength = 0;
                    RootNavigation.CompactPaneLength = 0;

                }
                else
                {

                    RootNavigation.OpenPaneLength = 175;
                    RootNavigation.CompactPaneLength = double.NaN;
                }
            };
        }


        public INavigationView GetNavigation() => RootNavigation;

        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        public void ShowWindow() => Show();
        public void CloseWindow() => Close();

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        public void SetPageService(IPageService pageService)
        {

            RootNavigation.SetPageService(pageService);
        }

    }
}
