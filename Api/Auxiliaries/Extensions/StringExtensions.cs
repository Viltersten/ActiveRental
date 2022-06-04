namespace Api.Auxiliaries.Extensions;

static class StringExtensions
{
    internal static Guid ToGuid(this string self, Guid fallback = default)
        => Guid.TryParse(self, out Guid result)
            ? result
            : fallback;

    internal static bool Is(this string self, string pattern)
        => Regex.IsMatch(self, pattern);
}