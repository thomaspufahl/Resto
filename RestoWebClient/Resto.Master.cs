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
        SessionManager sessionManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sessionManager = new SessionManager(Page);

                if (!sessionManager.IsSessionActive)
                {
                    sessionManager.Login("E66666666");
                }
            }

            Router.AuthorizeNavigation(HttpContext.Current.Request.Url.AbsolutePath.Substring(1));
        }
    }
}