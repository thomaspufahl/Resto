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
        ServiceResponse<int> Add();
        ServiceResponse<List<EmployeeDTO>> GetAll();
        ServiceResponse<EmployeeDTO> GetById(int employeeId);
        ServiceResponse<int> Update();
        ServiceResponse<int> Delete();
        ServiceResponse<int> Activate();
        ServiceResponse<int> Deactivate();
    }
}
