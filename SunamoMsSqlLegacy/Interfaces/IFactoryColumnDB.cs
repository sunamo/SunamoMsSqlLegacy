namespace SunamoMsSqlLegacy.Interfaces;

/// <summary>
/// Factory for column definitions.
/// </summary>
public interface IFactoryColumnDB<MSSloupecDB, SqlDbType2>
{
    /// <summary>
    /// Creates a column definition.
    /// </summary>
    MSSloupecDB CreateInstance(SqlDbType2 typ, string nazev, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey);
}