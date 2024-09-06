using Avalonia.MusicStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.ViewModels
{
    public partial class MusicStoreViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _searchText;
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private AlbumViewModel? _selectedAlbum;

        public ObservableCollection<AlbumViewModel> SearchResults { get; } = [];

        public event Action<AlbumViewModel?>? BuyMusicEvent;

        [RelayCommand]
        private void BuyMusic()
        {
            BuyMusicEvent?.Invoke(SelectedAlbum);
        }

        private CancellationTokenSource? _searchCancelTokenSource;
        private readonly TimeSpan _waitTime = TimeSpan.FromMilliseconds(400);

        partial void OnSearchTextChanged(string? value)
        {
            if (_searchCancelTokenSource != null)
            {
                _searchCancelTokenSource.Cancel();
                _searchCancelTokenSource.Dispose();
            }

            _searchCancelTokenSource = new();

            Task.Run(async () =>
            {
                await Task.Delay(_waitTime, _searchCancelTokenSource.Token);

                DoSearch(SearchText);
            }, _searchCancelTokenSource.Token);
        }

        private CancellationTokenSource? _loadCoverCancelTokenSource;

        private async void DoSearch(string? searchText)
        {
            IsBusy = true;
            SearchResults.Clear();

            _loadCoverCancelTokenSource?.Cancel();
            _loadCoverCancelTokenSource = new();
            var cancellationToken = _loadCoverCancelTokenSource.Token;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var albums = await Album.SearchAsync(searchText);

                foreach (var item in albums)
                {
                    var vm = new AlbumViewModel(item);
                    SearchResults.Add(vm);
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    LoadCovers(cancellationToken);
                }
            }

            IsBusy = false;
        }

        private async void LoadCovers(CancellationToken cancellationToken)
        {
            foreach (var album in SearchResults.ToList())
            {
                await album.LoadCover();

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}
