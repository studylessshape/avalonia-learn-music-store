using Avalonia.Controls;
using Avalonia.MusicStore.ViewModels;

namespace Avalonia.MusicStore;

public partial class MusicStoreView : UserControl
{
#if DEBUG
    public MusicStoreView()
    {
        InitializeComponent();
    }
#endif
    public MusicStoreView(MusicStoreViewModel viewModel)
    {
        this.DataContext = viewModel;

        InitializeComponent();
        ViewModel = viewModel;
    }

    public MusicStoreViewModel ViewModel { get; }
}