using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class Orders : System.Web.UI.Page
    {
        public OrderDTO SelectedOrder;
        public List<OrderItemDTO> SelectedOrderItemList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptRestoTables.DataSource = SessionManager.RestoTableList;
                rptRestoTables.DataBind();

                if (SessionManager.Contains("SelectedOrder"))
                {
                    SelectedOrder = (OrderDTO)SessionManager.Get("SelectedOrder");
                    SelectedOrderItemList = SessionManager.OrderItemListByOrderNumber(SelectedOrder.OrderNumber);
                } else
                {
                    SelectedOrder = null;
                }                
            }
        }

        protected void BtnSeeOrder_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var orderNumber = Convert.ToInt64(btn.CommandArgument);

            SelectedOrder = SessionManager.OrderByOrderNumber(orderNumber);
            SelectedOrderItemList = SessionManager.OrderItemListByOrderNumber(orderNumber);

            SessionManager.Add("SelectedOrder", SelectedOrder);

            BtnModifyOrder.CommandArgument = orderNumber.ToString();
        }

        protected void BtnAddOrder_Click(object sender, EventArgs e)
        {
            Router.RedirectTo(RouteName.ORDERITEMFORM);
        }

        protected void BtnModifyOrder_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            long orderNumber = Convert.ToInt64(btn.CommandArgument);

            Router.RedirectTo(RouteName.ORDERITEMFORM, "orderNumber", orderNumber);
        }
    }
}