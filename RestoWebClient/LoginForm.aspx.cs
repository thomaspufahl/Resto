using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string employeeNumber = employeeNumberInput.Value;
            SessionManager.Login(employeeNumber);
            System.Diagnostics.Debug.WriteLine($"LoginForm.aspx.cs: BtnLogin_Click: employeeNumber: {employeeNumber}");

            if (SessionManager.IsLogged)
            {
                System.Diagnostics.Debug.WriteLine(SessionManager.LoggedUser.FirstName);
                Response.Redirect("Default.aspx");
            }
        }
    }
}