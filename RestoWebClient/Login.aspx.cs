using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string employeeNumber = employeeNumberInput.Value;
            SessionManager.Login(employeeNumber);

            if (SessionManager.IsLogged)
            {
                Router.RedirectTo(RouteName.ROOT);
            }
        }
    }
}