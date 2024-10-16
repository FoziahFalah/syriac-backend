
using System.Text.RegularExpressions;

namespace SyriacSources.Backend.Application.Common.Extensions;
public static class StringExtensions
{
    public static string NormalizeString(this string name)
    {
        return Regex.Replace(name, @"\s+", "_").ToUpper();
    }
}
