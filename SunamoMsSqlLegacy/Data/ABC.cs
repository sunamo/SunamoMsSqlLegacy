namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// Seznam dvojic AB (název a hodnota).
/// </summary>
public class ABC : List<AB>
{
    /// <summary>
    /// Vytvoří prázdný seznam.
    /// </summary>
    public ABC()
    {
    }

    /// <summary>
    /// Vytvoří seznam z posloupnosti název, hodnota, název, hodnota.
    /// </summary>
    public ABC(params object[] setsNameValue)
    {
        for (int index = 0; index < setsNameValue.Length; index++)
        {
            this.Add(AB.Get(setsNameValue[index].ToString(), setsNameValue[++index]));
        }
    }

    /// <summary>
    /// Vytvoří seznam z hotových dvojic.
    /// </summary>
    public ABC(params AB[] abc)
    {
        this.AddRange(abc);
    }

    /// <summary>
    /// Vrátí jen hodnoty jako pole.
    /// </summary>
    public object[] OnlyBs()
    {
        return OnlyBsList().ToArray();
    }

    /// <summary>
    /// Vrátí jen hodnoty jako seznam.
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
    /// Vrátí jen názvy jako seznam.
    /// </summary>
    public List<string> OnlyAs()
    {
        List<string> names = new List<string>(this.Count);

        for (int index = 0; index < this.Count; index++)
        {
            //o[i] = this[i].A;
            names.Add(this[index].A);
        }
        return names;
    }

    /// <summary>
    /// Vrátí hodnoty ze seznamu dvojic.
    /// </summary>
    public static IEnumerable OnlyBs(List<AB> arr)
    {
        return arr.Select(pair => pair.B);
    }
}