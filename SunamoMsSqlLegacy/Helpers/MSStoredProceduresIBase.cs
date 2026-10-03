namespace SunamoMsSqlLegacy.Helpers;

/// <summary>
/// Základní operace nad MS SQL Serverem (select, insert, execute).
/// </summary>
public class MSStoredProceduresIBase
{
    public string _cs = null;

    /// <summary>
    /// Connection string instance, jinak výchozí z MSDatabaseLayer.
    /// </summary>
    private string Cs
    {
        get
        {
            if (_cs != null)
            {
                return _cs;
            }
            return MSDatabaseLayer.cs;
        }
    }

    /// <summary>
    /// Vrátí posledních N řádků seřazených sestupně podle sloupce.
    /// </summary>
    public DataTable SelectDataTableLastRows(string tableName, int limit, string columns, string columnOrder, params AB[] where)
    {
        var abc = new ABC(where);
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.CombinedWhere(abc) + " ORDER BY " + columnOrder + " DESC");
        AddCommandParameteres(comm, 0, abc);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Přidá do příkazu parametry z podmínek a vrátí další index.
    /// </summary>
    public static int AddCommandParameteres(SqlCommand comm, int startIndex, ABC aWhere)
    {
        foreach (var item in aWhere)
        {
            AddCommandParameter(comm, startIndex, item.B);
            startIndex++;
        }
        return startIndex;
    }

    /// <summary>
    /// Vrátí posledních N řádků se všemi sloupci.
    /// </summary>
    public DataTable SelectDataTableLastRows(string tableName, int limit, string colunmID, params AB[] abc)
    {
        return SelectDataTableLastRows(tableName, limit, "*", colunmID, abc);
    }

    /// <summary>
    /// Vloží řádek s hodnotami v daném pořadí.
    /// </summary>
    public void Insert4(string table, params object[] columns)
    {
        string values = MSDatabaseLayer.GetValues(columns);
        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", table, values));
        int to = columns.Length;
        for (int i = 0; i < to; i++)
        {
            object o = columns[i];
            AddCommandParameter(comm, i, o);
        }
        ExecuteNonQuery(comm);
    }

    /// <summary>
    /// Vrátí počet řádků tabulky.
    /// </summary>
    public long SelectCount(string table)
    {
        return Convert.ToInt64(ExecuteScalar("SELECT COUNT(*) FROM " + table));
    }

    /// <summary>
    /// Provede skalární dotaz s parametry.
    /// </summary>
    public object ExecuteScalar(string commText, params object[] para)
    {
        SqlCommand comm = new SqlCommand(commText);
        for (int i = 0; i < para.Length; i++)
        {
            AddCommandParameter(comm, i, para[i]);
        }
        var result = ExecuteScalar(comm);
        return result;
    }

    /// <summary>
    /// Provede skalární dotaz a vrátí short; u null vrátí krajní hodnotu.
    /// </summary>
    private short ExecuteScalarShort(bool signed, SqlCommand comm)
    {
        var o = ExecuteScalar(comm);
        if (o == null)
        {
            if (signed)
            {
                return short.MaxValue;
            }
            else
            {
                return -1;
            }
        }
        return Convert.ToInt16(o);
    }

    /// <summary>
    /// Provede skalární příkaz na novém připojení.
    /// </summary>
    public object ExecuteScalar(SqlCommand comm)
    {
        using (var conn = new SqlConnection(Cs))
        {
            conn.Open();
            comm.Connection = conn;
            var result = comm.ExecuteScalar();
            conn.Close();
            return result;
        }
    }

