namespace SunamoMsSqlLegacy.Helpers;

/// <summary>
/// Factory for MS SQL Server columns.
/// </summary>
public class MSFactoryColumnDB : IFactoryColumnDB<MSColumnDB, SqlDbType2>
{
    /// <summary>
    /// The instance.
    /// </summary>
    public static IFactoryColumnDB<MSColumnDB, SqlDbType2> Instance = new MSFactoryColumnDB();

    /// <summary>
    /// Private constructor of the singleton.
    /// </summary>
    private MSFactoryColumnDB()
    {
    }

    /// <summary>
    /// Creates a column of the given type and name.
    /// </summary>
    public MSColumnDB CreateInstance(SqlDbType2 type, string name, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey)
    {
        MSColumnDB column = new MSColumnDB();
        bool isNewId = false;
        column.type = type;
        if (column.Type == SqlDbType2.NChar || column.Type == SqlDbType2.NVarChar)
        {
            column.IsUnicode = true;
        }
        else if (column.Type == SqlDbType2.Char || column.Type == SqlDbType2.VarChar)
        {
            column.IsUnicode = false;
        }
        column.Name = name;
        column.primaryKey = primaryKey;
        return column;
    }
}