using Avalonia.MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.ViewModels
{
    public partial class AlbumViewModel : ViewModelBase
    {
        private readonly Album _album;

        public AlbumViewModel(Album album)
        {
            _album = album;
        }

        public string Artist => _album.Artist;
        public string Title => _album.Title;
    }
}
