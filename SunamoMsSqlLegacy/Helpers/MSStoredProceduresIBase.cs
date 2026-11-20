namespace SunamoMsSqlLegacy.Helpers;

/// <summary>
/// Basic operations over MS SQL Server (select, insert, execute).
/// </summary>
public class MSStoredProceduresIBase
{
    public string _cs = null;

    /// <summary>
    /// Connection string of the instance, otherwise the default from MSDatabaseLayer.
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
    /// Returns the last N rows sorted descending by a column.
    /// </summary>
    public DataTable SelectDataTableLastRows(string tableName, int limit, string columns, string columnOrder, params AB[] where)
    {
        var abc = new ABC(where);
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.CombinedWhere(abc) + " ORDER BY " + columnOrder + " DESC");
        AddCommandParameteres(comm, 0, abc);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Adds parameters from the conditions to the command and returns the next index.
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
    /// Returns the last N rows with all columns.
    /// </summary>
    public DataTable SelectDataTableLastRows(string tableName, int limit, string colunmID, params AB[] abc)
    {
        return SelectDataTableLastRows(tableName, limit, "*", colunmID, abc);
    }

    /// <summary>
    /// Inserts a row with values in the given order.
    /// </summary>
    public void Insert4(string table, params object[] columns)
    {
        string values = MSDatabaseLayer.GetValues(columns);
        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", table, values));
        int columnCount = columns.Length;
        for (int index = 0; index < columnCount; index++)
        {
            object column = columns[index];
            AddCommandParameter(comm, index, column);
        }
        ExecuteNonQuery(comm);
    }

    /// <summary>
    /// Returns the number of table rows.
    /// </summary>
    public long SelectCount(string table)
    {
        return Convert.ToInt64(ExecuteScalar("SELECT COUNT(*) FROM " + table));
    }

    /// <summary>
    /// Executes a scalar query with parameters.
    /// </summary>
    public object ExecuteScalar(string commText, params object[] para)
    {
        SqlCommand comm = new SqlCommand(commText);
        for (int index = 0; index < para.Length; index++)
        {
            AddCommandParameter(comm, index, para[index]);
        }
        var result = ExecuteScalar(comm);
        return result;
    }

    /// <summary>
    /// Executes a scalar query and returns a short; for null returns the boundary value.
    /// </summary>
    private short ExecuteScalarShort(bool signed, SqlCommand comm)
    {
        var result = ExecuteScalar(comm);
        if (result == null)
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
        return Convert.ToInt16(result);
    }

    /// <summary>
    /// Executes a scalar command on a new connection.
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
    /// Returns the column maximum plus 1, for an empty table short.MinValue.
    /// </summary>
    public short SelectMaxShortMinValue(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return short.MinValue;
        }
        var result = ExecuteScalarShort(true, new SqlCommand("SELECT MAX(" + column + ") FROM " + table));
        result++;
        return result;
    }

    /// <summary>
    /// Determines whether the table exists (on the given connection).
    /// </summary>
    public bool SelectExistsTable(string tableName, SqlConnection conn)
    {
        DataTable dataTable = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", tableName));
        return dataTable.Rows.Count != 0;
    }

    /// <summary>
    /// Drops the table if it exists.
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
    /// Executes a command without a return value and returns the number of changed rows.
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
    /// Determines whether the table exists.
    /// </summary>
    public bool SelectExistsTable(string tableName)
    {
        using (var conn = new SqlConnection(Cs))
        {
            DataTable dataTable = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", tableName));
            conn.Close();
            return dataTable.Rows.Count != 0;
        }
    }

    /// <summary>
    /// Executes a query with parameters on the given connection.
    /// </summary>
    private DataTable SelectDataTable(SqlConnection conn, string sql, params object[] _params)
    {
        SqlCommand comm = new SqlCommand(sql);
        for (int index = 0; index < _params.Length; index++)
        {
            AddCommandParameter(comm, index, _params[index]);
        }
        return SelectDataTable(conn, comm);
    }

    /// <summary>
    /// Returns the selected columns with conditions.
    /// </summary>
    public DataTable SelectDataTableSelective(string table, string columnNames, params AB[] conditions)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", columnNames, table) + GeneratorMsSql.CombinedWhere(new ABC( conditions)));
        AddCommandParameterFromAbc(comm, conditions);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Executes a command on a new connection and returns the result.
    /// </summary>
    public DataTable SelectDataTable(SqlCommand comm)
    {
        using (var conn = new SqlConnection(Cs))
        {
            conn.Open();
            DataTable dataTable = new DataTable();
            comm.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(dataTable);
            conn.Close();
            return dataTable;
        }
    }

    /// <summary>
    /// Adds parameters from an array of pairs.
    /// </summary>
    private static void AddCommandParameterFromAbc(SqlCommand comm, params AB[] where)
    {
        for (int index = 0; index < where.Length; index++)
        {
            AddCommandParameter(comm, index, where[index].B);
        }
    }

    /// <summary>
    /// Adds one parameter (null as DBNull) and returns the next index.
    /// </summary>
    public static int AddCommandParameter(SqlCommand comm, int index, object value)
    {
        if (value == null || value.GetType() == DBNull.Value.GetType())
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@p" + index.ToString();
            parameter.Value = DBNull.Value;
            comm.Parameters.Add(parameter);
        }
        else if (value.GetType() == typeof(byte[]))
        {
            SqlParameter param = comm.Parameters.Add("@p" + index.ToString(), SqlDbType.Binary);
            param.Value = value;
        }
        else if (value.GetType() == Types.tString || value.GetType() == Types.tChar)
        {
            string _ = value.ToString();
            comm.Parameters.AddWithValue("@p" + index.ToString(), _);
        }
        else
        {
            comm.Parameters.AddWithValue("@p" + index.ToString(), value);
        }
        ++index;
        return index;
    }

    /// <summary>
    /// Adds parameters from the conditions starting at the given index.
    /// </summary>
    private static int AddCommandParameterFromAbc(SqlCommand comm, ABC where, int index)
    {
        if (where != null)
        {
            for (var innerIndex = 0; innerIndex < where.Count; innerIndex++)
            {
                AddCommandParameter(comm, index, where[innerIndex].B);
                index++;
            }
        }
        return index;
    }

    /// <summary>
    /// Executes a command on the given connection and returns the result.
    /// </summary>
    public DataTable SelectDataTable(SqlConnection conn, SqlCommand comm)
    {
        DataTable dataTable = new DataTable();
        comm.Connection = conn;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dataTable);
        return dataTable;
    }
}