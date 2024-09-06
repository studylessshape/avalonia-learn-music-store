using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Avalonia.MusicStore.ViewModels
{
    public partial class MusicStoreViewModel : ViewModelBase
    {
        [ObservableProperty]
        private AlbumViewModel? _result;

        public event Action<AlbumViewModel?>? EnsureSelectEvent;

        [RelayCommand]
        private void EnsureSelect()
        {
            EnsureSelectEvent?.Invoke(Result);
        }
    }
}
