using Avalonia.MusicStore.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public event Func<Task<AlbumViewModel?>>? GetAlbumEvent;

    public MainViewModel()
    {
        LoadAlbums();
    }

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

    private async void LoadAlbums()
    {
        var albums = (await Album.LoadCacheAsync()).Select(x =>  new AlbumViewModel(x));

        foreach (var album in albums)
        {
            Albums.Add(album);
        }

        foreach (var album in Albums.ToList())
        {
            await album.LoadCover();
        }
    }
}
