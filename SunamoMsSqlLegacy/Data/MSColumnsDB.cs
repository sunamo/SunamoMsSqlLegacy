namespace SunamoMsSqlLegacy.Data;

/// <summary>
/// Seznam sloupců tabulky s generováním příkazu CREATE TABLE.
/// </summary>
public class MSColumnsDB : List<MSSloupecDB>
{
    private static string _tableNameField = "_tableName";
    private string derived;
    public Dictionary<string, MSSloupecDB> dict = new Dictionary<string, MSSloupecDB>();
    private string replaceMSinMSStoredProceduresI;
    private bool signed;

    /// <summary>
    /// Vytvoří seznam sloupců s názvem odvozené tabulky.
    /// </summary>
    public MSColumnsDB(string derived, bool signed, params MSSloupecDB[] columnDefinitions) : this(signed, derived, null, columnDefinitions)
    {
    }

    /// <summary>
    /// Vytvoří seznam sloupců s plným nastavením.
    /// </summary>
    public MSColumnsDB(bool signed, string derived, string replaceMSinMSStoredProceduresI, params MSSloupecDB[] columnDefinitions)
    {
        this.signed = signed;
        this.derived = derived;
        this.replaceMSinMSStoredProceduresI = replaceMSinMSStoredProceduresI;
        AddRange(columnDefinitions);
    }

    /// <summary>
    /// Vytvoří seznam sloupců s příznakem signed.
    /// </summary>
    public MSColumnsDB(bool signed, params MSSloupecDB[] columnDefinitions) : this(signed, null, null, columnDefinitions)
    {
    }

    /// <summary>
    /// Vytvoří seznam sloupců.
    /// </summary>
    public MSColumnsDB(params MSSloupecDB[] columnDefinitions) : this(false, null, null, columnDefinitions)
    {
    }

    /// <summary>
    /// Vrátí příkaz CREATE TABLE; připojení otevře z connection stringu.
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
    /// Vrátí příkaz CREATE TABLE pro statickou tabulku.
    /// </summary>
    public SqlCommand GetSqlCreateTable(string nazevTabulky, string connectionString)
    {
        return GetSqlCreateTable(nazevTabulky, false, connectionString);
    }

    /// <summary>
    /// Vrátí příkaz CREATE TABLE nad existujícím připojením.
    /// </summary>
    public SqlCommand GetSqlCreateTable(string table, bool dynamicTables, SqlConnection conn)
    {
        var sql = GeneratorMsSql.CreateTable(table, this, dynamicTables, conn);
        var comm = new SqlCommand(sql, conn);
        return comm;
    }
}