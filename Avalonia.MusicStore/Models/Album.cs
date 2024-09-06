using iTunesSearch.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.Models;

public class Album
{
    private static iTunesSearchManager s_SearchManager = new();

    public string Artist { get; set; }
    public string Title { get; set; }
    public string CoverUrl { get; set; }

    public Album(string artist, string title, string coverUrl)
    {
        Artist = artist;
        Title = title;
        CoverUrl = coverUrl;
    }

    public static async Task<IEnumerable<Album>> SearchAsync(string searchTerm)
    {
        var query = await s_SearchManager.GetAlbumsAsync(searchTerm).ConfigureAwait(false);

        return query.Albums.Select(x =>
            new Album(x.ArtistName,
                      x.CollectionName,
                      x.ArtworkUrl100.Replace("100x100b", "600x600b")));
    }

    private static HttpClient s_HttpClient = new();
    private string CachePath
    {
        get
        {
            string path = $"./Cache/{Artist} - {Title}";
            var processDirectory = Path.GetDirectoryName(Environment.ProcessPath);
            if (processDirectory != null)
            {
                path = Path.Combine(processDirectory, path);
            }

            return path;
        }
    }

    public async Task<Stream> LoadCoverBitmapAsync()
    {
        if (File.Exists(CachePath + ".bmp"))
        {
            return File.OpenRead(CachePath + ".bmp");
        }
        else
        {
            var data = await s_HttpClient.GetByteArrayAsync(CoverUrl);
            return new MemoryStream(data);
        }
    }
}
