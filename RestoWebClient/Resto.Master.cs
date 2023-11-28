using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class Resto : System.Web.UI.MasterPage
    {
        private readonly Router Router = new Router();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LinkDashboard.PostBackUrl = Router.RouteUrl[RouteName.DEFAULT];
                LinkOrders.PostBackUrl = Router.RouteUrl[RouteName.ORDERS];
                LinkEmployees.PostBackUrl = Router.RouteUrl[RouteName.EMPLOYEES];
                LinkProducts.PostBackUrl = Router.RouteUrl[RouteName.PRODUCTS];
                LinkReports.PostBackUrl = Router.RouteUrl[RouteName.REPORTS];

                if (!SessionManager.IsLoggedAsManager)
                {
                    LinkEmployees.Visible = false;
                    LinkProducts.Visible = false;
                    LinkReports.Visible = false;
                }

                if (SessionManager.IsLogged)
                {
                    loggedUser.InnerHtml = SessionManager.LoggedUser.FirstName + " " + SessionManager.LoggedUser.LastName;
                }
            }
            Router.AuthNavigation();
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
            Router.RedirectTo(RouteName.LOGIN);
        }
    }
}