using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared.ITable
{
    public interface IRestoTable
    {
        byte TableNumber { get; }
        long OrderNumber { get; }
        bool IsActive { get; }

        void Initialize(RestoTableDTO restoTableDTO);
        ServiceResponse<byte> Add();
        ServiceResponse<List<RestoTableDTO>> GetAll();
        ServiceResponse<RestoTableDTO> GetByTableNumber(byte tableNumber);
        ServiceResponse<int> Update();
        ServiceResponse<int> Delete();
        ServiceResponse<int> Activate();
        ServiceResponse<int> Deactivate();
    }
}
