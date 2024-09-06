﻿using Avalonia.Media.Imaging;
using Avalonia.MusicStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.ViewModels
{
    public partial class AlbumViewModel : ViewModelBase
    {
        private readonly Album _album;

        [ObservableProperty]
        private Bitmap? _cover;
        public AlbumViewModel(Album album)
        {
            _album = album;
        }

        public string Artist => _album.Artist;
        public string Title => _album.Title;

        public async Task LoadCover()
        {
            await using var imageStream = await _album.LoadCoverBitmapAsync();
            Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }

        public async Task SaveToDiskAsync()
        {
            await _album.SaveAsync();

            if (Cover != null)
            {
                var bitmap = Cover;

                await Task.Run(() =>
                {
                    using var fs = _album.SaveCoverBitmapStream();
                    bitmap.Save(fs);
                });
            }
        }
    }
}
