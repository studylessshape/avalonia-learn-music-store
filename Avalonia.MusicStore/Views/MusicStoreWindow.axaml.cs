using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.MusicStore;

public partial class MusicStoreWindow : Window
{
    public MusicStoreWindow(MusicStoreView view)
    {
        this.Content = view;
        InitializeComponent();
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        base.OnClosing(e);
    }
}