using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoWebClient
{
    public class OrderStatusConverter
    {
        public static string Convert(long orderNumber)
        {
            if (orderNumber == -1) return "Free";

            List<OrderDTO> list = SessionManager.OrderList;

            foreach (OrderDTO order in list)
            {
                if (order.OrderNumber == orderNumber)
                {
                    return Convert(order.OrderStatusId);
                }
            }

            return "Unknown";
        }

        public static string Convert(int orderStatusId)
        {
            List<OrderStatusDTO> list = SessionManager.OrderStatusList;

            foreach (OrderStatusDTO status in list)
            {
                if (status.OrderStatusId == orderStatusId)
                {
                    return status.OrderStatusName;
                }
            }

            return "Unknown";
        }

        public static int Convert(string orderStatusName)
        {
            List<OrderStatusDTO> list = SessionManager.OrderStatusList;

            foreach (OrderStatusDTO status in list)
            {
                if (status.OrderStatusName == orderStatusName)
                {
                    return status.OrderStatusId;
                }
            }

            return -1;
        }
    }
}