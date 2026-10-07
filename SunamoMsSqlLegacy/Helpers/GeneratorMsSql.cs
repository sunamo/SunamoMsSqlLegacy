namespace SunamoMsSqlLegacy.Helpers;

/// <summary>
/// Generátor SQL příkazů pro MS SQL Server.
/// </summary>
public class GeneratorMsSql
{
    /// <summary>
    /// Vrátí definici jednoho sloupce pro CREATE TABLE.
    /// </summary>
    private static string Column(MSSloupecDB var, string inTable, bool dynamicTables)
    {
        InstantSB builder = new InstantSB(AllStrings.space);
        builder.AddItem(var.Name);
        builder.AddItem((var.Type + var.Length));
        var columnType = var.Type;
        if (
            columnType == SqlDbType2.VarChar ||
            columnType == SqlDbType2.Char ||
            columnType == SqlDbType2.NVarChar ||
            columnType == SqlDbType2.NChar
            )
        {
            builder.AddItem("COLLATE Czech_CS_AS_KS_WS");
        }
        return builder.ToString();
    }

    /// <summary>
    /// Sestaví klauzuli WHERE spojenou přes AND a posune index parametrů.
    /// </summary>
    public static string CombinedWhere(ABC where, ref int addFrom)
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (where != null)
        {
            if (where.Count > 0)
            {
                stringBuilder.Append(" " + "WHERE" + " ");
            }
            bool first = true;
            foreach (AB var in where)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    stringBuilder.Append(" AND ");
                }
                stringBuilder.Append(string.Format(" {0} = {1} ", var.A, "@p" + addFrom));
                addFrom++;
            }
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Vrátí SQL CREATE TABLE, nebo null pokud tabulka už existuje.
    /// </summary>
    public static string CreateTable(string table, object columns2, bool dynamicTables, SqlConnection conn)
    {
        var columns = (MSColumnsDB)columns2;
        StringBuilder stringBuilder = new StringBuilder();
        bool exists = MSStoredProceduresI.ci.SelectExistsTable(table, conn);
        if (!exists)
        {
            stringBuilder.AppendFormat("CREATE TABLE {0}(", table);
            foreach (MSSloupecDB var in columns)
            {
                stringBuilder.Append(GeneratorMsSql.Column(var, table, dynamicTables) + AllStrings.comma);
            }
            string trimmedSql = stringBuilder.ToString();
            trimmedSql = trimmedSql.TrimEnd(AllChars.comma);
            string createTableSql = trimmedSql + AllStrings.rb;
            return createTableSql;
        }
        return null;
    }

    /// <summary>
    /// Sestaví klauzuli WHERE od parametru @p0.
    /// </summary>
    public static string CombinedWhere(ABC where)
    {
        int addFrom = 0;
        return CombinedWhere(where, ref addFrom);
    }
}