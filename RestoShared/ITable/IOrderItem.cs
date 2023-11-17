using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.ITable
{
    public interface IOrderItem
    {
        long OrderItemId { get; }
        long OrderNumber { get; }
        int ProductId { get; }
        byte Quantity { get; }
        bool IsActive { get; }

        void Initialize(OrderItemDTO orderItemDTO);
        ServiceResponse<long> Add();
        ServiceResponse<List<OrderItemDTO>> GetAll();
        ServiceResponse<OrderItemDTO> GetById(long orderItemId);
        ServiceResponse<OrderItemDTO> GetByOrderNumber(long orderNumber);
        ServiceResponse<int> Update();
        ServiceResponse<int> Delete();
        ServiceResponse<int> Activate();
        ServiceResponse<int> Deactivate();
    }
}
