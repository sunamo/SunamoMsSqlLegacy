namespace SunamoMsSqlLegacy.Data;

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
    public AB(string name, object value)
    {
        A = name;
        B = value;
    }

    /// <summary>
    /// Vytvoří dvojici, kde název je plný název typu.
    /// </summary>
    public static AB Get(Type type, object value)
    {
        return new AB(type.FullName, value);
    }

    /// <summary>
    /// Vytvoří dvojici z názvu a hodnoty.
    /// </summary>
    public static AB Get(string name, object value)
    {
        return new AB(name, value);
    }
}