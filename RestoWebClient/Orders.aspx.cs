using RestoService;
using RestoShared.DTO;
using RestoShared.ITable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class Orders : System.Web.UI.Page
    {
        public OrderDTO SelectedOrder
        {
            get
            {
                return SessionManager.Get("SelectedOrder") as OrderDTO;
            }

            set
            {
                AddSelectedOrder(value);
            }
        }
        public List<OrderItemDTO> SelectedOrderItemList
        {
            get
            {
                return SessionManager.Get("SelectedOrderItemList") as List<OrderItemDTO>;
            }

            set
            {
                AddSelectedOrderItemList(value);
            }
        }
        public List<RestoTableDTO> VisibleTableList
        {
            get
            {
                return SessionManager.Get("VisibleTableList") as List<RestoTableDTO>;
            }

            set
            {
                AddVisibleTableList(value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTableRepeater();

                if (SessionManager.Contains("SelectedOrder"))
                {
                    SelectedOrder = (OrderDTO)SessionManager.Get("SelectedOrder");
                    SelectedOrderItemList = SessionManager.OrderItemListByOrderNumber(SelectedOrder.OrderNumber);
                } else
                {
                    Debug.WriteLine("SelectedOrder not found in session");
                    SelectedOrder = null;
                }         
            }
        }

        private void AddSelectedOrder(OrderDTO order)
        {
            if (SessionManager.Contains("SelectedOrder"))
            {
                SessionManager.Remove("SelectedOrder");
            }

            SessionManager.Add("SelectedOrder", order);
        }
        private void AddSelectedOrderItemList(List<OrderItemDTO> orderItemList)
        {
            if (SessionManager.Contains("SelectedOrderItemList"))
            {
                SessionManager.Remove("SelectedOrderItemList");
            }

            SessionManager.Add("SelectedOrderItemList", orderItemList);
        }
        private void AddVisibleTableList(List<RestoTableDTO> tableList)
        {
            if (SessionManager.Contains("VisibleTableList"))
            {
                SessionManager.Remove("VisibleTableList");
            }

            SessionManager.Add("VisibleTableList", tableList);
        }

        private void CheckSelectedOrderForWaiter()
        {
            if (!SessionManager.IsLoggedAsManager)
            {
                // selected order is the first order of RestoTableList, maybe RestoTableList not contains an order number
                foreach (var table in VisibleTableList)
                {
                    if (table.OrderNumber != -1)
                    {
                        SelectedOrder = SessionManager.OrderByOrderNumber(table.OrderNumber);
                        SelectedOrderItemList = SessionManager.OrderItemListByOrderNumber(table.OrderNumber);
                        return;
                    }
                }
            }
        }

        private void LoadTableRepeater()
        {
            if (SessionManager.IsLoggedAsManager)
            {
                rptRestoTables.DataSource = SessionManager.RestoTableList;
                rptRestoTables.DataBind();

                VisibleTableList = SessionManager.RestoTableList;

                CheckSelectedOrderForWaiter();
            }
            else if (SessionManager.IsLogged)
            {
                //filter by logged waiter and free tables
                // tables contains orders, orders contains employee, employee contains role
                var waiterTables = SessionManager.RestoTableList.Where(t => t.OrderNumber == -1 && t.IsActive).ToList();

                var orders = SessionManager.OrderList.Where(o => o.EmployeeId == SessionManager.LoggedUser.EmployeeId).ToList();

                foreach (var order in orders)
                {
                    var table = SessionManager.RestoTableById(order.TableNumber);
                    
                    if (table != null) waiterTables.Add(table);
                }
                
                waiterTables = waiterTables.OrderBy(t => t.TableNumber).ToList();

                rptRestoTables.DataSource = waiterTables;
                rptRestoTables.DataBind();

                VisibleTableList = waiterTables;

                CheckSelectedOrderForWaiter();
            }            
        }
        private void LoadTableRepeater(List<RestoTableDTO> list)
        {
            rptRestoTables.DataSource = list;
            rptRestoTables.DataBind();
        }

        protected void BtnSeeOrder_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var orderNumber = Convert.ToInt64(btn.CommandArgument);

            SelectedOrder = SessionManager.OrderByOrderNumber(orderNumber);
            SelectedOrderItemList = SessionManager.OrderItemListByOrderNumber(orderNumber);

            AddSelectedOrder(SelectedOrder);

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

        public string UpdateOrderStatus(long orderNumber)
        {
            return OrderStatusConverter.Convert(orderNumber);
        }

        public string UpdateTableStatusBar(byte tableNumber)
        {
            var table = SessionManager.RestoTableById(tableNumber);
            string statusCss = "p-1 ";

            if (!table.IsActive) return statusCss += "bg-gray-700";
            if (table.OrderNumber == -1) return statusCss + "bg-green-500";

            // Status
            OrderDTO order = SessionManager.OrderByOrderNumber(table.OrderNumber);
            OrderStatusDTO status = SessionManager.OrderStatusById(order.OrderStatusId);

            if (Convert.ToInt32(status.StatusCode) < 200) return statusCss + "bg-yellow-300";
            
            return status + "bg-blue-700";
        }

        public string GetOrderDate(long orderNumber)
        {
            if (orderNumber == -1) return "-";

            var order = SessionManager.OrderByOrderNumber(orderNumber);
            return order.OrderDate.ToString("dd/MM - HH:mm");
        }

        public string GetOrderUpdatedAt(long orderNumber)
        {
            if (orderNumber == -1) return "-";

            var order = SessionManager.OrderByOrderNumber(orderNumber);
            return order.UpdatedAt.ToString("dd/MM - HH:mm");
        }

        protected void BtnDeactivateTable_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            byte tableNumber = Convert.ToByte(btn.CommandArgument);

            RestoManager.Data.RestoTable.Initialize(new RestoTableDTO { TableNumber = tableNumber });
            var response = RestoManager.Data.RestoTable.Deactivate();

            if (response.IsSuccess)
            {
                SessionManager.LoadRestoTableList();
                LoadTableRepeater();
            }
        }

        protected void BtnActivateTable_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            byte tableNumber = Convert.ToByte(btn.CommandArgument);

            RestoManager.Data.RestoTable.Initialize(new RestoTableDTO { TableNumber = tableNumber });
            var response = RestoManager.Data.RestoTable.Activate();

            if (response.IsSuccess)
            {
                SessionManager.LoadRestoTableList();
                LoadTableRepeater();
            }
        }

        protected void BtnAddRestoTable_ServerClick(object sender, EventArgs e)
        {
            RestoTableDTO newTable = new RestoTableDTO();

            var tableList = SessionManager.RestoTableList;
            var lastTable = tableList.Last();
            newTable.TableNumber = (byte)(lastTable.TableNumber + 1);

            RestoManager.Data.RestoTable.Initialize(newTable);
            var Response = RestoManager.Data.RestoTable.Add();

            if (Response.IsSuccess)
            {
                SessionManager.LoadRestoTableList();
                LoadTableRepeater();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage","alert('"+ "Table " + newTable.TableNumber + " added" +"')", true);
            } else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('"+ "error: " + Response.Message +"')", true);
            }
        }

        protected void BtnDeleteTable_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            byte tableNumber = Convert.ToByte(btn.CommandArgument);

            RestoManager.Data.RestoTable.Initialize(new RestoTableDTO { TableNumber = tableNumber });
            var response = RestoManager.Data.RestoTable.Delete();

            if (response.IsSuccess)
            {
                SessionManager.LoadRestoTableList();
                LoadTableRepeater();

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + "Table " + tableNumber + " deleted" + "')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + "error: " + response.Message + "')", true);
            }
        }

        protected void Search_TextChanged(object sender, EventArgs e)
        {
            // filter by input text
            var input = Search.Text.ToLower();

            if (input == string.Empty)
            {
                LoadTableRepeater();
                return;
            }

            var filteredTableList = new List<RestoTableDTO>();

            // if order number equals -1 filter by table number

            foreach (var table in VisibleTableList)
            {
                if (table.OrderNumber == -1)
                {
                    if (table.TableNumber.ToString().Contains(input))
                    {
                        filteredTableList.Add(table);
                    }
                } else
                {
                    var order = SessionManager.OrderByOrderNumber(table.OrderNumber);

                    if (order.OrderNumber.ToString().Contains(input) || table.TableNumber.ToString().Contains(input))
                    {
                        filteredTableList.Add(table);
                    }
                }
            }

            //filteredTableList = VisibleTableList.FindAll(t => t.TableNumber.ToString().Contains(input) || t.OrderNumber.ToString().Contains(input));

            LoadTableRepeater(filteredTableList);
        }
    }
}