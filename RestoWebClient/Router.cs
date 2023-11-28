using RestoService;
using RestoShared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace RestoWebClient
{
    public enum RouteName
        {
            ROOT,
            DEFAULT,
            LOGIN,
            NOT_FOUND,

            REGISTER,

            ORDERS,
            ORDERITEMFORM,

            EMPLOYEES,
            EMPLOYEEFORM,

            PRODUCTS,
            PRODUCTFORM,

            REPORTS,
        }

    public class Router
    {
        private readonly Dictionary<RouteName, AccessLevel> RouteAuth = new Dictionary<RouteName, AccessLevel>()
        {
            { RouteName.ROOT, AccessLevel.LOGGED },
            { RouteName.DEFAULT, AccessLevel.LOGGED },
            { RouteName.LOGIN, AccessLevel.NOT_LOGGED },
            { RouteName.NOT_FOUND, AccessLevel.NOT_LOGGED },
            { RouteName.REGISTER, AccessLevel.MANAGER },
            { RouteName.ORDERS, AccessLevel.LOGGED },
            { RouteName.ORDERITEMFORM, AccessLevel.LOGGED },
            { RouteName.EMPLOYEES, AccessLevel.MANAGER },
            { RouteName.EMPLOYEEFORM, AccessLevel.MANAGER },
            { RouteName.PRODUCTS, AccessLevel.MANAGER },
            { RouteName.PRODUCTFORM, AccessLevel.MANAGER },
            { RouteName.REPORTS, AccessLevel.MANAGER },
        };

        public static readonly Dictionary<RouteName, string> RouteUrl = new Dictionary<RouteName, string>()
        {
            { RouteName.ROOT, "default.aspx" },
            { RouteName.DEFAULT, "Default.aspx"},
            { RouteName.LOGIN, "Login.aspx"},
            { RouteName.NOT_FOUND, "404.aspx"},
            { RouteName.REGISTER, "RegisterForm.aspx"},
            { RouteName.ORDERS, "Orders.aspx" },
            { RouteName.ORDERITEMFORM, "OrderItemForm.aspx" },
            { RouteName.EMPLOYEES, "Employees.aspx" },
            { RouteName.EMPLOYEEFORM, "EmployeeForm.aspx" },
            { RouteName.PRODUCTS, "Products.aspx" },
            { RouteName.PRODUCTFORM, "ProductForm.aspx" },
            { RouteName.REPORTS, "Reports.aspx" },
        };

        public static string CurrentRoute
        {
            get
            {
                return HttpContext.Current.Request.Url.AbsolutePath.Substring(1);
            }            
        }

        public void AuthNavigation()
        {   
            // check if path exists in RouteUrl
            foreach (KeyValuePair<RouteName, string> route in RouteUrl)
            {
                if (route.Value == path)
                {
                    if (RestoManager.Security.IsAuth(SessionManager.LoggedAccessLevel, RouteAuth[route.Key]))
                    {
                        RedirectTo(RouteName.LOGIN);
                    }
                }
            }
        }

        public static void RedirectOnError()
        {
            HttpContext.Current.Response.Redirect(RouteUrl[RouteName.NOT_FOUND]);
        }

        public static void RedirectTo(RouteName route)
        {
            HttpContext.Current.Response.Redirect(RouteUrl[route], false);
        }

        // Test if this works
        public static void RedirectTo(RouteName route, string query)
        {
            HttpContext.Current.Response.Redirect(RouteUrl[route] + query, false);
        }
        public static void RedirectTo(RouteName route, string query, string queryValue)
        {
            HttpContext.Current.Response.Redirect(RouteUrl[route] + "?" + query + "=" + queryValue, false);
        }
        public static void RedirectTo(RouteName route, string query, int queryValue)
        {
            HttpContext.Current.Response.Redirect(RouteUrl[route] + "?" + query + "=" + queryValue, false);
        }
        public static void RedirectTo(RouteName route, string query, long queryValue)
        {
            HttpContext.Current.Response.Redirect(RouteUrl[route] + "?" + query + "=" + queryValue, false);
        }
    }
}