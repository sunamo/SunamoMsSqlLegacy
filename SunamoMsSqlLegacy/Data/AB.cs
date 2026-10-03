namespace SunamoMsSqlLegacy;

/// <summary>
/// Dvojice název a hodnota, používaná jako podmínka nebo parametr SQL dotazu.
/// </summary>
public class AB
{
    public string A = null;
    public object B = null;

    /// <summary>
    /// Vytvoří dvojici z názvu a hodnoty.
    /// </summary>
    public AB(string a, object b)
    {
        A = a;
        B = b;
    }

    /// <summary>
    /// Vytvoří dvojici, kde název je plný název typu.
    /// </summary>
    public static AB Get(Type a, object b)
    {
        return new AB(a.FullName, b);
    }

    /// <summary>
    /// Vytvoří dvojici z názvu a hodnoty.
    /// </summary>
    public static AB Get(string a, object b)
    {
        return new AB(a, b);
    }
}
