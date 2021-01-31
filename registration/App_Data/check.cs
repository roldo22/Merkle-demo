using System;
using System.Data.SqlTypes;
using System.Web.Security;

public class check
{

    public static bool Bool(object oBool, bool blnDefault = false)
    {
        bool bReturn = blnDefault;

        string sBool = check.String(oBool).ToLower();
        if (sBool == "true" || sBool == "1" || sBool == "yes" || sBool == "on")
        {
            bReturn = true;
        }
        else if (sBool == "false" || sBool == "0" || sBool == "no" || sBool == "off")
            bReturn = false;
        else
        {
            if (bool.TryParse(sBool, out bReturn) == false)
            {
                bReturn = blnDefault;
            }
        }
        return bReturn;
    }
    public static Int32 Int(object oNum)
    {
        return Int(oNum, 0);
    }
    public static Int32 Int(object oNum, int iDefault)
    {
        int iReturn = iDefault;
        if (!int.TryParse(check.String(oNum), out iReturn))
        {
            iReturn = iDefault;
        }

        return iReturn;

        //try
        //{
        //    return Convert.ToInt32(oNum.ToString());
        //}
        //catch
        //{
        //    return iDefault;
        //}
    }

    public static long Long(object oNum, long lDefault=0)
    {
        long lReturn;
        return (long.TryParse(check.String(oNum), out lReturn)) ? lReturn : lDefault;
    }

    public static decimal Decimal(object oNum, long dDefault = 0)
    {
        decimal dReturn;
        return (decimal.TryParse(check.String(oNum), out dReturn)) ? dReturn : dDefault;
    }


    public static decimal Decimal(object oNum, decimal dDefault)
    {
        decimal decReturn = dDefault;
        if (!decimal.TryParse(check.String(oNum), out decReturn))
        {
            decReturn = dDefault;
        }
        return decReturn;

        //try
        //{
        //    return Convert.ToDouble(oNum.ToString());
        //}
        //catch
        //{
        //    return iDefault;
        //}
    }


    public static double Double(object oNum, double dDefault)
    {
        double dReturn = dDefault;
        if (!double.TryParse(check.String(oNum), out dReturn))
        {
            dReturn = dDefault;
        }
        return dReturn;

        //try
        //{
        //    return Convert.ToDouble(oNum.ToString());
        //}
        //catch
        //{
        //    return iDefault;
        //}
    }
    public static DateTime Date(object oDate)
    {
        return Date(oDate, DateTime.MinValue);
    }
    //public static DateTime Date(string sDate, DateTime dDefault)
    //{
    //    DateTime dReturn = dDefault;

    //    if (!DateTime.TryParse(String(sDate)))


    //    try
    //    {
    //        return DateTime.Parse(sDate);
    //    }
    //    catch
    //    {
    //        return dDefault;

    //    }
    //}
    public static DateTime Date(object oDate, DateTime dDefault)
    {
        DateTime dReturn = dDefault;
        if (!DateTime.TryParse(check.String(oDate), out dReturn))
        {
            dReturn = dDefault;
        }
        return dReturn;


    }
    public static int BoolToNum(object bItem)
    {
        if (Bool(bItem, false))
            return 1;
        else
            return 0;
    }

    public static string String(object oStr)
    {
        return String(oStr, "");
    }
    //
    // chkStr - Check strings to ensure not DBNUll and whatnot
    //
    public static string String(object oStr, string sDefault)
    {
        if (oStr == null)
        {
            return sDefault;
        }
        else
        {
            return oStr.ToString();
        }
    }

    public static string String(object oStr, string sDefault, int Length)
    {

        string sReturn = String(oStr, sDefault);
        if (sReturn.Length > 0 && sReturn.Length > Length)
        {
            sReturn = sReturn.TrimStart();
            sReturn = sReturn.Substring(0, Length);
        }
        return sReturn;
    }


    //
    // chkStr - Check strings to ensure not DBNUll and whatnot
    //
    public static string StringClean(object oStr, string sDefault)
    {
        return String(oStr).TrimStart().TrimEnd();
    }
    public static SqlGuid GUID(object o)
    {
        SqlGuid guidReturn = new SqlGuid();

        try
        {
            if (o.ToString().Length > 0)
            {
                guidReturn = SqlGuid.Parse(o.ToString());
            }
        }
        catch
        {

        }
        return guidReturn;
    }
    public static SqlGuid GUID(MembershipUser oUser)
    {
        try
        {
            return SqlGuid.Parse(oUser.ProviderUserKey.ToString());
        }
        catch
        {
            return SqlGuid.Null;
        }
    }


    public static string Pad(object _Number, int TotalChars)
    {
        string Number = check.String(_Number);
        string sReturn = Number;
        if (Number.Length < TotalChars)
        {
            do
            {
                Number = "0" + Number;
                sReturn = Number;
            } while (sReturn.Length < TotalChars);
        }
        return sReturn;
    }
}