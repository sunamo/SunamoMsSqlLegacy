namespace SunamoMsSqlLegacy.Helpers;

/// <summary>
/// SQL command generator for MS SQL Server.
/// </summary>
public class GeneratorMsSql
{
    /// <summary>
    /// Returns the definition of one column for CREATE TABLE.
    /// </summary>
    private static string Column(MSColumnDB var, string inTable, bool dynamicTables)
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
    /// Builds a WHERE clause joined with AND and shifts the parameter index.
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
    /// Returns SQL CREATE TABLE, or null if the table already exists.
    /// </summary>
    public static string CreateTable(string table, object columns2, bool dynamicTables, SqlConnection conn)
    {
        var columns = (MSColumnsDB)columns2;
        StringBuilder stringBuilder = new StringBuilder();
        bool exists = MSStoredProceduresI.ci.SelectExistsTable(table, conn);
        if (!exists)
        {
            stringBuilder.AppendFormat("CREATE TABLE {0}(", table);
            foreach (MSColumnDB var in columns)
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
    /// Builds a WHERE clause starting from parameter @p0.
    /// </summary>
    public static string CombinedWhere(ABC where)
    {
        int addFrom = 0;
        return CombinedWhere(where, ref addFrom);
    }
}