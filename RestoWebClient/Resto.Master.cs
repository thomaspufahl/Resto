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
            if (SessionManager.IsLogged)
            {
                Login.Visible = false;
            }
            else
            {
                btnLogout.Visible = false;
            }

            Router.AuthorizeNavigation(HttpContext.Current.Request.Url.AbsolutePath.Substring(1));
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
        }

        protected void Login_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("LoginForm.aspx");
        }
    }
}