using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Database wrapper class supports inline SQL or stored procedure calls
/// </summary>
public class db : IDisposable
{

    #region "public properties"


    public System.Collections.Generic.List<spParam> parmList = new System.Collections.Generic.List<spParam>();

    /// <summary>
    /// optional connection string.  if not provided, will use the default connection
    /// </summary>
    public string connectionString { get; set; }


    /// <summary>
    /// stored procedure name
    /// </summary>
    public string spName { get; set; }

    /// <summary>
    /// adds stored proc parameter
    /// </summary>
    /// <param name="parmName"></param>
    /// <param name="parmValue"></param>
    public void addParm(string parmName, object parmValue)
    {
        if (parmList == null)
        {
            parmList = new System.Collections.Generic.List<spParam>();
        }
        parmList.Add(new spParam(parmName, parmValue));
    }

    private Exception _ex;
    /// <summary>
    /// exceptions raised from call
    /// </summary>
    public Exception db_exception
    {
        get { return _ex; }

        set
        {
            _ex = value;
            ///log exception
            //Logger eh = new Logger(_ex);

            if (throwExceptions)
            { throw _ex; }
        }
    }

    private string _parmString;
    /// <summary>
    /// read only string of parameters passed in
    /// </summary>
    public string parmString { get { return _parmString; } }

    /// <summary>
    /// will throw any exceptions if true (default false)
    /// </summary>
    public bool throwExceptions { get; set; }

    /// <summary>
    /// The time in seconds to wait for the command to execute. The default is 30 seconds.
    /// </summary>
    public Int32 commandTimeout { get; set; }

    #endregion
    #region "Private Properties

    private SqlCommand sCmd { get; set; }

    private string ConnStringMAIN
    {
        get
        {
            System.Configuration.ConnectionStringSettings s = new System.Configuration.ConnectionStringSettings("main", config.ConnStringMAIN);
            return s.ToString();
        }
    }


    private string defaultConnection
    {
        get { return ConnStringMAIN; }
    }

