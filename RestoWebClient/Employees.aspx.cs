using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EmployeeList.DataSource = SessionManager.EmployeeList;
                EmployeeList.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Router.RedirectTo(RouteName.EMPLOYEEFORM);
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            Router.RedirectTo(RouteName.EMPLOYEEFORM, "id", id);
        }
    }
}