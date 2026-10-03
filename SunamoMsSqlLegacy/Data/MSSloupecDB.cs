namespace SunamoMsSqlLegacy;

/// <summary>
/// Definice sloupce tabulky v MS SQL Serveru.
/// </summary>
public class MSSloupecDB : ColumnsDBBase<MSSloupecDB, SqlDbType2>
{
    /// <summary>
    /// Vytvoří sloupec a nastaví databázovou vrstvu.
    /// </summary>
    public MSSloupecDB() : base()
    {
        databaseLayer = MSDatabaseLayer.ci;
    }

    /// <summary>
    /// Nastaví výchozí databázovou vrstvu.
    /// </summary>
    static MSSloupecDB()
    {
        ColumnsDBBase<MSSloupecDB, SqlDbType2>.databaseLayer = MSDatabaseLayer.ci;
    }
}