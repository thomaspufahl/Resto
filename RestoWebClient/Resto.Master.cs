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


            System.Diagnostics.Debug.WriteLine($"Resto.Master.cs: Page_Load: IsLogged: {SessionManager.IsLogged}");
            string name = SessionManager.IsLogged ? SessionManager.LoggedUser.FirstName : "No hay nadie logeado";
            System.Diagnostics.Debug.WriteLine($"Resto.Master.cs: Page_Load: LoggedUser: {name}");
            System.Diagnostics.Debug.WriteLine($"Resto.Master.cs: Page_Load: AccessLevel: {SessionManager.LoggedAccessLevel}");

            Router.AuthorizeNavigation(HttpContext.Current.Request.Url.AbsolutePath.Substring(1));
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
        }
    }
}