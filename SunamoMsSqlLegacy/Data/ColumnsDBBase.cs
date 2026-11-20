namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// Common base of a database table column definition.
/// </summary>
public class ColumnsDBBase<MSSloupecDB, SqlDbType2>
{
    /// <summary>
    /// Sets the column factory for MS SQL.
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
    /// Column type converted to SqlDbType.
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
    /// Length of the type including parentheses, e.g. (50).
    /// </summary>
    public string Length
    {
        get
        {
            return length;
        }
    }

    /// <summary>
    /// Column name; an optional length in parentheses is separated into Length.
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
    /// Whether the column is a primary key.
    /// </summary>
    public bool PrimaryKey
    {
        get
        {
            return primaryKey;
        }
    }
    /// <summary>
    /// Creates a column with the primary key specified.
    /// </summary>
    public static MSSloupecDB CI(SqlDbType2 typ, string nazev, bool primaryKey)
    {
        return factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, false, false, null, null, primaryKey);
    }
    /// <summary>
    /// Creates a column without a primary key.
    /// </summary>
    public static MSSloupecDB CI(SqlDbType2 typ, string nazev)
    {
        return factoryColumnDB.CreateInstance(typ, nazev, Signed.Other, false, false, null, null, false);
    }
}