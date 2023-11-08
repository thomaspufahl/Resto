using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.ITable
{
    public interface IProductCategory
    {
        int ProductCategoryId { get; }
        string ProductCategoryName { get; }
        bool IsActive { get; }

        void Initialize(ProductCategoryDTO productCategoryDTO);
        ServiceResponse<int> Add();
        ServiceResponse<List<ProductCategoryDTO>> GetAll();
        ServiceResponse<ProductCategoryDTO> GetById(int productCategoryId);
        ServiceResponse<int> Update();
        ServiceResponse<int> Delete();
        ServiceResponse<int> Activate();
        ServiceResponse<int> Deactivate();
    }
}
