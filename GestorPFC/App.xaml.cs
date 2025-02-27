
using System.IO;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.DependencyInjection;
using Wpf.Ui;
using GestorPFC.ViewModels;
using GestorPFC.Views.Pages;
using GestorPFC.Services;

namespace GestorPFC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly IHost _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(c =>
            {
                var basePath = Path.GetDirectoryName(AppContext.BaseDirectory)
                               ?? throw new DirectoryNotFoundException("Unable to find base directory.");
                _ = c.SetBasePath(basePath);
            })
            .ConfigureServices((context, services) =>
            {
                _ = services.AddNavigationViewPageProvider();
                // App Host
                _ = services.AddHostedService<ApplicationHostService>();

                // Navigation service
                _ = services.AddSingleton<INavigationService, NavigationService>();

                // Main Window with Navigation
                _ = services.AddSingleton<INavigationWindow, Views.MainWindow>();
                _ = services.AddSingleton<MainWindowViewModel>();

                // ViewModels
                _ = services.AddSingleton<LoginViewModel>();
                _ = services.AddSingleton<DashboardViewModel>();

                // Pages
                _ = services.AddSingleton<LoginPage>();
                _ = services.AddSingleton<DashboardPage>();

                // Configuration
                _ = services.Configure<Utils.AppConfig>(context.Configuration.GetSection(nameof(Utils.AppConfig)));
            })
            .Build();

        /// <summary>
        /// Gets the application's service provider.
        /// </summary>
        public static IServiceProvider Services => _host.Services;

        /// <summary>
        /// Occurs when the application starts.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

        }

        /// <summary>
        /// Occurs when the application exits.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Handles unhandled exceptions.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
        }
    }
}
