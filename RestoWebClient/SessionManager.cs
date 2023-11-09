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
    }
}