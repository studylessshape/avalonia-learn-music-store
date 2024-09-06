using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public event Func<Task<AlbumViewModel?>>? GetAlbumEvent;

    [RelayCommand]
    private async void BuyMusic()
    {
        var task = GetAlbumEvent?.Invoke();
        var result = task == null ? null : await task;

        if (result != null)
        {
            Albums.Add(result);
            await result.SaveToDiskAsync();
        }
    }

    public ObservableCollection<AlbumViewModel> Albums { get; } = new();

}
