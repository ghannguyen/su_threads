using System.Text.RegularExpressions;

namespace Archive.Web.Extensions;

public static class SlugHelper
{
    public static string ToSlug(string value)
    {
        var normalized = value.Trim().ToLowerInvariant();
        normalized = Regex.Replace(normalized, @"[^a-z0-9\s-]", string.Empty);
        normalized = Regex.Replace(normalized, @"\s+", "-");
        normalized = Regex.Replace(normalized, @"-+", "-");
        return normalized.Trim('-');
    }

    public static List<string> ExtractHashtags(string content)
    {
        return Regex.Matches(content, @"#([a-zA-Z0-9_]+)")
            .Select(x => x.Groups[1].Value.ToLowerInvariant())
            .Distinct()
            .ToList();
    }
}
