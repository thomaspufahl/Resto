using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoShared.DTO;


namespace RestoShared.ITable
{

    public interface IEmployee
    {
        int EmployeeId { get; }
        string EmployeeNumber { get; }
        string FirstName { get; }
        string LastName { get; }
        int RoleId { get; }
        bool IsActive { get; }

        void Initialize(EmployeeDTO employeeDTO);
        int Add();
        List<EmployeeDTO> GetAll();
        EmployeeDTO GetById();
        int Update();
        int Delete();
        int Activate();
        int Deactivate();
    }
}
