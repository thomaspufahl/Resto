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
        int RoleId { get; }
        string Name { get; }
        string Description { get; }
        List<RoleDTO> GetAll();
    }
}
