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
                
                if (!SessionManager.IsLoggedAsManager)
                {
                    DivEmployee.Visible = false;
                    DivProduct.Visible = false;
                    DivReport.Visible = false;

                    
                    DivOrder.Attributes["class"] = "absolute top-1/3 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-indigo-500 text-white p-6 text-center box-border border-4 w-1/4 rounded-lg";
                }
            
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