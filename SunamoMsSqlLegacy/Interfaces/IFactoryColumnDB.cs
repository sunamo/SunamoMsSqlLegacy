namespace SunamoMsSqlLegacy.Interfaces;

/// <summary>
/// Factory for column definitions.
/// </summary>
public interface IFactoryColumnDB<MSColumnDB, SqlDbType2>
{
    /// <summary>
    /// Creates a column definition.
    /// </summary>
    MSColumnDB CreateInstance(SqlDbType2 type, string name, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey);
}