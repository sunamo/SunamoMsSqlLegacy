namespace SunamoMsSqlLegacy;

/// <summary>
/// Továrna na definice sloupců.
/// </summary>
public interface IFactoryColumnDB<MSSloupecDB, SqlDbType2>
{
    /// <summary>
    /// Vytvoří definici sloupce.
    /// </summary>
    MSSloupecDB CreateInstance(SqlDbType2 typ, string nazev, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey);
}