using RestoShared.ITable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoShared
{
    public interface IRestoManager
    {
        IRole Role { get; }
    }
}
