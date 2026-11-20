namespace SunamoMsSqlLegacy.Extensions;

/// <summary>
/// Extensions for <see cref="NameValueCollection"/>.
/// </summary>
public static class NameValueCollectionExtensions
{
    /// <summary>
    /// Returns the value of the key, converting written whitespace sequences (\r\n, \n, \r, \t) to real characters.
    /// </summary>
    /// <param name="nvc">Collection from which the value is read.</param>
    /// <param name="key">Key of the value.</param>
    public static string GetUrlDecode(this NameValueCollection nvc, string key)
    {
        var value = nvc.Get(key);
        if (value != null)
        {
            switch (value)
            {
                case "\\r\\n":
                case "\\n":
                case "\\r":
                    return "\n";
                case "\\t":
                    return "\t";
            }
        }
        return value;
    }
}