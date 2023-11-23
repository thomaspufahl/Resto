using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class OrderItemForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["orderNumber"] != null)
            {
                long orderNumber = Convert.ToInt64(Request.QueryString["orderNumber"]);
                OrderDTO ord = SessionManager.OrderByOrderNumber(orderNumber);
            }
        }
    }
}