    private SqlConnection SQL
    {
        get
        {
            if (connectionString == null)
            {//uses default connection string if none is set
                connectionString = "Data Source = registration.mssql.somee.com; initial catalog = registration; uid = roldo22_SQLLogin_1; password = 1ajk8vbohs";
            }
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }

    #endregion

    /// <summary>
    /// default construct, use db(SQLCommand) 
    /// </summary>
    public db()
    {
        init();
    }

    public db(string SQLCommand)
    {
        init();
        spName = SQLCommand;
    }

    /// <summary>
    /// "SQLCommand" can either be a sp name, function or inline sql 
    /// </summary>
    /// <param name="StoredProcedure"></param>


    #region "private functions"
    private void init()
    {   ///set defaults
        throwExceptions = false;
        _ex = null;
        parmList = new System.Collections.Generic.List<spParam>();
        commandTimeout = -1;
    }

    private void BuildInline()
    {
        SqlCommand oCmd = new SqlCommand();
        try
        {
            //Conn.DataSource + "." + Conn.Database + ".dbo." + 
            string cmdText = spName;
            oCmd = new SqlCommand();
            oCmd.Connection = SQL;
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = cmdText;
            if (commandTimeout > 0)
            {
                oCmd.CommandTimeout = commandTimeout;
            }
        }
        catch (Exception ex)
        {
            db_exception = ex;
        }
        sCmd = oCmd;

    }

    private void BuildSP()
    {
        SqlCommand oCmd = new SqlCommand();

        bool doSP = true;
        if (spName.Length > 14)
        {
            string spName_l = spName.ToLower();
            if (spName_l.StartsWith("select") || spName_l.StartsWith("update") || spName_l.StartsWith("insert") || spName_l.StartsWith("delete"))
            {
                doSP = false;
            }
        }
        if (doSP)
        {
            try
            {
                //Conn.DataSource + "." + Conn.Database + ".dbo." + 
                string cmdText = string.Concat(SQL.Database, ".dbo.", spName);
                oCmd = new SqlCommand();
                oCmd.Connection = SQL;
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = cmdText;
                if (commandTimeout > 0)
                {
                    oCmd.CommandTimeout = commandTimeout;
                }
            }
            catch (Exception ex)
            {
                db_exception = ex;
            }
            sCmd = oCmd;
        }
        else
        {
            BuildInline();
        }
    }


    private void BuildParms()
    {
        foreach (spParam p in parmList)
        {
            string Name = p.paramName;
            object Value = p.paramValue;

            if (sCmd != null)
            {
                sCmd.Parameters.AddWithValue(Name, Value);
                _parmString = string.Concat(_parmString, " ", Name, ": ", Value, " ");
            }
        }
    }

    private SqlParameter AddParm_Return()
    {
        SqlParameter newReqNumber = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
        if (sCmd != null)
        {
            sCmd.Parameters.Add(newReqNumber);
            newReqNumber.Direction = ParameterDirection.ReturnValue;
        }
        return newReqNumber;
    }

    #endregion

    #region "public functions"

    /// <summary>
    /// returns a datatable using the stored proc/inline sql and params passed
    /// </summary>
    public DataTable getDataTable
    {
        get
        {
            DataTable dtReturn = new DataTable();

            BuildSP();
            BuildParms();

            try
            {
                sCmd.Connection.Open();
                using (SqlDataReader dr = sCmd.ExecuteReader())
                {
                    dtReturn.Load(dr);
                }
            }
            catch (Exception ex)
            {
                db_exception = ex;
            }
            finally
            {
                CloseAll();
            }

            return dtReturn;
        }
    }

    public DataSet getDataSet
    {
        get
        {
            DataSet dsReturn = new DataSet();
            BuildSP();
            BuildParms();
            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sCmd))
                {
                    sda.Fill(dsReturn);
                }
            }
            catch (Exception ex)
            {
                db_exception = ex;
            }
            finally
            {
                CloseAll();
            }
            return dsReturn;

        }
    }

    /// <summary>
    /// runs ExecuteNonQuery on SQL Command and returns the return value(int)
    /// </summary>
    public int Exec()
    {
        int iReturn = 0;
        BuildSP();
        BuildParms();

        SqlParameter sp = AddParm_Return();
        try
        {
            sCmd.Connection.Open();
            sCmd.ExecuteNonQuery();
            iReturn = check.Int(sp.Value);
        }
        catch (Exception ex)
        {
            db_exception = ex;
        }
        finally
        {
            CloseAll();
        }

        return iReturn;
    }
    public object ExecScaler()
    {
        object oReturn = null;
        BuildSP();
        BuildParms();

        SqlParameter sp = AddParm_Return();
        try
        {
            sCmd.Connection.Open();
            oReturn = sCmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            db_exception = ex;
        }
        finally
        {
            CloseAll();
        }

        return oReturn;
    }


    #region "dispose"

    private void CloseAll()
    {
        sCmd.Connection.Close();
        sCmd.Connection.Dispose();
        sCmd.Dispose();
    }

    /// <summary>
    /// disposes all objects
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool disposed = false;
    private void Dispose(bool disposing)
    {

        if (!this.disposed)
        {
            if (disposing)
            {
                if (SQL != null)
                {
                    if (SQL.State == ConnectionState.Open)
                    { SQL.Close(); }
                }
                if (sCmd != null)
                {
                    sCmd.Connection.Dispose();
                    sCmd.Dispose();
                }
                if (SQL != null)
                {
                    SQL.Dispose();
                }
            }
            disposed = true;
        }
    }

    #endregion
    #endregion
}



/// <summary>
/// stored proc parameter 
/// </summary>
public class spParam
{
    public string paramName { get; set; }
    public object paramValue { get; set; }

    public spParam(string _paramName, object _paramValue)
    {
        paramName = _paramName;
        paramValue = _paramValue;
    }

}