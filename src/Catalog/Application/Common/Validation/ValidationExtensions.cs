namespace Catalog.Application.Common.Validation;

internal static class ValidationExtensions
{
    public static bool IsValidUrl(this string input) =>
        Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

    public static bool IsValidHtml(this string input)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(input);
        return !doc.ParseErrors.Any();
    }

    public static bool ContainsXssScript(this string input)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(input);
        return doc.DocumentNode.DescendantsAndSelf()
            .Any(n => n.NodeType == HtmlNodeType.Element &&
                      n.Attributes.Any(
                          a => a.Name == "on*"
                               || a.Name == "href" && a.Value.ToLower().StartsWith("javascript")));
    }
}
