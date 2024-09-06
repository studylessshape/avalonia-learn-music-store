namespace Avalonia.MusicStore.Services
{
    public interface IBindable<TBindObj>
    {
        void Bind(TBindObj obj);
    }
}
