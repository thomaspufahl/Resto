using RestoShared.ITable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared
{
    public interface IRestoDataManager
    {
        IRole Role { get; }
        IEmployee Employee { get; }
        IProductCategory ProductCategory { get; }
        IProduct Product { get; }
        IOrderStatus OrderStatus { get; }
        IOrder Order { get; }
        IOrderItem OrderItem { get; }
    }
}
