using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace registration
{
    public partial class AdminReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Registration reg = new Registration();
                DataTable dt = new DataTable();
                dt = Registration.Get();
                gvAdminReport.DataSource = dt;
                gvAdminReport.DataBind();

            }
        }
    }
}