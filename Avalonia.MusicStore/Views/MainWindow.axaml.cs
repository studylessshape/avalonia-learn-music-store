using Avalonia.Controls;
using Avalonia.MusicStore.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.Views;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;

    public MainWindow(MainView mainView, IServiceProvider serviceProvider)
    {
        this.Content = mainView;
        this._serviceProvider = serviceProvider;
        
        var mainViewModel = _serviceProvider.GetService<MainViewModel>();
        if (mainViewModel != null )
        {
            mainViewModel.GetAlbumEvent += DoShowDialog;
        }

        InitializeComponent();
    }

    private async Task<AlbumViewModel?> DoShowDialog()
    {
        var storeWindow = _serviceProvider.GetService<MusicStoreWindow>();

        if (storeWindow is null) return null;

        return await storeWindow.ShowDialog<AlbumViewModel?>(this);
    }
}