    /// <summary>
    /// Vrátí maximum sloupce plus 1, u prázdné tabulky short.MinValue.
    /// </summary>
    public short SelectMaxShortMinValue(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return short.MinValue;
        }
        var rs = ExecuteScalarShort(true, new SqlCommand("SELECT MAX(" + column + ") FROM " + table));
        rs++;
        return rs;
    }

    /// <summary>
    /// Zjistí, zda tabulka existuje (na daném připojení).
    /// </summary>
    public bool SelectExistsTable(string p, SqlConnection conn)
    {
        DataTable dt = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
        return dt.Rows.Count != 0;
    }

    /// <summary>
    /// Smaže tabulku, pokud existuje.
    /// </summary>
    public int DropTableIfExists(string table)
    {
        if (SelectExistsTable(table))
        {
            return ExecuteNonQuery(new SqlCommand("DROP TABLE " + table));
        }
        return 0;
    }

    /// <summary>
    /// Provede příkaz bez návratové hodnoty a vrátí počet změněných řádků.
    /// </summary>
    public int ExecuteNonQuery(SqlCommand comm)
    {
        using (SqlConnection conn = new SqlConnection(Cs))
        {
            conn.Open();
            comm.Connection = conn;
            var result = comm.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }

    /// <summary>
    /// Zjistí, zda tabulka existuje.
    /// </summary>
    public bool SelectExistsTable(string p)
    {
        using (var conn = new SqlConnection(Cs))
        {
            DataTable dt = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
            conn.Close();
            return dt.Rows.Count != 0;
        }
    }

    /// <summary>
    /// Provede dotaz s parametry na daném připojení.
    /// </summary>
    private DataTable SelectDataTable(SqlConnection conn, string sql, params object[] _params)
    {
        SqlCommand comm = new SqlCommand(sql);
        for (int i = 0; i < _params.Length; i++)
        {
            AddCommandParameter(comm, i, _params[i]);
        }
        return SelectDataTable(conn, comm);
    }

    /// <summary>
    /// Vrátí vybrané sloupce s podmínkami.
    /// </summary>
    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", nazvySloupcu, tabulka) + GeneratorMsSql.CombinedWhere(new ABC( ab)));
        AddCommandParameterFromAbc(comm, ab);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Provede příkaz na novém připojení a vrátí výsledek.
    /// </summary>
    public DataTable SelectDataTable(SqlCommand comm)
    {
        using (var conn = new SqlConnection(Cs))
        {
            conn.Open();
            DataTable dt = new DataTable();
            comm.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
    }

    /// <summary>
    /// Přidá parametry z pole dvojic.
    /// </summary>
    private static void AddCommandParameterFromAbc(SqlCommand comm, params AB[] where)
    {
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }
    }

    /// <summary>
    /// Přidá jeden parametr (null jako DBNull) a vrátí další index.
    /// </summary>
    public static int AddCommandParameter(SqlCommand comm, int i, object o)
    {
        if (o == null || o.GetType() == DBNull.Value.GetType())
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = "@p" + i.ToString();
            p.Value = DBNull.Value;
            comm.Parameters.Add(p);
        }
        else if (o.GetType() == typeof(byte[]))
        {
            SqlParameter param = comm.Parameters.Add("@p" + i.ToString(), SqlDbType.Binary);
            param.Value = o;
        }
        else if (o.GetType() == Types.tString || o.GetType() == Types.tChar)
        {
            string _ = o.ToString();
            comm.Parameters.AddWithValue("@p" + i.ToString(), _);
        }
        else
        {
            comm.Parameters.AddWithValue("@p" + i.ToString(), o);
        }
        ++i;
        return i;
    }

    /// <summary>
    /// Přidá parametry z podmínek od daného indexu.
    /// </summary>
    private static int AddCommandParameterFromAbc(SqlCommand comm, ABC where, int i)
    {
        if (where != null)
        {
            for (var i2 = 0; i2 < where.Count; i2++)
            {
                AddCommandParameter(comm, i, where[i2].B);
                i++;
            }
        }
        return i;
    }

    /// <summary>
    /// Provede příkaz na daném připojení a vrátí výsledek.
    /// </summary>
    public DataTable SelectDataTable(SqlConnection conn, SqlCommand comm)
    {
        DataTable dt = new DataTable();
        comm.Connection = conn;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dt);
        return dt;
    }
}