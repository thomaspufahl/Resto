using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared
{
    public enum AccessLevel
    {
        NOT_LOGGED = 0,
        LOGGED = 1,
        MANAGER = 2,
    }
    public interface IRestoSecurityManager
    {
        EmployeeDTO LoggedUser { get; }
        bool IsLogged { get; }
        bool IsLoggedAsManager { get; }

        ServiceResponse<bool> Login(string employeeNumber);
        ServiceResponse<bool> Logout();
        bool IsAuthorized(AccessLevel accessLevel);
    }
}
