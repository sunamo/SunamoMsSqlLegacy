namespace SunamoMsSqlLegacy;



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
        for (int i = 0; i < setsNameValue.Length; i++)
        {
            this.Add(AB.Get(setsNameValue[i].ToString(), setsNameValue[++i]));
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
        List<object> o = new List<object>(this.Count);
        for (int i = 0; i < this.Count; i++)
        {
            o.Add(this[i].B);
        }
        return o;
    }

    /// <summary>
    /// Vrátí jen názvy jako seznam.
    /// </summary>
    public List<string> OnlyAs()
    {
        List<string> o = new List<string>(this.Count);

        for (int i = 0; i < this.Count; i++)
        {
            //o[i] = this[i].A;
            o.Add(this[i].A);
        }
        return o;
    }

    /// <summary>
    /// Vrátí hodnoty ze seznamu dvojic.
    /// </summary>
    public static IEnumerable OnlyBs(List<AB> arr)
    {
        return arr.Select(d => d.B);
    }
}
