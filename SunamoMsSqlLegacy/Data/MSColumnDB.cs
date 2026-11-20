namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// Definition of a table column in MS SQL Server.
/// </summary>
public class MSColumnDB : ColumnsDBBase<MSColumnDB, SqlDbType2>
{
    /// <summary>
    /// Creates a column and sets up the database layer.
    /// </summary>
    public MSColumnDB() : base()
    {
        databaseLayer = MSDatabaseLayer.ci;
    }

    /// <summary>
    /// Sets the default database layer.
    /// </summary>
    static MSColumnDB()
    {
        ColumnsDBBase<MSColumnDB, SqlDbType2>.databaseLayer = MSDatabaseLayer.ci;
    }
}