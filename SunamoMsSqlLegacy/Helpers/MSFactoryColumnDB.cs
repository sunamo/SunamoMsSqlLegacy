namespace SunamoMsSqlLegacy.Helpers;

/// <summary>
/// Factory for MS SQL Server columns.
/// </summary>
public class MSFactoryColumnDB : IFactoryColumnDB<MSSloupecDB, SqlDbType2>
{
    public static IFactoryColumnDB<MSSloupecDB, SqlDbType2> Instance = new MSFactoryColumnDB();

    /// <summary>
    /// Private constructor of the singleton.
    /// </summary>
    private MSFactoryColumnDB()
    {
    }

    /// <summary>
    /// Creates a column of the given type and name.
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