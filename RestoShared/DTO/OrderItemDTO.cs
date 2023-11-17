using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.DTO
{
    public class OrderItemDTO
    {
        public long OrderItemId { get; set; }
        public long OrderNumber { get; set; }
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
