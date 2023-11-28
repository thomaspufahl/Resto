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
            if (SessionManager.IsLogged) 
            {                 
                if (!SessionManager.IsLoggedAsManager)
                {
                    BtnEmployees.Visible = false;
                    BtnProducts.Visible = false;
                    BtnReports.Visible = false;
                }            
            }       
        }

        protected void Employee_Click(object sender, EventArgs e)
        {
            Router.RedirectTo(RouteName.EMPLOYEES);
        }

        protected void Products_Click(object sender, EventArgs e)
        {
            Router.RedirectTo(RouteName.PRODUCTS);
        }

        protected void Orders_Click(object sender, EventArgs e)
        {
            Router.RedirectTo(RouteName.ORDERS);            
        }

        protected void Reports_Click(object sender, EventArgs e)
        {
            Router.RedirectTo(RouteName.REPORTS);
        }
    }
}