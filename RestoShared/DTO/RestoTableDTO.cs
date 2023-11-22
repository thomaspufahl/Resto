using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.DTO
{
    public class RestoTableDTO
    {
        public byte TableNumber { get; set; }
        public long OrderNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
