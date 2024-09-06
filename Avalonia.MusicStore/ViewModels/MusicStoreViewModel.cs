using Avalonia.MusicStore.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
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

        public event Action<AlbumViewModel?>? EnsureSelectEvent;

        [RelayCommand]
        private void EnsureSelect()
        {
            EnsureSelectEvent?.Invoke(SelectedAlbum);
        }

        private CancellationTokenSource? _cancelToken = null;
        private readonly TimeSpan _waitTime = TimeSpan.FromMilliseconds(400);

        partial void OnSearchTextChanged(string? value)
        {
            if (_cancelToken != null)
            {
                _cancelToken.Cancel();
                _cancelToken.Dispose();
            }

            _cancelToken = new();

            Task.Run(async () =>
            {
                await Task.Delay(_waitTime, _cancelToken.Token);

                DoSearch(SearchText);
            }, _cancelToken.Token);
        }

        private async void DoSearch(string? searchText)
        {
            IsBusy = true;
            SearchResults.Clear();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var albums = await Album.SearchAsync(searchText);

                foreach (var item in albums)
                {
                    var vm = new AlbumViewModel(item);
                    SearchResults.Add(vm);
                }
            }

            IsBusy = false;
        }
    }
}
