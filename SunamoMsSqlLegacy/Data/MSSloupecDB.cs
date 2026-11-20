namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// Definice sloupce tabulky v MS SQL Serveru.
/// </summary>
public class MSSloupecDB : ColumnsDBBase<MSSloupecDB, SqlDbType2>
{
    /// <summary>
    /// Creates a column and sets up the database layer.
    /// </summary>
    public MSSloupecDB() : base()
    {
        databaseLayer = MSDatabaseLayer.ci;
    }

    /// <summary>
    /// Sets the default database layer.
    /// </summary>
    static MSSloupecDB()
    {
        ColumnsDBBase<MSSloupecDB, SqlDbType2>.databaseLayer = MSDatabaseLayer.ci;
    }
}