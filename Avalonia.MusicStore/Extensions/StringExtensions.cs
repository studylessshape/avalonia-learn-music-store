using System.IO;
using System.Text;

namespace Avalonia.MusicStore.Extensions;

internal static class StringExtensions
{
    internal static string ValidFileName(this string filename)
    {
        var newStr = new StringBuilder(filename);
        foreach (var invalidChar in Path.GetInvalidFileNameChars())
        {
            newStr.Replace($"{invalidChar}", "");
        }

        return newStr.ToString();
    }
}
