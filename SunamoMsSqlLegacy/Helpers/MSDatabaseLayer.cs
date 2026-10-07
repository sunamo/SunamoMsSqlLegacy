namespace SunamoMsSqlLegacy.Helpers;

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
        int columnCount = sloupce.Length;
        return GetValuesDirect(columnCount);
    }

    /// <summary>
    /// Vrátí seznam parametrů (@p0, @p1...) pro daný počet.
    /// </summary>
    public static string GetValuesDirect(int count)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(AllStrings.lb);
        for (int index = 0; index < count; index++)
        {
            stringBuilder.Append("@p" + (index).ToString() + AllStrings.comma);
        }

        return stringBuilder.ToString().TrimEnd(AllChars.comma) + AllStrings.rb;
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
    public static bool LoadNewConnection(string connectionString)
    {
        MSDatabaseLayer.cs = connectionString;
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