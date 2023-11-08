using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.ITable
{
    public interface IProduct
    {
        int ProductId { get; }
        string ProductName { get; }
        string ProductDescription { get; }
        int ProductCategoryId { get; }
        int Stock { get; }
        int MinStockLevel { get; }
        decimal UnitPrice { get; }
        bool IsActive { get; }

        void Initialize(ProductDTO productDTO);
        ServiceResponse<int> Add();
        ServiceResponse<List<ProductDTO>> GetAll();
        ServiceResponse<ProductDTO> GetById(int productId);
        ServiceResponse<int> Update();
        ServiceResponse<int> Delete();
        ServiceResponse<int> Activate();
        ServiceResponse<int> Deactivate();
    }
}
