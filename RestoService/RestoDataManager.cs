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
        public IRole Role { get { return _RoleService; } }

        public RestoDataManager()
        {
            _RoleService = new RoleService();
        }
    }
}
