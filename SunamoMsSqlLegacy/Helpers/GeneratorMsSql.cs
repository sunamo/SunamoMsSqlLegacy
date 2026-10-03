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
        InstantSB sb = new InstantSB(AllStrings.space);
        sb.AddItem(var.Name);
        sb.AddItem((var.Type + var.Length));
        var t = var.Type;
        if (
            t == SqlDbType2.VarChar ||
            t == SqlDbType2.Char ||
            t == SqlDbType2.NVarChar ||
            t == SqlDbType2.NChar
            )
        {
            sb.AddItem("COLLATE Czech_CS_AS_KS_WS");
        }
        return sb.ToString();
    }

    /// <summary>
    /// Sestaví klauzuli WHERE spojenou přes AND a posune index parametrů.
    /// </summary>
    public static string CombinedWhere(ABC where, ref int addFrom)
    {
        StringBuilder sb = new StringBuilder();
        if (where != null)
        {
            if (where.Count > 0)
            {
                sb.Append(" " + "WHERE" + " ");
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
                    sb.Append(" AND ");
                }
                sb.Append(string.Format(" {0} = {1} ", var.A, "@p" + addFrom));
                addFrom++;
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Vrátí SQL CREATE TABLE, nebo null pokud tabulka už existuje.
    /// </summary>
    public static string CreateTable(string table, object columns2, bool dynamicTables, SqlConnection conn)
    {
        var columns = (MSColumnsDB)columns2;
        StringBuilder sb = new StringBuilder();
        bool exists = MSStoredProceduresI.ci.SelectExistsTable(table, conn);
        if (!exists)
        {
            sb.AppendFormat("CREATE TABLE {0}(", table);
            foreach (MSSloupecDB var in columns)
            {
                sb.Append(GeneratorMsSql.Column(var, table, dynamicTables) + AllStrings.comma);
            }
            string dd = sb.ToString();
            dd = dd.TrimEnd(AllChars.comma);
            string vr = dd + AllStrings.rb;
            return vr;
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