using RestoService;
using RestoShared;
using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace RestoWebClient
{
    public static class SessionManager
    {
        private static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        // GENERAL
        public static void Add(string key, object value)
        {
            Session.Add(key, value);
        }
        public static void Remove(string key)
        {
            Session.Remove(key);
        }
        public static object Get(string key)
        {
            return Session[key];
        }
        public static bool Contains(string key)
        {
            return Session[key] != null;
        }

        // SECURITY
        public static EmployeeDTO LoggedUser
        {
            get
            {
                return Session["LoggedUser"] != null ? (EmployeeDTO)Session["LoggedUser"] : null;
            }
        }
        public static bool IsLogged
        {
            get
            {
                return Session["IsLogged"] != null && (bool)Session["IsLogged"];
            }
        }
        public static bool IsLoggedAsManager
        {
            get
            {
                return Session["IsLoggedAsManager"] != null && (bool)Session["IsLoggedAsManager"];
            }
        }
        public static AccessLevel LoggedAccessLevel
        {
            get
            {
                return Session["LoggedAccessLevel"] != null ? (AccessLevel)Session["LoggedAccessLevel"] : AccessLevel.NOT_LOGGED;
            }
        }

        public static bool Login(string employeeNumber)
        {
            var loginResponse = RestoManager.Security.Login(employeeNumber);

            if (!loginResponse.IsSuccess) return false;

            Session["LoggedUser"] = RestoManager.Security.LoggedUser;;
            Session["IsLoggedAsManager"] = RestoManager.Security.IsLoggedAsManager;
            Session["IsLogged"] = RestoManager.Security.IsLogged;
            Session["LoggedAccessLevel"] = RestoManager.Security.LoggedAccessLevel;

            return true;
        }
        public static bool Logout()
        {
            RestoManager.Security.Logout();
            if (!IsLogged) return false;

            Session["LoggedUser"] = null;
            Session["IsLoggedAsManager"] = false;
            Session["IsLogged"] = false;
            Session["LoggedAccessLevel"] = AccessLevel.NOT_LOGGED;

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            return true;
        }

        // DATA
        // Role
        public static List<RoleDTO> RoleList
        {
            get
            {
                if (Session["RoleList"] == null)
                {
                    if (!LoadRoleList()) return null;
                }

                return (List<RoleDTO>)Session["RoleList"];
            }
        }
        public static bool LoadRoleList()
        {
            var response = RestoManager.Data.Role.GetAll();

            if (!response.IsSuccess) return false;

            Session["RoleList"] = response.Data;

            return true;
        }

        // Employee
        public static List<EmployeeDTO> EmployeeList
        {
            get
            {
                if (Session["EmployeeList"] == null)
                {
                    if (!LoadEmployeeList()) return null;
                }

                return (List<EmployeeDTO>)Session["EmployeeList"];                
            }
        }
        public static bool LoadEmployeeList()
        {
            var response = RestoManager.Data.Employee.GetAll();

            if (!response.IsSuccess) return false;

            Session["EmployeeList"] = response.Data;

            return true;
        }
        public static EmployeeDTO EmployeeById(int id)
        {
            return EmployeeList.Where(x => x.EmployeeId == id).FirstOrDefault();
        }

        // ProductCategory
        public static List<ProductCategoryDTO> ProductCategoryList
        {
            get
            {
                if (Session["ProductCategoryList"] == null)
                {
                    if (!LoadProductCategoryList()) return null;
                }

                return (List<ProductCategoryDTO>)Session["ProductCategoryList"];
            }
        }
        public static bool LoadProductCategoryList()
        {
            var response = RestoManager.Data.ProductCategory.GetAll();

            if (!response.IsSuccess) return false;

            Session["ProductCategoryList"] = response.Data;

            return true;
        }

        // Product
        public static List<ProductDTO> ProductList
        {
            get
            {
                if (Session["ProductList"] == null)
                {
                    if (!LoadProductList()) return null;
                }

                return (List<ProductDTO>)Session["ProductList"];
            }
        }
        public static bool LoadProductList()
        {
            var response = RestoManager.Data.Product.GetAll();

            if (!response.IsSuccess) return false;

            Session["ProductList"] = response.Data;

            return true;
        }
        public static ProductDTO ProductById(int id)
        {
            return ProductList.Where(x => x.ProductId == id).FirstOrDefault();
        }

        // OrderStatus
        public static List<OrderStatusDTO> OrderStatusList
        {
            get
            {
                if (Session["OrderStatusList"] == null)
                {
                    if (!LoadOrderStatusList()) return null;
                }

                return (List<OrderStatusDTO>)Session["OrderStatusList"];
            }
        }
        public static bool LoadOrderStatusList()
        {
            var response = RestoManager.Data.OrderStatus.GetAll();

            if (!response.IsSuccess) return false;

            Session["OrderStatusList"] = response.Data;

            return true;
        }
        public static OrderStatusDTO OrderStatusById(int id)
        {
            return OrderStatusList.Where(x => x.OrderStatusId == id).FirstOrDefault();
        }

        // Order
        public static List<OrderDTO> OrderList
        {
            get
            {
                if (Session["OrderList"] == null)
                {
                    if (!LoadOrderList()) return null;
                }

                return (List<OrderDTO>)Session["OrderList"];
            }
        }
        public static bool LoadOrderList()
        {
            var response = RestoManager.Data.Order.GetAll();

            if (!response.IsSuccess) return false;

            Session["OrderList"] = response.Data;

            return true;
        }
        public static OrderDTO OrderByOrderNumber(long orderNumber)
        {
            return OrderList.Where(x => x.OrderNumber == orderNumber).FirstOrDefault();
        }

        // OrderItem
        public static List<OrderItemDTO> OrderItemList
        {
            get
            {
                if (Session["OrderItemList"] == null)
                {
                    if (!LoadOrderItemList()) return null;
                }

                return (List<OrderItemDTO>)Session["OrderItemList"];
            }
        }
        public static bool LoadOrderItemList()
        {
            var response = RestoManager.Data.OrderItem.GetAll();

            if (!response.IsSuccess) return false;

            Session["OrderItemList"] = response.Data;

            return true;
        }
        public static List<OrderItemDTO> OrderItemListByOrderNumber(long orderNumber)
        {
            return OrderItemList.FindAll(x => x.OrderNumber == orderNumber);
        }
        public static OrderItemDTO OrderItemById(long id)
        {
            return OrderItemList.Where(x => x.OrderItemId == id).FirstOrDefault();
        }

        // RestoTable
        public static List<RestoTableDTO> RestoTableList
        {
            get
            {
                if (Session["RestoTableList"] == null)
                {
                    if (!LoadRestoTableList()) return null;
                }

                return (List<RestoTableDTO>)Session["RestoTableList"];
            }
        }
        public static bool LoadRestoTableList()
        {
            var response = RestoManager.Data.RestoTable.GetAll();

            if (!response.IsSuccess) return false;

            Session["RestoTableList"] = response.Data;

            return true;
        }
        public static RestoTableDTO RestoTableById(byte tableNumber)
        {
            return RestoTableList.Where(x => x.TableNumber == tableNumber).FirstOrDefault();
        }
    }
}