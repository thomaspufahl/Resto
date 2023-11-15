using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.ITable
{
    public interface IOrderStatus
    {
        int OrderStatusId { get; }
        string StatusCode { get; }
        string OrderStatusName { get; }
        string OrderStatusDescription { get; }
        bool IsActive { get; }

        void Initialize(OrderStatusDTO orderStatusDTO);
        ServiceResponse<int> Add();
        ServiceResponse<List<OrderStatusDTO>> GetAll();
        ServiceResponse<OrderStatusDTO> GetById(int orderStatusId);
        ServiceResponse<OrderStatusDTO> GetByCode(string statusCode);
        ServiceResponse<int> Update();
        ServiceResponse<int> Delete();
        ServiceResponse<int> Activate();
        ServiceResponse<int> Deactivate();
    }
}
