using RestoService.Service;
using RestoShared;
using RestoShared.ITable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService
{
    public class RestoDataManager : IRestoDataManager
    {
        private RoleService _RoleService;
        private EmployeeService _EmployeeService;

        public IRole Role { get { return _RoleService; } }
        public IEmployee Employee { get { return _EmployeeService; } }

        public RestoDataManager()
        {
            _RoleService = new RoleService();
            _EmployeeService = new EmployeeService();
        }
    }
}
