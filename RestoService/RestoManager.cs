using RestoShared;
using RestoShared.ITable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService
{
    public class RestoManager : IRestoManager
    {
        public IRole Role { get; }

        public RestoManager()
        {
            
        }
    }
}
