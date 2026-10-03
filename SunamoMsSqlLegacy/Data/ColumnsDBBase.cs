namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// Společný základ definice sloupce databázové tabulky.
/// </summary>
public class ColumnsDBBase<MSSloupecDB, SqlDbType2>
{
    /// <summary>
    /// Nastaví továrnu sloupců pro MS SQL.
    /// </summary>
    static ColumnsDBBase()
    {
        MSDatabaseLayer.SetFactoryColumnDb();
    }

    public static IFactoryColumnDB<MSSloupecDB, SqlDbType2> factoryColumnDB = null;
    public SqlDbType2 typ = default(SqlDbType2);
    public bool IsUnicode = false;
    private string _name = "";
    public static IDatabaseLayer<SqlDbType2> databaseLayer = null;
    private string length = "";
    public bool primaryKey = false;

    /// <summary>
    /// Typ sloupce převedený na SqlDbType.
    /// </summary>
    public SqlDbType Type2
    {
        get
        {
            return (SqlDbType)Enum.Parse(typeof(SqlDbType), Type.ToString());
        }
    }

    /// <summary>
    /// Typ sloupce.
    /// </summary>
    public SqlDbType2 Type
    {
        get
        {
            return typ;
        }
        set
        {
            typ = value;
        }
    }
    /// <summary>
    /// Délka typu včetně závorek, např. (50).
    /// </summary>
    public string Length
    {
        get
        {
            return length;
        }
    }

    /// <summary>
    /// Název sloupce; případná délka v závorce se oddělí do Length.
    /// </summary>
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            int dex = value.IndexOf(AllChars.lb);
            if (dex != -1)
            {
                _name = value.Substring(0, dex);
                length = value.Substring(dex);
            }
            else
            {
                _name = value;
            }
        }
    }

    /// <summary>
    /// Zda je sloupec primární klíč.
    /// </summary>
    public bool PrimaryKey
    {
        get
        {
            return primaryKey;
        }
    }
    /// <summary>
    /// Vytvoří sloupec s určením primárního klíče.
    /// </summary>
    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool primaryKey)
    {
        return factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, false, false, null, null, primaryKey);
    }
    /// <summary>
    /// Vytvoří sloupec bez primárního klíče.
    /// </summary>
    public static MSSloupecDB CI(SqlDbType2 typ, string nazev)
    {
        return factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, false, false, null, null, false);
    }
}