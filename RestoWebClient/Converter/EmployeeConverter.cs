using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoWebClient
{
    public class EmployeeConverter
    {
        public static string Convert(int employeeId)
        {
            EmployeeDTO emp = SessionManager.EmployeeById(employeeId);

            if (emp == null) return "Unknown employee";

            return emp.FirstName + " " + emp.LastName;
        }

        public static string Number(int employeeId)
        {
            EmployeeDTO emp = SessionManager.EmployeeById(employeeId);

            if (emp == null) return "Unknown employee";

            return emp.EmployeeNumber;
        }
    }
}