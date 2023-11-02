using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.ITable
{
    public interface IRole
    {
        List<RoleDTO> GetAll();
    }
}
