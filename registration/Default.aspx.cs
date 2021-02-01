using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace registration
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //added for server side check
            if (txtFirstName.Text.Length > 0)
            {

                Registration reg = new Registration();
                reg.FirstName = txtFirstName.Text;
                reg.LastName = txtLastName.Text;
                reg.Address1 = txtAddress1.Text;
                reg.Address2 = txtAddress2.Text;
                reg.City = txtCity.Text;
                reg.State = DropDownListState.Text;
                reg.Zip = txtZip.Text;
                reg.Country = txtCountry.Text;
                reg.AddRegistration();
                Response.Redirect("~/Confirmation.aspx");
            }
            else
            {
                lblNameError.Visible = true;
            }
            

        }

        
    }
}