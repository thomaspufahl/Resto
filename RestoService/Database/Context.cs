using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService.Database
{
    internal class Context
    {
        private static readonly Context _Instance = new Context();
        
        private Context()
        {
        }

        public static Context Instance
        {
            get
            {
                return _Instance;
            }
        }
    }
}
