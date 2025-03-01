using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace GestorPFC2.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private INavigationWindow? _navigationWindow;

        public ApplicationHostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await HandleActivationAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        private async Task HandleActivationAsync()
        {
            await Task.CompletedTask;

            if (!Application.Current.Windows.OfType<Views.MainWindow>().Any())
            {
                // Obtén la instancia de INavigationWindow desde el contenedor de dependencias
                _navigationWindow = _serviceProvider.GetService<INavigationWindow>();

                if (_navigationWindow == null)
                {
                    throw new InvalidOperationException("No se pudo resolver INavigationWindow desde el contenedor de dependencias.");
                }

                // Muestra la ventana
                _navigationWindow.ShowWindow();

                // Navega a la página de inicio
                //_ = _navigationWindow.Navigate(typeof(Views.Pages.DashboardPage));
            }

            await Task.CompletedTask;
        }
    }
}