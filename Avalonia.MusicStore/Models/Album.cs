using Avalonia.Media;
using Avalonia.MusicStore.Extensions;
using iTunesSearch.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
    private static string CacheDirectory
    {
        get
        {
            var directory = Path.GetDirectoryName(Environment.ProcessPath);
            if (directory is null)
            {
                return "./Cache";
            }
            else
            {
                return Path.Combine(directory, "./Cache");
            }
        }
    }
    private string CachePath => Path.Combine(CacheDirectory, $"{Artist} - {Title}".ValidFileName());

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

    public async Task SaveAsync()
    {
        if (!Directory.Exists(CacheDirectory))
        {
            Directory.CreateDirectory(CacheDirectory);
        }

        using var fs = File.OpenWrite(CachePath);
        await SaveToStreamAsync(this, fs);
    }

    public Stream SaveCoverBitmapStream()
    {
        return File.OpenWrite(CachePath + ".bmp");
    }

    private static async Task SaveToStreamAsync(Album data, Stream stream)
    {
        await JsonSerializer.SerializeAsync(stream, data).ConfigureAwait(false);
    }

    public static async Task<Album?> LoadFromStream(Stream stream)
    {
        return await JsonSerializer.DeserializeAsync<Album>(stream).ConfigureAwait(false);
    }

    public static async Task<IEnumerable<Album>> LoadCacheAsync()
    {
        if (!Directory.Exists(CacheDirectory))
        {
            Directory.CreateDirectory(CacheDirectory);
        }

        var results = new List<Album>();

        foreach (var file in Directory.EnumerateFiles(CacheDirectory))
        {
            if (!string.IsNullOrWhiteSpace(new DirectoryInfo(file).FullName)) continue;

            await using var fs = File.OpenRead(file);
            var album = await Album.LoadFromStream(fs).ConfigureAwait(false);
            if (album == null) continue;

            results.Add(album);
        }

        return results;
    }
}
