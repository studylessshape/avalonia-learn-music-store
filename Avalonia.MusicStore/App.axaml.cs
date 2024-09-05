using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.MusicStore.Services;
using Avalonia.MusicStore.ViewModels;
using Avalonia.MusicStore.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Avalonia.MusicStore;

public partial class App : Application
{
    private IServiceProvider? _serviceProvider;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        var serviceCollection = new ServiceCollection();
        serviceCollection.RegistAllServices();

        _serviceProvider = serviceCollection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = _serviceProvider.GetService<MainWindow>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = _serviceProvider.GetService<MainView>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
