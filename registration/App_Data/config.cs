using System;
using System.Web;

/// <summary>
/// Summary description for config
/// </summary>
public class config
{
    public config()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string siteDirName
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["siteDirName"]);
        }
    }

    public static string siteHost
    {
        get
        {
            return GetASBaseUrl();
        }

    }

    public static string GetASBaseUrl()
    {
        string baseUrl = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme,
        HttpContext.Current.Request.Url.Authority,
        HttpContext.Current.Request.ApplicationPath);
        baseUrl = baseUrl.TrimEnd('/');
        return baseUrl;
    }


    public static string emailServer
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["emailServer"]);
        }
    }

    public static string Redirect404
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["404Redirect"]);
        }
    }

    public static string ConnStringMAIN
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.ConnectionStrings["Main"].ConnectionString);
        }

    }


    public static string HomeURL
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["HomeURL"]);
        }

    }

    public static string Domain
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["Domain"]);
        }
    }

    public static string DomainDisplay
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["DomainDisplay"]);
        }
    }

    public static string ExceptionLog
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["ExceptionLog"]);
        }
    }

    public static string ConnectionInfo
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["ConnectionInfo"]);
        }
    }

    public static string adminPW
    {
        get
        {
            return check.String(System.Configuration.ConfigurationManager.AppSettings["adminPW"]);
        }
    }


    public static bool showIssueReporting
    {
        get
        {
            return check.Bool(System.Configuration.ConfigurationManager.AppSettings["showIssueReporting"]);
        }
    }
    public static string websiteState { get { return check.String(System.Configuration.ConfigurationManager.AppSettings["websiteState"]); } }
}
