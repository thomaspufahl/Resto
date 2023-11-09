using RestoService;
using RestoShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace RestoWebClient
{
    public class SessionManager
    {
        private Page Page { get; set; }
        public SessionManager(Page page)
        {
            Page = page;
        }

        public void Login(string employeeNumber)
        {
            RestoManager.Security.Login(employeeNumber);
            Page.Session["LoggedUser"] = RestoManager.Security.LoggedUser;
        }

        public void Logout()
        {
            RestoManager.Security.Logout();
            Page.Session["LoggedUser"] = null;
        }

        public bool IsSessionActive
        {
            get
            {
                return Page.Session["LoggedUser"] != null;
            }
        }
    }
}