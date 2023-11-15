using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProductList.DataSource = SessionManager.ProductList;
                ProductList.DataBind();
            }
        }

        protected void UpdateProduct_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            Router.RedirectTo(RouteName.PRODUCTFORM, "id", id);
        }

        protected void AddProduct_Click(object sender, EventArgs e)
        {
            Router.RedirectTo(RouteName.PRODUCTFORM);
        }
    }
}