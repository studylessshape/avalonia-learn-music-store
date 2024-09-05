using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public event Func<Task<AlbumViewModel?>>? GetAlbumEvent;

    [RelayCommand]
    private async void BuyMusic()
    {
        var album = await GetAlbumEvent?.Invoke()!;
    }
}
