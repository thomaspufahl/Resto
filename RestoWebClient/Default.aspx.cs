using RestoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.IsLogged) { 
            
                WelcomeMessage.InnerText = "!Bienvenido " + RestoManager.Security.LoggedUser.FirstName + "!";
            
            }


          

        }

        protected void Employee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employees.aspx");
        }

        protected void Products_Click(object sender, EventArgs e)
        {
            Response.Redirect("Products.aspx");
        }

        protected void Orders_Click(object sender, EventArgs e)
        {
            Response.Redirect("Orders.aspx");
        }

        protected void Reports_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx");
        }
    }
}