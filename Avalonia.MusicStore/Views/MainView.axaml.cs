using Avalonia.Controls;
using Avalonia.MusicStore.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Avalonia.MusicStore.Views;

public partial class MainView : UserControl
{
#if DEBUG
    public  MainView()
    {
        InitializeComponent();
    }
#endif

    public MainView(MainViewModel mainViewModel)
    {
        this.DataContext = mainViewModel;

        InitializeComponent();
    }
}
