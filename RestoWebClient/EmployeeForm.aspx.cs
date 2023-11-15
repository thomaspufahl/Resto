using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class EmployeeForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    EmployeeDTO emp = SessionManager.EmployeeById(id);

                    if (emp != null) 
                    { 
                        firstName.Value = emp.FirstName;
                        lastName.Value = emp.LastName;
                        employeeNumber.Value = emp.EmployeeNumber;
                    }
                }
            }
        }
    }
}