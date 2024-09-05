using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.MusicStore.ViewModels;

namespace Avalonia.MusicStore;

public partial class MusicStoreView : UserControl
{
    public MusicStoreView(MusicStoreViewModel viewModel)
    {
        this.DataContext = viewModel;

        InitializeComponent();
        ViewModel = viewModel;
    }

    public MusicStoreViewModel ViewModel { get; }
}