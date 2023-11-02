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
    public class RestoManager : IRestoManager
    {
        private RoleService _RoleService;
        public IRole Role { get { return _RoleService; } }

        public RestoManager()
        {
            _RoleService = new RoleService();
        }
    }
}
