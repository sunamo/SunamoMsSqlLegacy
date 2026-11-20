namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// Common base of a database table column definition.
/// </summary>
public class ColumnsDBBase<MSColumnDB, SqlDbType2>
{
    /// <summary>
    /// Sets the column factory for MS SQL.
    /// </summary>
    static ColumnsDBBase()
    {
        MSDatabaseLayer.SetFactoryColumnDb();
    }

    /// <summary>
    /// The factory column DB.
    /// </summary>
    public static IFactoryColumnDB<MSColumnDB, SqlDbType2> factoryColumnDB = null;
    /// <summary>
    /// The type.
    /// </summary>
    public SqlDbType2 type = default(SqlDbType2);
    /// <summary>
    /// The is unicode.
    /// </summary>
    public bool IsUnicode = false;
    private string _name = "";
    /// <summary>
    /// The database layer.
    /// </summary>
    public static IDatabaseLayer<SqlDbType2> databaseLayer = null;
    private string length = "";
    /// <summary>
    /// The primary key.
    /// </summary>
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
    /// Column type.
    /// </summary>
    public SqlDbType2 Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
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
    public static MSColumnDB CI(SqlDbType2 type, string name, bool primaryKey)
    {
        return factoryColumnDB.CreateInstance(type, name, Signed.Other, false, false, null, null, primaryKey);
    }
    /// <summary>
    /// Creates a column without a primary key.
    /// </summary>
    public static MSColumnDB CI(SqlDbType2 type, string name)
    {
        return factoryColumnDB.CreateInstance(type, name, Signed.Other, false, false, null, null, false);
    }
}