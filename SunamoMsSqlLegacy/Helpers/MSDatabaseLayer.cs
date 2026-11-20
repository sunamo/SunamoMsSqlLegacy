namespace SunamoMsSqlLegacy.Helpers;

/// <summary>
/// Static connection and helper methods for MS SQL Server.
/// </summary>
public class MSDatabaseLayer : IDatabaseLayer<SqlDbType2>
{
    public static MSDatabaseLayer ci = new MSDatabaseLayer();
    public static SqlConnection conn = null;
    public static string cs = null;

    /// <summary>
    /// Returns a list of parameters (@p0, @p1...) according to the number of columns.
    /// </summary>
    public static string GetValues(params object[] columns)
    {
        int columnCount = columns.Length;
        return GetValuesDirect(columnCount);
    }

    /// <summary>
    /// Returns a list of parameters (@p0, @p1...) for the given count.
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
    /// Builds and saves a connection string from the server and database name.
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
    /// Saves the connection string.
    /// </summary>
    public static bool LoadNewConnection(string connectionString)
    {
        MSDatabaseLayer.cs = connectionString;
        return true;
    }
    /// <summary>
    /// Sets the column factory for MS SQL.
    /// </summary>
    public static void SetFactoryColumnDb()
    {
        ColumnsDBBase<MSColumnDB, SqlDbType2>.factoryColumnDB = MSFactoryColumnDB.Instance;
    }
}