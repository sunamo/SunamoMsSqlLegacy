namespace SunamoMsSqlLegacy;

/// <summary>
/// Rozšíření pro <see cref="NameValueCollection"/>.
/// </summary>
public static class NameValueCollectionExtensions
{
    /// <summary>
    /// Vrátí hodnotu klíče, přičemž zapsané whitespace sekvence (\r\n, \n, \r, \t) převede na skutečné znaky.
    /// </summary>
    /// <param name="nvc">Kolekce, ze které se hodnota čte.</param>
    /// <param name="key">Klíč hodnoty.</param>
    public static string GetUrlDecode(this NameValueCollection nvc, string key)
    {
        var v = nvc.Get(key);
        if (v != null)
        {
            switch (v)
            {
                case "\\r\\n":
                case "\\n":
                case "\\r":
                    return "\n";
                case "\\t":
                    return "\t";
            }
        }
        return v;
    }
}
