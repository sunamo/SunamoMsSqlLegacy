namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// Name and value pair, used as a condition or parameter of an SQL query.
/// </summary>
public class AB
{
    public string A = null;
    public object B = null;

    /// <summary>
    /// Creates a pair of name and value.
    /// </summary>
    public AB(string name, object value)
    {
        A = name;
        B = value;
    }

    /// <summary>
    /// Creates a pair where the name is the full type name.
    /// </summary>
    public static AB Get(Type type, object value)
    {
        return new AB(type.FullName, value);
    }

    /// <summary>
    /// Creates a pair of name and value.
    /// </summary>
    public static AB Get(string name, object value)
    {
        return new AB(name, value);
    }
}