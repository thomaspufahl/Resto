using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.ITable
{
    public interface IOrder
    {
        long OrderNumber { get; }
        int EmployeeId { get; }
        byte TableNumber { get; }
        DateTime OrderDate { get; }
        int OrderStatusId { get; }
        DateTime UpdatedAt { get; }
        bool IsActive { get; }

        void Initialize(OrderDTO orderDTO);
        ServiceResponse<int> Add();
        ServiceResponse<List<OrderDTO>> GetAll();
        ServiceResponse<OrderDTO> GetByOrderNumber(long orderNumber);
        ServiceResponse<int> Update();
        ServiceResponse<int> Delete();
        ServiceResponse<int> Activate();
        ServiceResponse<int> Deactivate();
    }
}
