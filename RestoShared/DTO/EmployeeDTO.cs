using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId{ get; set; }

        public string EmployeeNumber { get; set; }

        public string FirstName{ get; set; }

        public string LastName { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; }

    }
}
