using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace Avalonia.MusicStore.ViewModels
{
    public partial class MusicStoreViewModel : ViewModelBase
    {
        public MusicStoreViewModel()
        {
            SearchResults.Add(new AlbumViewModel());
            SearchResults.Add(new AlbumViewModel());
            SearchResults.Add(new AlbumViewModel());
        }

        [ObservableProperty]
        private string _searchText;
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private AlbumViewModel? _selectedAlbum;

        public ObservableCollection<AlbumViewModel> SearchResults { get; } = [];

        public event Action<AlbumViewModel?>? EnsureSelectEvent;

        [RelayCommand]
        private void EnsureSelect()
        {
            EnsureSelectEvent?.Invoke(SelectedAlbum);
        }
    }
}
