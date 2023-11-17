using RestoService.Service;
using RestoShared;
using RestoShared.ITable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService
{
    public class RestoDataManager : IRestoDataManager
    {
        private RoleService _RoleService;
        private EmployeeService _EmployeeService;
        private ProductCategoryService _ProductCategoryService;
        private ProductService _ProductService;
        private OrderStatusService _OrderStatusService;
        private OrderService _OrderService;
        private OrderItemService _OrderItemService;

        public IRole Role { get { return _RoleService; } }
        public IEmployee Employee { get { return _EmployeeService; } }
        public IProductCategory ProductCategory { get { return _ProductCategoryService; } }        
        public IProduct Product { get { return _ProductService; } }
        public IOrderStatus OrderStatus { get { return _OrderStatusService; } }
        public IOrder Order { get { return _OrderService; } }
        public IOrderItem OrderItem { get { return _OrderItemService; } }

        public RestoDataManager()
        {
            _RoleService = new RoleService();
            _EmployeeService = new EmployeeService();
            _ProductCategoryService = new ProductCategoryService();
            _ProductService = new ProductService();
            _OrderStatusService = new OrderStatusService();
            _OrderService = new OrderService();
            _OrderItemService = new OrderItemService();
        }
    }
}
