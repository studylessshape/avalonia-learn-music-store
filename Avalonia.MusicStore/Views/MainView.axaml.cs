using Avalonia.Controls;
using Avalonia.MusicStore.ViewModels;

namespace Avalonia.MusicStore.Views;

public partial class MainView : UserControl
{
    public MainView(MainViewModel viewModel)
    {
        this.DataContext = viewModel;

        InitializeComponent();
    }
}
