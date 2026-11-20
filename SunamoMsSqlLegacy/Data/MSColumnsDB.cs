namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// List of table columns with generation of the CREATE TABLE command.
/// </summary>
public class MSColumnsDB : List<MSColumnDB>
{
    private static string _tableNameField = "_tableName";
    private string derived;
    /// <summary>
    /// The dict.
    /// </summary>
    public Dictionary<string, MSColumnDB> dict = new Dictionary<string, MSColumnDB>();
    private string replaceMSinMSStoredProceduresI;
    private bool signed;

    /// <summary>
    /// Creates a list of columns with the name of the derived table.
    /// </summary>
    public MSColumnsDB(string derived, bool signed, params MSColumnDB[] columnDefinitions) : this(signed, derived, null, columnDefinitions)
    {
    }

    /// <summary>
    /// Creates a list of columns with full settings.
    /// </summary>
    public MSColumnsDB(bool signed, string derived, string replaceMSinMSStoredProceduresI, params MSColumnDB[] columnDefinitions)
    {
        this.signed = signed;
        this.derived = derived;
        this.replaceMSinMSStoredProceduresI = replaceMSinMSStoredProceduresI;
        AddRange(columnDefinitions);
    }

    /// <summary>
    /// Creates a list of columns with the signed flag.
    /// </summary>
    public MSColumnsDB(bool signed, params MSColumnDB[] columnDefinitions) : this(signed, null, null, columnDefinitions)
    {
    }

    /// <summary>
    /// Creates a list of columns.
    /// </summary>
    public MSColumnsDB(params MSColumnDB[] columnDefinitions) : this(false, null, null, columnDefinitions)
    {
    }

    /// <summary>
    /// Returns the CREATE TABLE command; opens the connection from the connection string.
    /// </summary>
    public SqlCommand GetSqlCreateTable(string table, bool dynamicTables, string connectionString)
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var comm = GetSqlCreateTable(table, dynamicTables, conn);
            conn.Close();
            return comm;
        }
    }

    /// <summary>
    /// Returns the CREATE TABLE command for a static table.
    /// </summary>
    public SqlCommand GetSqlCreateTable(string tableName, string connectionString)
    {
        return GetSqlCreateTable(tableName, false, connectionString);
    }

    /// <summary>
    /// Returns the CREATE TABLE command over an existing connection.
    /// </summary>
    public SqlCommand GetSqlCreateTable(string table, bool dynamicTables, SqlConnection conn)
    {
        var sql = GeneratorMsSql.CreateTable(table, this, dynamicTables, conn);
        var comm = new SqlCommand(sql, conn);
        return comm;
    }
}