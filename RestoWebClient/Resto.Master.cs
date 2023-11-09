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

            Router.AuthorizeNavigation(HttpContext.Current.Request.Url.AbsolutePath.Substring(1));
        }
    }
}