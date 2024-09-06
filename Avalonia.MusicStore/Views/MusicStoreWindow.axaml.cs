using Avalonia.Controls;

namespace Avalonia.MusicStore.Views;

public partial class MusicStoreWindow : Window
{
#if DEBUG
    public MusicStoreWindow()
    {
        InitializeComponent();
    }
#endif

    public MusicStoreWindow(MusicStoreView view)
    {
        this.Content = view;

        InitializeComponent();

        view.ViewModel.EnsureSelectEvent += Close;
    }
}