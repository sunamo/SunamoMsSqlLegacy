namespace SunamoMsSqlLegacy;



/// <summary>
/// Statické připojení a pomocné metody pro MS SQL Server.
/// </summary>
public class MSDatabaseLayer : IDatabaseLayer<SqlDbType2>
{
    public static MSDatabaseLayer ci = new MSDatabaseLayer();
    public static SqlConnection conn = null;
    public static string cs = null;

    /// <summary>
    /// Vrátí seznam parametrů (@p0, @p1...) podle počtu sloupců.
    /// </summary>
    public static string GetValues(params object[] sloupce)
    {
        int to = sloupce.Length;
        return GetValuesDirect(to);
    }

    /// <summary>
    /// Vrátí seznam parametrů (@p0, @p1...) pro daný počet.
    /// </summary>
    public static string GetValuesDirect(int to)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(AllStrings.lb);
        for (int i = 0; i < to; i++)
        {
            sb.Append("@p" + (i).ToString() + AllStrings.comma);
        }

        return sb.ToString().TrimEnd(AllChars.comma) + AllStrings.rb;
    }

    /// <summary>
    /// Sestaví a uloží connection string z názvu serveru a databáze.
    /// </summary>
    public static bool LoadNewConnection(string dataSource, string database)
    {
        cs = "Data Source=" + dataSource;
        if (!string.IsNullOrEmpty(database))
        {
            cs += ";Database=" + database;
        }

        cs += ";" + "Integrated Security=True;MultipleActiveResultSets=True" +
              ";TransparentNetworkIPResolution=False;Max Pool Size=50000;Pooling=True;";
        return LoadNewConnection(cs);
    }

    /// <summary>
    /// Uloží connection string.
    /// </summary>
    public static bool LoadNewConnection(string cs)
    {
        MSDatabaseLayer.cs = cs;
        return true;
    }
    /// <summary>
    /// Nastaví továrnu sloupců pro MS SQL.
    /// </summary>
    public static void SetFactoryColumnDb()
    {
        ColumnsDBBase<MSSloupecDB, SqlDbType2>.factoryColumnDB = MSFactoryColumnDB.Instance;
    }
}
