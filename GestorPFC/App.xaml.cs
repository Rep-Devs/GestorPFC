
using System.IO;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.DependencyInjection;
using GestorPFC.Services;
using GestorPFC.Views.Pages;
using GestorPFC.ViewModels;

namespace GestorPFC
{
    public partial class App
    {

        private static readonly IHost _host = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration(c =>
                    {
                        var basePath =
                            Path.GetDirectoryName(AppContext.BaseDirectory)
                            ?? throw new DirectoryNotFoundException(
                                "Unable to find the base directory of the application."
                            );
                        _ = c.SetBasePath(basePath);
                    })
            .ConfigureServices((context, services) =>
            {
                // App Host
                services.AddHostedService<ApplicationHostService>();

                // Registrar Navegación con Wpf.Ui
                services.AddNavigationViewPageProvider();
                services.AddSingleton<INavigationService, NavigationService>();

                // Registrar Servicios
                services.AddSingleton<ApiService>();

                // Registrar Ventana Principal y ViewModel
                services.AddSingleton<INavigationWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();

                // Registrar Páginas y ViewModels
                services.AddSingleton<LoginPage>();
                services.AddSingleton<LoginViewModel>();
            })
            .Build();


        /// <summary>
        /// Gets services.
        /// </summary>
        public static IServiceProvider Services
        {
            get { return _host.Services; }
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }
    }
}
