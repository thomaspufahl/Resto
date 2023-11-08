using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.DTO
{
    public class ProductCategoryDTO
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
