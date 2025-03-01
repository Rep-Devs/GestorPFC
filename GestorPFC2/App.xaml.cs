﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.IO;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;

namespace GestorPFC2;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
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
     _ = services.AddHostedService<ApplicationHostService>();

     // Theme manipulation
     _ = services.AddSingleton<IThemeService, ThemeService>();

     // TaskBar manipulation
     _ = services.AddSingleton<ITaskBarService, TaskBarService>();

     // Navigation service
     _ = services.AddSingleton<INavigationService, NavigationService>();

     // Page service (necesario para la navegación)
     //_ = services.AddSingleton<IPageService, PageService>();

     // Main Window with Navigation
     _ = services.AddSingleton<INavigationWindow, Views.MainWindow>();
     _ = services.AddSingleton<MainWindowViewModel>();

     // ViewModels
     _ = services.AddSingleton<LoginViewModel>();
     _ = services.AddSingleton<DashboardViewModel>();

     // Pages
     _ = services.AddSingleton<LoginPage>();
     _ = services.AddSingleton<DashboardPage>();
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
