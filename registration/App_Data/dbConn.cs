using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
/// <summary>
/// dbConn holds Connection String and SQLCommand properties
/// </summary>
public class dbConn
{

    #region SQL Connections
    public static SqlConnection SQL()
    {//main sql connection  
        SqlConnection conn = new SqlConnection(config.ConnStringMAIN);
        return conn;
    }
  
    public static SqlCommand SqlSP(string StoredProc)
    {
        return pSqlSP(SQL(), StoredProc);
    }

    public static SqlCommand SqlSP(string StoredProc, SqlConnection Conn)
    {
        return pSqlSP(Conn, StoredProc);
    }

    private static SqlCommand pSqlSP(SqlConnection Conn, string StoredProc)
    {
        SqlCommand oCmd = new SqlCommand();
        try
        {
            //Conn.DataSource + "." + Conn.Database + ".dbo." + 
            string cmdText = Conn.Database + ".dbo." + StoredProc; 
            oCmd = new SqlCommand();
            oCmd.Connection = Conn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = cmdText;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return oCmd;
    } 
    #endregion

    #region SQL Stored Procedure Calls
    public static DataTable spDataTable(SqlCommand sCmd)
    {
        DataTable dtReturn = new DataTable();
        SqlDataReader dr;
       
        try
        {
            sCmd.Connection.Open();
            dr = sCmd.ExecuteReader();
            dtReturn.Load(dr);
            dr.Dispose();
        }
        catch (Exception ex)
        {
            string blah = ex.Message;
            throw ex;
        }
        finally
        {
            sCmd.Connection.Close();
            sCmd.Connection.Dispose();
            sCmd.Dispose();
          
        }
        
        return dtReturn;
    }

    public static bool spUpdate(SqlCommand sCmd)
    {
        bool bReturn = false;
        try
        {
            sCmd.Connection.Open();
            sCmd.ExecuteNonQuery();
            bReturn = true;
        }
        catch (Exception ex)
        {
            bReturn = false;
            throw ex;
        }
        finally
        {
            sCmd.Connection.Close();
            sCmd.Connection.Dispose();
            sCmd.Dispose();
        }
        return bReturn;
    }

    public static int spUpdateWithReturn(SqlCommand sCmd)
    {
        SqlParameter sp = AddParm_Return(sCmd);
        int iReturn = 0;

        try
        {
            sCmd.Connection.Open();
            sCmd.ExecuteNonQuery();
            int id = check.Int(sp.Value);
            if (id > 0)
            {
                iReturn = id;
            }
            else { iReturn = 0; }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            sCmd.Connection.Close();
            sCmd.Connection.Dispose();
            sCmd.Dispose();
        }
        return iReturn;
    }
    
    #endregion

    #region Add Stored Procedure Parm
    public static SqlCommand AddParm(int Value, string ParmName, SqlCommand sCmd)
    {
        sCmd.Parameters.Add(ParmName, SqlDbType.Int).Value = Value;
        return sCmd;
    }
    public static SqlCommand AddParm(string Value, string ParmName, SqlCommand sCmd)
    {
        sCmd.Parameters.Add(ParmName, SqlDbType.NVarChar).Value = Value;
        return sCmd;
    }
    public static SqlCommand AddParm(string Value, string ParmName, SqlCommand sCmd, int Size)
    {
        sCmd.Parameters.Add(ParmName, SqlDbType.NVarChar,Size).Value = Value;
        return sCmd;
    }
    public static SqlCommand AddParm(DateTime Value, string ParmName, SqlCommand sCmd)
    {
        sCmd.Parameters.Add(ParmName, SqlDbType.DateTime).Value = Value;
        return sCmd;
    }

    public static SqlCommand AddParm(bool Value, string ParmName, SqlCommand sCmd)
    {
        sCmd.Parameters.Add(ParmName, SqlDbType.Bit).Value = Value;
        return sCmd;
    }
    public static SqlCommand AddParm(System.Data.SqlTypes.SqlGuid Value, string ParmName, SqlCommand sCmd)
    {//double check this sqldbtype
        sCmd.Parameters.Add(ParmName, SqlDbType.UniqueIdentifier).Value = Value;
        return sCmd;
    }
    public static SqlCommand AddParm(Double Value, string ParmName, SqlCommand sCmd)
    {
        sCmd.Parameters.Add(ParmName, SqlDbType.Float).Value = Value;
        return sCmd;
    }

    public static SqlCommand AddParm(DBNull Value, string ParmName, SqlCommand sCmd)
    {
        //sCmd.Parameters.Add(ParmName,);
        SqlParameter sp = new SqlParameter(ParmName,DBNull.Value);
        sCmd.Parameters.Add(sp);
        return sCmd;
    }


    private static SqlParameter AddParm_Return(SqlCommand sCmd)
    {
        SqlParameter newReqNumber = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
        sCmd.Parameters.Add(newReqNumber);
        newReqNumber.Direction = ParameterDirection.ReturnValue;
        return newReqNumber;
    }

   

    #endregion

 
    public static int GetInt(SqlCommand sCmd, string FieldName)
    {
        int iReturn = 0;
        foreach (DataRow row in dbConn.spDataTable(sCmd).Rows)
        {
            iReturn = check.Int(row[FieldName], 0);
            break;
        }
        sCmd.Dispose();
        return iReturn;    
    }

    public static string GetStr(SqlCommand sCmd, string FieldName)
    {
        string sReturn = "";
        foreach (DataRow row in dbConn.spDataTable(sCmd).Rows)
        {
            sReturn = check.String(row[FieldName], "");
            break;
        }
        sCmd.Dispose();
        return sReturn;
    }

    public static int GetReturnValue(SqlCommand sCmd)
    {
        SqlParameter sp = AddParm_Return(sCmd);
        int iReturn = 0;

        try
        {
            sCmd.Connection.Open();
            sCmd.ExecuteNonQuery();
            int id = check.Int(sp.Value,0);
            iReturn = id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            sCmd.Connection.Close();
            sCmd.Connection.Dispose();
            sCmd.Dispose();
        }
        return iReturn;
    }

    public static System.Collections.ArrayList GetArrayList(SqlCommand sCmd, string FieldName)
    {//will load an arraylist with the values in 1 column of the sCmd result 
        System.Collections.ArrayList alReturn = new System.Collections.ArrayList();

        DataTable dt = dbConn.spDataTable(sCmd);
        try
        {
            foreach (DataRow Row in dt.Rows)
            {
                string item = check.String(Row[FieldName], "");
                if (item.Length > 0)
                {
                    alReturn.Add(item);
                }
            }
           
            dt.Dispose();
            sCmd.Dispose();
        }
        catch { }
      
        return alReturn;
    }



}
