namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// List of AB pairs (name and value).
/// </summary>
public class ABC : List<AB>
{
    /// <summary>
    /// Creates an empty list.
    /// </summary>
    public ABC()
    {
    }

    /// <summary>
    /// Creates a list from the sequence name, value, name, value.
    /// </summary>
    public ABC(params object[] setsNameValue)
    {
        for (int index = 0; index < setsNameValue.Length; index++)
        {
            this.Add(AB.Get(setsNameValue[index].ToString(), setsNameValue[++index]));
        }
    }

    /// <summary>
    /// Creates a list from ready-made pairs.
    /// </summary>
    public ABC(params AB[] abc)
    {
        this.AddRange(abc);
    }

    /// <summary>
    /// Returns only the values as an array.
    /// </summary>
    public object[] OnlyBs()
    {
        return OnlyBsList().ToArray();
    }

    /// <summary>
    /// Returns only the values as a list.
    /// </summary>
    public List<object> OnlyBsList()
    {
        List<object> values = new List<object>(this.Count);
        for (int index = 0; index < this.Count; index++)
        {
            values.Add(this[index].B);
        }
        return values;
    }

    /// <summary>
    /// Returns only the names as a list.
    /// </summary>
    public List<string> OnlyAs()
    {
        List<string> names = new List<string>(this.Count);

        for (int index = 0; index < this.Count; index++)
        {
            names.Add(this[index].A);
        }
        return names;
    }

    /// <summary>
    /// Returns the values from a list of pairs.
    /// </summary>
    public static IEnumerable OnlyBs(List<AB> arr)
    {
        return arr.Select(pair => pair.B);
    }
}