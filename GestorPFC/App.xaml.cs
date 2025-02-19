using GestorPFC.Services;
using GestorPFC.ViewModels;
using GestorPFC.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;

namespace GestorPFC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Contenedor de servicios
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            // Configurar la inyección de dependencias
            var services = new ServiceCollection();

            // Servicios
            services.AddSingleton<ApiService>();
            services.AddSingleton<NavigationService>();

            // ViewModels
            services.AddSingleton<LoginViewModel>();
            //services.AddSingleton<DashboardViewModel>();

            // Views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginPage>();
            //services.AddSingleton<DashboardPage>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Obtener la ventana principal desde el contenedor de servicios
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }

}
