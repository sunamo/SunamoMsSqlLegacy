namespace SunamoMsSqlLegacy;

/// <summary>
/// Továrna na sloupce MS SQL Serveru.
/// </summary>
public class MSFactoryColumnDB : IFactoryColumnDB<MSSloupecDB, SqlDbType2>
{
    public static IFactoryColumnDB<MSSloupecDB, SqlDbType2> Instance = new MSFactoryColumnDB();

    /// <summary>
    /// Soukromý konstruktor singletonu.
    /// </summary>
    private MSFactoryColumnDB()
    {
    }

    /// <summary>
    /// Vytvoří sloupec daného typu a názvu.
    /// </summary>
    public MSSloupecDB CreateInstance(SqlDbType2 typ, string nazev, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey)
    {
        MSSloupecDB column = new MSSloupecDB();
        bool isNewId = false;
        column.typ = typ;
        if (column.Type == SqlDbType2.NChar || column.Type == SqlDbType2.NVarChar)
        {
            column.IsUnicode = true;
        }
        else if (column.Type == SqlDbType2.Char || column.Type == SqlDbType2.VarChar)
        {
            column.IsUnicode = false;
        }
        column.Name = nazev;
        column.primaryKey = primaryKey;
        return column;
    }
}