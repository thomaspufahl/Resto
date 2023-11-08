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
        ServiceResponse<List<RoleDTO>> GetAll();
        ServiceResponse<RoleDTO> GetById(int roleId);
    }
}
