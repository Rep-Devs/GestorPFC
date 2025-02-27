using GestorPFC.ViewModels;
using System;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Controls;

namespace GestorPFC.Views
{
    public partial class MainWindow : INavigationWindow
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(MainWindowViewModel viewModel, INavigationService navigationService)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            if (RootNavigation == null)
            {
                throw new InvalidOperationException("RootNavigation no está inicializado. Verifica el XAML.");
            }

            navigationService.SetNavigationControl(RootNavigation);
        }

        public INavigationView GetNavigation() => RootNavigation;
        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);
        public void ShowWindow() => Show();
        public void CloseWindow() => Close();

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Make sure that closing this window will begin the process of closing the application.
            Application.Current.Shutdown();
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            RootNavigation.SetServiceProvider(serviceProvider);
        }

        public void SetPageService(INavigationViewPageProvider navigationViewPageProvider)
        {
            throw new NotImplementedException();
        }
        public void SetPageService(IPageService pageService)
        {
            RootNavigation.SetPageService(pageService);
        }
    }
}
