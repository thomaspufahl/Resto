using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.DTO
{
    public class OrderStatusDTO
    {
        public int OrderStatusId { get; set; }
        public string StatusCode { get; set; }
        public string OrderStatusName { get; set; }
        public string OrderStatusDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
