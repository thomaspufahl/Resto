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

            Session["LoggedUser"] = RestoManager.Security.LoggedUser;
            Session["IsLoggedAsManager"] = RestoManager.Security.IsLoggedAsManager;
            Session["IsLogged"] = RestoManager.Security.IsLogged;
            Session["LoggedAccessLevel"] = RestoManager.Security.LoggedAccessLevel;

            return true;
        }
        public static bool Logout()
        {
            //RestoManager.Security.Logout();
            if (!IsLogged) return false;

            Session["LoggedUser"] = null;
            Session["IsLoggedAsManager"] = false;
            Session["IsLogged"] = false;
            Session["LoggedAccessLevel"] = AccessLevel.NOT_LOGGED;

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
    }
}